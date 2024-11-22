using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSolverClient
{
    public class Target
    {
        public float TargetX { get; set; }
        public float TargetY { get; set; }
    }

    public class Position
    {
        public float PositionX { get; set; }
        public float PositionY { get; set; }
    }

    public class Velocity
    {
        public List<double> VelocityX { get; set; }
        public List<double> VelocityY { get; set; }
    }

    public class History
    {
        public List<double> HistoryX{ get; set; }
        public List<double> HistoryY { get; set; }
    }

    public class Data
    {
        public Target TargetXY { get; set; }
        public Position PositionXY { get; set; }
        public Velocity VelocityXY { get; set; }
        public History HistoryXY { get; set; }
        public float Time { get; set; }
        public int Level { get; set; }
    }
}
