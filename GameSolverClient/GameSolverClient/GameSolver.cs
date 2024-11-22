using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GameSolverClient
{
    public class GameSolver
    {

        private TcpClient client;  // The TCP client
        private NetworkStream stream; // The network stream for communication
        private CancellationTokenSource cancellationTokenSource; // Token source for cancelling the thread

        public event Action<string> DataReceived;

        private double currentX; // Current position of the mouse
        private double currentY;
        private double prevX;    // Previous position for velocity calculation
        private double prevY;
        private Data data = new Data();

        public async Task ConnectToServer()
        {
            try
            {
                string serverIP = "localhost"; // Change to the server's IP
                int port = 6000; // Port of the server

                client = new TcpClient();
                await client.ConnectAsync(serverIP, port);
                stream = client.GetStream();

                // Create a new CancellationTokenSource
                cancellationTokenSource = new CancellationTokenSource();

                // Start a task to listen for incoming messages
                _ = ReceiveDataAsync(cancellationTokenSource.Token);

                DataReceived?.Invoke("Connected...");
            }
            catch (Exception ex)
            {
                DataReceived?.Invoke($"Error: {ex.Message}");
            }
        }

        public async Task ReceiveDataAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (stream != null && stream.CanRead)
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                            try
                            {
                                data = JsonConvert.DeserializeObject<Data>(receivedMessage);

                                PDController();

                                /*// Access the parsed data
                                Console.WriteLine("Target X: " + data.TargetXY.TargetX);
                                Console.WriteLine("Target Y: " + data.TargetXY.TargetY);
                                Console.WriteLine("Position X: " + data.PositionXY.PositionX);
                                Console.WriteLine("Position Y: " + data.PositionXY.PositionY);
                                Console.WriteLine("Velocity X: " + string.Join(", ", data.VelocityXY.VelocityX));
                                Console.WriteLine("Velocity Y: " + string.Join(", ", data.VelocityXY.VelocityY));
                                Console.WriteLine("History X: " + string.Join(", ", data.HistoryXY.HistoryX));
                                Console.WriteLine("History Y: " + string.Join(", ", data.HistoryXY.HistoryY));
                                Console.WriteLine("Time: " + data.Time);
                                Console.WriteLine("Level: " + data.Level);*/

                                DataReceived?.Invoke(Math.Floor(data.PositionXY.PositionX).ToString() + " " + Math.Floor(data.PositionXY.PositionY).ToString());
                            }
                            catch (JsonException ex)
                            {
                                Console.WriteLine("Error parsing JSON: " + ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DataReceived?.Invoke($"Error: {ex.Message}");
            }
        }

        public void SolveTheGame()
        {
            //Cursor.Position = new Point(xfin, 1080 - yfin);

        }

        private void PDController1()
        {
            // Fetch current cursor position
            currentX = data.PositionXY.PositionX;
            currentY = data.PositionXY.PositionY;

            // PD Controller Parameters
            double kp = 0.1; // Proportional gain (adjust as needed)

            // Calculate errors (distance to target)
            double errorX = data.TargetXY.TargetX - currentX;
            double errorY = data.TargetXY.TargetY - currentY;

            // Compute control signals
            double controlX = kp * errorX;
            double controlY = kp * errorY;

            // Normalize control signals to prevent excessive speed
            double maxSpeed = 100.0; // Maximum speed in pixels per frame
            double controlMag = Math.Sqrt(controlX * controlX + controlY * controlY);

            if (controlMag > maxSpeed)
            {
                double scale = maxSpeed / controlMag;
                controlX *= scale;
                controlY *= scale;
            }

            double mouseX = 1920 / 2 + controlX * (1920 / 2) / maxSpeed;
            double mouseY = 1080 / 2 + controlY * (1080 / 2) / maxSpeed;

            // Apply the calculated movement
            MoveMouseAbsolute(mouseX, mouseY);

            // Update previous position for the next iteration
            prevX = currentX;
            prevY = currentY;
        }

        private void PDController()
        {
            // Fetch current cursor position
            currentX = data.PositionXY.PositionX;
            currentY = data.PositionXY.PositionY;

            // Fetch velocity
            double velX = data.VelocityXY.VelocityX[1];
            double velY = data.VelocityXY.VelocityY[1];

            // PD Controller Parameters
            double kp = 0.1; // Proportional gain (adjust as needed)
            double kd = 0.5; // Derivative gain (adjust as needed)

            // Calculate errors (distance to target)
            double errorX = data.TargetXY.TargetX - currentX;
            double errorY = data.TargetXY.TargetY - currentY;

            // Compute control signals
            double controlX = kp * errorX - kd * velX;
            double controlY = kp * errorY - kd * velY;

            // Normalize control signals to prevent excessive speed
            double maxAcceleration = 100.0; // Maximum speed in pixels per frame
            double controlMag = Math.Sqrt(controlX * controlX + controlY * controlY);

            if (controlMag > maxAcceleration)
            {
                double scale = maxAcceleration / controlMag;
                controlX *= scale;
                controlY *= scale;
            }

            double mouseX = 1920 / 2 + controlX * (1920 / 2) / maxAcceleration;
            double mouseY = 1080 / 2 + controlY * (1080 / 2) / maxAcceleration;

            // Apply the calculated movement
            MoveMouseAbsolute(mouseX, mouseY);

            // Update previous position for the next iteration
            prevX = currentX;
            prevY = currentY;
        }

        private void MoveMouseAbsolute(double mouseX, double mouseY)
        {
            Cursor.Position = new Point((int)mouseX, 1080 - (int)mouseY);
        }

        // Method to disconnect from the server
        public void DisconnectFromServer()
        {
            // Request cancellation of the receive task
            cancellationTokenSource?.Cancel(); // Cancel the task
            stream?.Close();
            client?.Close();

            // Dispose of the CancellationTokenSource
            cancellationTokenSource?.Dispose();
        }
    }
}
