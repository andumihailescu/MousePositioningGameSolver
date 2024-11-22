using System.Net.Sockets;
using System.Text;

namespace GameSolverClient
{
    public partial class Form1 : Form
    {
        private GameSolver gameSolver;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gameSolver = new GameSolver();
            gameSolver.DataReceived += OnDataReceived;
        }

        private void OnDataReceived(string message)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => OnDataReceived(message)));
            }
            else
            {
                txtReceived.AppendText(message + Environment.NewLine);
                txtReceived.ScrollToCaret();
            }
        }

        private void Form1_Close(object sender, FormClosedEventArgs e)
        {
            gameSolver.DisconnectFromServer();
        }

        private void connectToServerBtn_Click(object sender, EventArgs e)
        {
            gameSolver.ConnectToServer();
        }

        private void solveTheGameBtn_Click(object sender, EventArgs e)
        {
            //gameSolver.SolveTheGame();
        }
    }
}
