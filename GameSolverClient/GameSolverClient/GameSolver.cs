﻿using System.Net.Sockets;
using System.Text;
using System.Threading;

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

        public void ConnectToServer()
        {
            try
            {
                string serverIP = "localhost"; // Change to the server's IP
                int port = 6000; // Port of the server

                client = new TcpClient();
                client.Connect(serverIP, port);
                stream = client.GetStream();

                // Create a new CancellationTokenSource
                cancellationTokenSource = new CancellationTokenSource();

                // Start a thread to listen for incoming messages
                receiveThread = new Thread(() => ReceiveData(cancellationTokenSource.Token));
                receiveThread.IsBackground = true;
                receiveThread.Start();

                MessageBox.Show("Connected to the server!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        public void ReceiveData(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (stream != null && stream.CanRead)
                    {
                        byte[] buffer = new byte[1024];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            string[] parts = receivedMessage.Split(',');
                            if (i == 0)
                            {
                                xfin = Convert.ToInt32(parts[0]);
                                yfin = Convert.ToInt32(parts[1]);
                                Console.WriteLine(xfin + " " + yfin);
                            }
                            else
                            {
                                x = Convert.ToInt32(parts[0]);
                                y = Convert.ToInt32(parts[1]);
                                DataReceived?.Invoke(x.ToString() + " " + y.ToString());
                            }
                            i++;
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
            Cursor.Position = new Point(xfin, 1080 - yfin);
        }

        // Method to disconnect from the server
        public void DisconnectFromServer()
        {
            // Request cancellation of the receive thread
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel(); // Cancel the thread
            }
            if (receiveThread != null && receiveThread.IsAlive)
            {
                receiveThread.Join(); // Wait for the thread to finish before closing resources
            }
            if (stream != null)
            {
                stream.Close();
            }
            if (client != null)
            {
                client.Close();
            }

            // Dispose of the CancellationTokenSource
            cancellationTokenSource?.Dispose();
        }
    }
}
