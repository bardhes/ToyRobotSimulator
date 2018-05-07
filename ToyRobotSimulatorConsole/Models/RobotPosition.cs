namespace ToyRobotSimulatorConsole.Models
{
    public class RobotPosition
    {
        /// <summary>
        /// X-Axis value
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y-Axis value
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Direction the robot is facing
        /// </summary>
        public Orientation Facing { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">X-Axis value</param>
        /// <param name="y">Y-Axis value</param>
        /// <param name="facing">Direction the robot is facing</param>
        public RobotPosition(int x, int y, Orientation facing)
        {
            X = x;
            Y = y;
            Facing = facing;
        }
    }
}
