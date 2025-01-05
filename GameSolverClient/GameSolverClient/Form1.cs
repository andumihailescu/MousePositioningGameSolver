using OxyPlot.WindowsForms;
using OxyPlot;
using System.Net.Sockets;
using System.Text;
using OxyPlot.Series;
using System.Diagnostics.Metrics;

namespace GameSolverClient
{
    public partial class Form1 : Form
    {
        private GameSolver gameSolver;
        private PlotView plotView;
        private PlotModel plotModel;
        private LineSeries lineSeriesX;
        private LineSeries lineSeriesY;

        private double kp;
        private double kd;

        public Form1()
        {
            InitializeComponent();
            gameSolver = new GameSolver();
            gameSolver.DataReceived += OnDataReceived;
            plotView = new PlotView();
            plotView.Location = new Point(0, 0);
            plotView.Size = new Size(600, 350);
            this.Controls.Add(plotView);
            plotModel = new PlotModel { Title = "Example Plot" };
            plotView.Model = plotModel;
            lineSeriesX = gameSolver.lineSeriesX;
            lineSeriesY = gameSolver.lineSeriesY;
            lineSeriesX.Title = "Velocity X";
            lineSeriesY.Title = "Velocity Y";
            lineSeriesX.MarkerSize = 0.1;
            lineSeriesY.MarkerSize = 0.1;
            plotModel.Series.Add(lineSeriesX);
            plotModel.Series.Add(lineSeriesY);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void OnDataReceived(string message)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => OnDataReceived(message)));
            }
            else
            {
                plotView.Model.InvalidatePlot(true);
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
            kp = Convert.ToDouble(kpNumeric.Value);
            kd = Convert.ToDouble(kdNumeric.Value);
            gameSolver.SetKpKd(kp, kd);
            gameSolver.ConnectToServer();
        }

        private void solveTheGameBtn_Click(object sender, EventArgs e)
        {
            gameSolver.StartSolving = true;
        }
    }
}
