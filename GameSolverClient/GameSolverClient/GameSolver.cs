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
        private Thread receiveThread; // Thread for receiving data
        private CancellationTokenSource cancellationTokenSource; // Token source for cancelling the thread

        public event Action<string> DataReceived;

        private int x;
        private int y;
        private int xfin;
        private int yfin;



        private int i = 0;

        private System.Timers.Timer controlTimer;
        private double currentX; // Current position of the mouse
        private double currentY;
        private double prevX;    // Previous position for velocity calculation
        private double prevY;
        private double targetX = 960; // Target position (e.g., center of the screen)
        private double targetY = 540;
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

        private void PDController()
        {
            // Fetch current cursor position
            currentX = data.PositionXY.PositionX;
            currentY = data.PositionXY.PositionY;

            // Fetch velocity
            double velX = data.VelocityXY.VelocityX[1];
            double velY = data.VelocityXY.VelocityY[1];

            // PD Controller Parameters
            double kp = 0.5; // Proportional gain (adjust as needed)
            double kd = 0.1; // Derivative gain (adjust as needed)

            // Calculate errors (distance to target)
            double errorX = targetX - currentX;
            double errorY = targetY - currentY;

            // Compute control signals
            double controlX = kp * errorX - kd * velX;
            double controlY = kp * errorY - kd * velY;

            // Normalize control signals to prevent excessive speed
            double maxSpeed = 10.0; // Maximum speed in pixels per frame
            double controlMag = Math.Sqrt(controlX * controlX + controlY * controlY);

            if (controlMag > maxSpeed)
            {
                double scale = maxSpeed / controlMag;
                controlX *= scale;
                controlY *= scale;
            }

            // Apply the calculated movement
            MoveMouseRelative(controlX, controlY);

            // Update previous position for the next iteration
            prevX = currentX;
            prevY = currentY;
        }

        // Move the mouse cursor relative to its current position
        private void MoveMouseRelative(double offsetX, double offsetY)
        {
            int newPosX = (int)Math.Round(currentX + offsetX);
            int newPosY = (int)Math.Round(currentY + offsetY);
            Cursor.Position = new Point(newPosX, 1080 - newPosY);
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
