using System;

namespace ToyRobotSimulatorConsole.Models
{
    public enum Turn { LEFT = 0, RIGHT}
    public enum Orientation { NORTH = 0, SOUTH, EAST, WEST }           

    public class Robot
    {
        /// <summary>
        /// Position of the robot on the table
        /// </summary>
        public RobotPosition Position { get; set; }

        /// <summary>
        /// Turn the robot 90 degrees to the left
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the robot has not been placed.</exception>
        /// <exception>Ignores the command if any other exception occurs.</exception>
        public void Left()
        {
            if (Position != null)
            {
                try
                {
                    Turn(Models.Turn.LEFT);
                }
                catch
                {
                    // Do nothing when an exception occurs
                }
            }
        }

        /// <summary>
        /// Turn the robot 90 degrees to the right
        /// </summary>
        /// <param name="facing">The current direction - <see cref="Orientation"/></param>
        /// <returns>Returns the new direction of the robot</returns>
        /// <exception>Ignores the command if an exception occurs.</exception>
        public void Right()
        {
            if (Position != null)
            {
                try
                {
                    Turn(Models.Turn.RIGHT);
                }
                catch
                {
                    // Do nothing when an exception occurs
                }
            }
        }

        /// <summary>
        /// Reports the robot's position on the table
        /// </summary>
        /// <returns>A string representing the robot's x,y co-ordinates and the direction it's facing.</returns>
        public string Report()
        {
            if (Position != null)
            {
                return $"x: { Position.X }, y: { Position.Y }, Facing: { Position.Facing }";
            }

            return string.Empty;
        }

        /// <summary>
        /// Turn the robot on the table.
        /// </summary>
        /// <param name="facing">The current direction - <see cref="Orientation"/></param>
        /// <param name="direction">The direction to turn - <see cref="Models.Turn"/> </param>
        /// <returns>Returns the new direction of the robot</returns>
        /// <exception cref="NotSupportedException">Thrown if the current orientation isn't supported.</exception>
        private void Turn(Turn direction)
        {
            if (Position != null)
            {
                switch (Position.Facing)
                {
                    case Orientation.NORTH:
                        Position.Facing = Models.Turn.RIGHT == direction ? Orientation.EAST : Orientation.WEST;
                        break;
                    case Orientation.EAST:
                        Position.Facing = Models.Turn.RIGHT == direction ? Orientation.SOUTH : Orientation.NORTH;
                        break;
                    case Orientation.SOUTH:
                        Position.Facing = Models.Turn.RIGHT == direction ? Orientation.WEST : Orientation.EAST;
                        break;
                    case Orientation.WEST:
                        Position.Facing = Models.Turn.RIGHT == direction ? Orientation.NORTH : Orientation.SOUTH;
                        break;
                }

                // Ideally, if we get here we should log the exception

                throw new NotSupportedException($"The current robot orientation ({ Position.Facing.ToString() }) has not been recognised.");
            }
        }
    }    
}
