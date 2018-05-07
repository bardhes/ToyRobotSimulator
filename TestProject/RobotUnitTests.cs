using NUnit.Framework;
using ToyRobotSimulatorConsole.Models;

namespace TestProject
{
    [TestFixture]
    public class RobotUnitTests
    {
        private Robot sut;

        [SetUp]
        public void Setup()
        {
            sut = new Robot();
        }

        [Test]
        public void Robot_Left_WhenRobotHasNotBeenPlaced_PositionIsNull()
        {
            // Arrange

            // Act
            sut.Left();

            //Assert
            Assert.IsNull(sut.Position);
            
        }

        [TestCase(0, 0)]
        [TestCase(4, 4)]
        [TestCase(0, 4)]
        [TestCase(4, 0)]
        public void Robot_Left_WhenRobotHasBeenPlaced_RobotPositionDoesNotChange(int x, int y)
        {
            // Arrange
            sut.Position = new RobotPosition(x, y, Orientation.WEST);


            // Act
            sut.Left();

            //Assert
            Assert.AreEqual(x, sut.Position.X);
            Assert.AreEqual(y, sut.Position.Y);
        }

        [TestCase(Orientation.NORTH, ExpectedResult = Orientation.WEST)]
        [TestCase(Orientation.SOUTH, ExpectedResult = Orientation.EAST)]
        [TestCase(Orientation.EAST, ExpectedResult = Orientation.NORTH)]
        [TestCase(Orientation.WEST, ExpectedResult = Orientation.SOUTH)]
        public Orientation Robot_Left_WhenRobotHasBeenPlaced_RobotDirectionIsCorrectlyChanged(Orientation startingDirection)
        {
            // Arrange
            sut.Position = new RobotPosition(0, 0, startingDirection);

            // Act
            sut.Left();

            //Assert
            return sut.Position.Facing;

        }

        [Test]
        public void Robot_Right_WhenRobotHasNotBeenPlaced_PositionIsNull()
        {
            // Arrange

            // Act
            sut.Right();

            //Assert
            Assert.IsNull(sut.Position);

        }

        [TestCase(Orientation.NORTH, ExpectedResult = Orientation.EAST )]
        [TestCase(Orientation.SOUTH, ExpectedResult = Orientation.WEST)]
        [TestCase(Orientation.EAST, ExpectedResult = Orientation.SOUTH)]
        [TestCase(Orientation.WEST, ExpectedResult = Orientation.NORTH)]
        public Orientation Robot_Right_WhenRobotHasBeenPlaced_RobotDirectionIsCorrectlyChanged(Orientation startingDirection)
        {
            // Arrange
            sut.Position = new RobotPosition(0, 0, startingDirection);

            // Act
            sut.Right();

            //Assert
            return sut.Position.Facing;

        }

        [Test]
        public void Robot_Report_WhenRobotHasNotBeenPlaced_ReturnsEmptyString()
        {
            // Arrange
            
            // Act
            string actual = sut.Report();

            //Assert
            Assert.IsEmpty(actual);

        }

        [Test]
        public void Robot_Report_WhenRobotHasBeenPlaced_ReturnsExpectedString()
        {
            // Arrange
            sut.Position = new RobotPosition(0, 0, Orientation.NORTH);
            string expected = $"x: 0, y: 0, Facing: NORTH";

            // Act
            string actual = sut.Report();

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
