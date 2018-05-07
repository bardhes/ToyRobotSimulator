namespace ToyRobotSimulatorConsole.Models
{
    public class Simulator
    {
        /// <summary>
        /// Instance of the robot
        /// </summary>
        public Robot MyRobot { get; set; }

        /// <summary>
        /// Instance ofthe table
        /// </summary>
        public Table MyTable { get; set; }

        /// <summary>
        /// Constructor creates new instance of <see cref="MyRobot"/> and <see cref="MyTable"/>
        /// </summary>
        public Simulator()
        {
            MyRobot = new Robot();
            MyTable = new Table();
        }

        /// <summary>
        /// Places the robot on the table.
        /// All other commands before Place are ignored.
        /// </summary>
        /// <param name="x">Position on the x axis of the table</param>
        /// <param name="y">Position on the y axis of the table</param>
        /// <param name="f">Direction the robot is facing</param>
        public void Place(int x, int y, Orientation f)
        {
            if (x > -1 && x <= (MyTable.Dimensions.Width - 1) &&
                y > -1 && y <= (MyTable.Dimensions.Height - 1))
            {
                MyRobot.Position = new RobotPosition(x, y, f);
            }
        }

        /// <summary>
        /// Moves the robot one unit in the direction it is facing.
        /// Commands to move a robot that has not been placed are ignored.
        /// Commands to move the robot to a position off the table are ignored.
        /// </summary>
        public void Move()
        {
            if (MyRobot.Position != null)
            {
                int maxX = MyTable.Dimensions.Width - 1;
                int maxY = MyTable.Dimensions.Height - 1;

                switch (MyRobot.Position.Facing)
                {
                    case Orientation.NORTH:
                        if (MyRobot.Position.Y < maxY)
                        {
                            MyRobot.Position.Y++;
                        }
                        break;
                    case Orientation.EAST:
                        if (MyRobot.Position.X < maxX)
                        {
                            MyRobot.Position.X++;
                        }
                        break;
                    case Orientation.SOUTH:
                        if (MyRobot.Position.Y > 0)
                        {
                            MyRobot.Position.Y--;
                        }
                        break;
                    case Orientation.WEST:
                        if (MyRobot.Position.X > 0)
                        {
                            MyRobot.Position.X--;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Turns the robot 90 degrees to the left
        /// </summary>
        public void Left() => MyRobot.Left();

        /// <summary>
        /// Turns the robot 90 degrees to the right
        /// </summary>
        public void Right() => MyRobot.Right();

        /// <summary>
        /// Reports the robot's x,y co-ordinates and the direction it's facing
        /// </summary>
        public string Report() => MyRobot.Report();
    }
}
