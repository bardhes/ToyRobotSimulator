using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ToyRobotSimulatorConsole.Models;

namespace TestProject
{
    [TestFixture]
    class FileProcessorIntegrationTests
    {
        private FileProcessor sut;
        private Simulator simulator;
        private readonly string ConstraintExample1 = "TestCommandFiles/ConstraintExample1.txt";
        private readonly string ConstraintExample2 = "TestCommandFiles/ConstraintExample2.txt";
        private readonly string ConstraintExample3 = "TestCommandFiles/ConstraintExample3.txt";
        private readonly string MyExample1 = "TestCommandFiles/MyExample1.txt";
        private readonly string MyExample2 = "TestCommandFiles/MyExample2.txt";
        private readonly string MyExample3 = "TestCommandFiles/MyExample3.txt";
        private readonly string MyExample4 = "TestCommandFiles/MyExample4.txt";
        
        [SetUp]
        public void Setup()
        {
            simulator = new Simulator();
        }

        [Test]
        public void Simulator_ProcessFile_CommandsToMoveRobotFrom00NorthReports01North()
        {
            // Arrange
            sut = new FileProcessor(ConstraintExample1, simulator);
            StringBuilder expectedProcessingOutput = new StringBuilder();
            expectedProcessingOutput.AppendLine(ReportHelper(0,1, Orientation.NORTH));

            string expected = BuildExpectedOutput(ConstraintExample1, expectedProcessingOutput.ToString());

            // Act
            string actual = sut.ProcessFile();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Simulator_ProcessFile_CommandsToLeftTurnRobotFrom00NorthReports01North()
        {
            // Arrange
            sut = new FileProcessor(ConstraintExample2, simulator);
            StringBuilder expectedProcessingOutput = new StringBuilder();
            expectedProcessingOutput.AppendLine(ReportHelper(0, 0, Orientation.WEST));

            string expected = BuildExpectedOutput(ConstraintExample2, expectedProcessingOutput.ToString());

            // Act
            string actual = sut.ProcessFile();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Simulator_ProcessFile_CommandsToMoveAndLeftTurnRobotReportCorrectLocation()
        {
            // Arrange
            sut = new FileProcessor(ConstraintExample3, simulator);
            StringBuilder expectedProcessingOutput = new StringBuilder();
            expectedProcessingOutput.AppendLine(ReportHelper(3, 3, Orientation.NORTH));

            string expected = BuildExpectedOutput(ConstraintExample3, expectedProcessingOutput.ToString());

            // Act
            string actual = sut.ProcessFile();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Simulator_ProcessFile_CommandsThatPlaceRobotTwiceReportCorrectLocationAfterEachCommand()
        {
            // Arrange
            sut = new FileProcessor(MyExample1, simulator);
            StringBuilder expectedProcessingOutput = new StringBuilder();
            expectedProcessingOutput.AppendLine(ReportHelper(2, 4, Orientation.NORTH));
            expectedProcessingOutput.AppendLine(ReportHelper(3, 3, Orientation.SOUTH));
            expectedProcessingOutput.AppendLine(ReportHelper(3, 2, Orientation.SOUTH));

            string expected = BuildExpectedOutput(MyExample1, expectedProcessingOutput.ToString());

            // Act
            string actual = sut.ProcessFile();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Simulator_ProcessFile_CommandsThatWouldMoveRobotOffTableAreIgnored()
        {
            // Arrange
            sut = new FileProcessor(MyExample2, simulator);
            StringBuilder expectedProcessingOutput = new StringBuilder();
            expectedProcessingOutput.AppendLine(ReportHelper(2, 4, Orientation.NORTH));
            expectedProcessingOutput.AppendLine(ReportHelper(2, 4, Orientation.NORTH));

            string expected = BuildExpectedOutput(MyExample2, expectedProcessingOutput.ToString());

            // Act
            string actual = sut.ProcessFile();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Simulator_ProcessFile_CommandsOccuringBeforeRobotIsPlacedAreIgnored()
        {
            // Arrange
            sut = new FileProcessor(MyExample3, simulator);
            StringBuilder expectedProcessingOutput = new StringBuilder();
            expectedProcessingOutput.AppendLine(ReportHelper(1, 2, Orientation.EAST));
            expectedProcessingOutput.AppendLine(ReportHelper(2, 2, Orientation.EAST));

            string expected = BuildExpectedOutput(MyExample3, expectedProcessingOutput.ToString());

            // Act
            string actual = sut.ProcessFile();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Simulator_ProcessFile_WhenRobotIsNeverPlacedCorrectlyOnTableThereIsNoOutput()
        {
            // Arrange
            sut = new FileProcessor(MyExample4, simulator);
            StringBuilder expectedProcessingOutput = new StringBuilder();
            expectedProcessingOutput.AppendLine("The file has finished processing, but there was nothing to report.");

            string expected = BuildExpectedOutput(MyExample4, expectedProcessingOutput.ToString());

            // Act
            string actual = sut.ProcessFile();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        private string BuildExpectedOutput(string filePath, string processingOutput, bool isTxtFile = true)
        {
            StringBuilder expected = new StringBuilder();

            if (isTxtFile)
            {
                expected.AppendLine($"Processing: {filePath}.");
                expected.Append(processingOutput);
                expected.AppendLine($"Processing of {filePath} is complete.");
                expected.AppendLine();
            }
            else
            {
                expected.AppendLine($"{filePath} has been ignored because it is not a txt file type.");
            }

            return expected.ToString();
        }

        private string ReportHelper(int x, int y, Orientation f)
        {
            return $"x: {x }, y: { y }, Facing: { f }";

        }
    }
}
