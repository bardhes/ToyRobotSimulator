using System.Drawing;

namespace ToyRobotSimulatorConsole.Models
{
    public class Table
    {
        /// <summary>
        /// Dimensions of the table.
        /// Table is dimensions are defaulted to 5 by 5.
        /// </summary>
        public Size Dimensions { get; set; } = new Size(5, 5);

        /// <summary>
        /// Table constructor.
        /// </summary>
        public Table() { }
    }
}
