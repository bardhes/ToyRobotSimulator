using NUnit.Framework;
using System.Drawing;
using ToyRobotSimulatorConsole.Models;

namespace TestProject
{
    [TestFixture]
    public class SimulatorUnitTests
    {       
        private Simulator sut;

        [SetUp]
        public void Setup()
        {
            sut = new Simulator();
        }

        [Test]
        public void Simulator_Constructor_CreatesTableAndRobot()
        {
            // Arrange

            // Act

            //Assert
            Assert.IsNotNull(sut.MyRobot);
            Assert.IsNotNull(sut.MyTable);
        }

        [Test]
        public void Simulator_Constructor_CreatesDefault5By5Table()
        {
            // Arrange
            int expectedWidth = 5;
            int expectedHeight = 5;
            // Act

            //Assert
            Assert.AreEqual(expectedWidth, sut.MyTable.Dimensions.Width);
            Assert.AreEqual(expectedHeight, sut.MyTable.Dimensions.Height);
        }

        [TestCase(5, 5, 0, 0, Orientation.NORTH)]
        [TestCase(5, 5, 4, 4, Orientation.NORTH)]
        [TestCase(5, 5, 0, 4, Orientation.NORTH)]
        [TestCase(5, 5, 4, 0, Orientation.NORTH)]
        [TestCase(5, 5, 0, 0, Orientation.WEST)]
        [TestCase(5, 5, 4, 4, Orientation.WEST)]
        [TestCase(5, 5, 0, 4, Orientation.WEST)]
        [TestCase(5, 5, 4, 0, Orientation.WEST)]
        public void Simulator_Place_CorrectlyPlacesRobotOn5By5Table(int tableWidth, int tableLength, int x, int y, Orientation facing)
        {
            // Arrange
            sut.MyTable.Dimensions = new Size(tableWidth, tableLength);

            // Act;
            sut.Place(x, y, facing);
            
            //Assert
            Assert.AreEqual(x, sut.MyRobot.Position.X);
            Assert.AreEqual(y, sut.MyRobot.Position.Y);
            Assert.AreEqual(facing, sut.MyRobot.Position.Facing);

        }

        [TestCase(5, 5, -1, -1, Orientation.NORTH)]
        [TestCase(5, 5, 5, 5, Orientation.NORTH)]
        [TestCase(5, 5, -1, 5, Orientation.NORTH)]
        [TestCase(5, 5, 5, -1, Orientation.NORTH)]
        [TestCase(5, 5, 0, -1, Orientation.NORTH)]
        [TestCase(5, 5, 0, 5, Orientation.NORTH)]
        [TestCase(5, 5, -1, 0, Orientation.NORTH)]
        [TestCase(5, 5, 5, 0, Orientation.NORTH)]
        public void Simulator_Place_InvalidXOrY_DoesNotPlaceRobotOn5By5Table(int tableWidth, int tableLength, int x, int y, Orientation facing)
        {
            // Arrange
            sut.MyTable.Dimensions = new Size(tableWidth, tableLength);

            // Act;
            sut.Place(x, y, facing);

            //Assert
            Assert.IsNull(sut.MyRobot.Position);
        }

        [TestCase(5, 5, 0, 3, Orientation.NORTH)]
        [TestCase(5, 5, 3, 4, Orientation.EAST)]
        [TestCase(5, 5, 4, 1, Orientation.SOUTH)]
        [TestCase(5, 5, 1, 0, Orientation.WEST)]
        public void Simulator_Move_ValidMove_MovesRobotOn5By5Table(int tableWidth, int tableLength, int x, int y, Orientation facing)
        {
            // Arrange
            sut.MyTable.Dimensions = new Size(tableWidth, tableLength);
            sut.Place(x, y, facing);
            RobotPosition expected = GetExpectedPosition(sut.MyRobot.Position);

            // Act;
            sut.Move();

            //Assert
            Assert.IsTrue(x != sut.MyRobot.Position.X || y != sut.MyRobot.Position.Y, "The robot has not been moved.");
            Assert.AreEqual(expected.X, sut.MyRobot.Position.X);
            Assert.AreEqual(expected.Y, sut.MyRobot.Position.Y);
            Assert.AreEqual(expected.Facing, sut.MyRobot.Position.Facing);
        }

        [TestCase(5, 5, 0, 4, Orientation.NORTH)]
        [TestCase(5, 5, 4, 4, Orientation.EAST)]
        [TestCase(5, 5, 4, 0, Orientation.SOUTH)]
        [TestCase(5, 5, 0, 0, Orientation.WEST)]
        public void Simulator_Move_InvalidMove_RobotRemainsInPlaceOn5By5Table(int tableWidth, int tableLength, int x, int y, Orientation facing)
        {
            // Arrange
            sut.MyTable.Dimensions = new Size(tableWidth, tableLength);
            sut.Place(x, y, facing);
            RobotPosition expected = sut.MyRobot.Position;

            // Act;
            sut.Move();

            //Assert
            Assert.IsTrue(expected.X == sut.MyRobot.Position.X && expected.Y == sut.MyRobot.Position.Y, "The robot has moved.");
            Assert.AreEqual(expected.X, sut.MyRobot.Position.X);
            Assert.AreEqual(expected.Y, sut.MyRobot.Position.Y);
            Assert.AreEqual(expected.Facing, sut.MyRobot.Position.Facing);
        }

        private RobotPosition GetExpectedPosition(RobotPosition currentPosition)
        {
            switch (currentPosition.Facing)
            {
                case Orientation.NORTH:
                    if (currentPosition.Y == 3)
                    {
                        return new RobotPosition(currentPosition.X, 4, Orientation.NORTH);
                    }
                    break;
                case Orientation.EAST:
                    if (currentPosition.X == 3)
                    {
                        return new RobotPosition( 4, currentPosition.Y, Orientation.EAST);                        
                    }
                    break;
                case Orientation.SOUTH:
                    if (currentPosition.Y == 1)
                    {
                        return new RobotPosition(currentPosition.X, 0, Orientation.SOUTH);
                    }
                    break;
                case Orientation.WEST:
                    if (currentPosition.X == 1)
                    {
                        return new RobotPosition(0, currentPosition.Y, Orientation.WEST);
                    }
                    break;
            }

            return currentPosition;
        }        
    }
}
