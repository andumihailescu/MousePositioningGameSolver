using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Diagnostics;

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
        private Data data = new Data();
        private bool startSolving = false;

        private double kp;
        private double kd;

        public void SetKpKd(double kp, double kd)
        {
            this.kp = kp;
            this.kd = kd;
        }

        public bool StartSolving { get => startSolving; set => startSolving = value; }

        public LineSeries lineSeriesX;
        public LineSeries lineSeriesY;

        private Stopwatch stopWatch;

        public GameSolver()
        {
            lineSeriesX = new LineSeries();
            lineSeriesY = new LineSeries();

            stopWatch = new Stopwatch();
        }

        public async Task ConnectToServer()
        {
            try
            {
                string serverIP = "localhost";
                int port = 6000;
                client = new TcpClient();
                await client.ConnectAsync(serverIP, port);
                stream = client.GetStream();
                cancellationTokenSource = new CancellationTokenSource();
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
                                if (startSolving == false)
                                {
                                    stopWatch.Start();
                                    startSolving = true;
                                }
                                SolveTheGame();
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
            try
            {
                DataReceived?.Invoke(Math.Floor(data.PositionXY.PositionX).ToString() + " " + Math.Floor(data.PositionXY.PositionY).ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error displaying position: " + ex.Message);
            }
            switch (data.Level)
            {
                case 1:
                    SolveLevel0();
                    break;
                case 2:
                    SolveLevel1();
                    break;
                case 3:
                    SolveLevel2();
                    break;
            }
        }

        private void SolveLevel0()
        {
            double mouseX = data.TargetXY.TargetX;
            double mouseY = data.TargetXY.TargetY;
            MoveMouseAbsolute(mouseX, mouseY);
        }

        private void SolveLevel1()
        {
            currentX = data.PositionXY.PositionX;
            currentY = data.PositionXY.PositionY;

            double errorX = data.TargetXY.TargetX - currentX;
            double errorY = data.TargetXY.TargetY - currentY;

            double controlX = kp * errorX;
            double controlY = kp * errorY;

            double maxSpeed = 100.0;
            double controlMag = Math.Sqrt(controlX * controlX + controlY * controlY);

            if (controlMag > maxSpeed)
            {
                double scale = maxSpeed / controlMag;
                controlX *= scale;
                controlY *= scale;
            }

            double mouseX = 1920 / 2 + controlX * (1920 / 2) / maxSpeed;
            double mouseY = 1080 / 2 + controlY * (1080 / 2) / maxSpeed;

            MoveMouseAbsolute(mouseX, mouseY);
        }

        private void SolveLevel2()
        {
            currentX = data.PositionXY.PositionX;
            currentY = data.PositionXY.PositionY;

            double velX = data.VelocityXY.VelocityX[1];
            double velY = data.VelocityXY.VelocityY[1];

            double errorX = data.TargetXY.TargetX - currentX;
            double errorY = data.TargetXY.TargetY - currentY;

            double controlX = kp * errorX - kd * velX;
            double controlY = kp * errorY - kd * velY;

            double maxAcceleration = 100.0;
            double controlMag = Math.Sqrt(controlX * controlX + controlY * controlY);

            if (controlMag > maxAcceleration)
            {
                double scale = maxAcceleration / controlMag;
                controlX *= scale;
                controlY *= scale;
            }

            double mouseX = 1920 / 2 + controlX * (1920 / 2) / maxAcceleration;
            double mouseY = 1080 / 2 + controlY * (1080 / 2) / maxAcceleration;

            MoveMouseAbsolute(mouseX, mouseY);
        }

        private void MoveMouseAbsolute(double mouseX, double mouseY)
        {
            Cursor.Position = new Point((int)mouseX, 1080 - (int)mouseY);
            AddPoints(mouseX, mouseY);
        }

        private void AddPoints(double x, double y)
        {
            var time = (stopWatch.ElapsedMilliseconds / 0.1) * 0.1;
            lineSeriesX.Points.Add(new DataPoint(time, x));
            lineSeriesY.Points.Add(new DataPoint(time, y));
        }

        public void DisconnectFromServer()
        {
            cancellationTokenSource?.Cancel();
            stream?.Close();
            client?.Close();

            cancellationTokenSource?.Dispose();
        }
    }
}
