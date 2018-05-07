using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ToyRobotSimulatorConsole.Models
{
    public class FileProcessor
    {
        private string FilePath;
        private Simulator Simulator;

        /// <summary>
        /// FileProcessor constructor
        /// </summary>
        /// <param name="filePath">Path to the file to process</param>
        /// <param name="simulator">A simulator instance</param>
        public FileProcessor(string filePath, Simulator simulator)
        {
            FilePath = filePath;
            Simulator = simulator;
        }

        /// <summary>
        /// Method to process the file. 
        /// </summary>
        /// <returns>Result of processing the file contents, or a message to say the file has been ignored.</returns>
        public string ProcessFile()
        {
            StringBuilder responseBuilder = new StringBuilder();

            if (Path.GetExtension(FilePath).ToLower() != ".txt")
            {
                responseBuilder.AppendLine($"{FilePath} has been ignored because it is not a txt file type.");
            }
            else
            {
                responseBuilder.AppendLine($"Processing: {FilePath}.");
                IEnumerable<string> commands = File.ReadLines(FilePath);
                responseBuilder.Append(ProcessFileContents(commands));
                responseBuilder.AppendLine($"Processing of {FilePath} is complete.");
                responseBuilder.AppendLine();
            }

            return responseBuilder.ToString();
        }

        /// <summary>
        /// Processes the contents of the file.
        /// </summary>
        /// <param name="commands">The lines in the file</param>
        /// <returns>The result of processing the commands</returns>
        private string ProcessFileContents(IEnumerable<string> commands)
        {
            StringBuilder responseBuilder = new StringBuilder();

            if (commands != null)
            {
                foreach (string command in commands)
                {
                    if (command.Length > 0)
                    {
                        string output = ProcessCommand(command);
                        if (!string.IsNullOrWhiteSpace(output))
                        {
                            responseBuilder.AppendLine(output);
                        }
                    }
                }
            }

            if (responseBuilder.Length == 0)
            {
                responseBuilder.AppendLine("The file has finished processing, but there was nothing to report.");
            }

            return responseBuilder.ToString();
        }

        /// <summary>
        /// Processes correctly provided PLACE, MOVE, LEFT, RIGHT and REPORT commands
        /// </summary>
        /// <param name="command">Command to process</param>
        /// <returns>A response from the command, or an empty string if the command returns no response.</returns>
        private string ProcessCommand(string command)
        {
            string report = string.Empty;
            string[] commandSegments = command.Split(" ");

            switch (commandSegments[0])
            {
                case "PLACE":
                    if (FormatPlaceCommand(command, out int x, out int y, out Orientation f))
                    {
                        Simulator.Place(x, y, f);
                    }
                    break;
                case "MOVE":
                    Simulator.Move();
                    break;
                case "LEFT":
                    Simulator.Left();
                    break;
                case "RIGHT":
                    Simulator.Right();
                    break;
                case "REPORT":
                    report = Simulator.Report();
                    break;
            }

            return report;
        }

        /// <summary>
        /// Helper function to format the place command.
        /// </summary>
        /// <param name="command">Command to analyse</param>
        /// <param name="x">Computed x axis value, defaulted to 0</param>
        /// <param name="y">Computed y axis value, defaulted to 0</param>
        /// <param name="f">Computed orientation, defaulted to NORTH</param>
        /// <returns>True if the formatting was valid, False otherwise.</returns>
        private bool FormatPlaceCommand(string command, out int x, out int y, out Orientation f)
        {
            bool valid = false;
            string placeCommand = command.Replace("PLACE ", "").Replace(" ", "");
            string[] parameters = placeCommand.Split(",");

            // Default orientation and position.
            f = Orientation.NORTH;
            x = 0;
            y = 0;

            if (parameters.Length == 3)
            {
                valid = true;
                valid = valid && int.TryParse(parameters[0], out x);
                valid = valid && int.TryParse(parameters[1], out y);
                valid = valid && Enum.TryParse<Orientation>(parameters[2], out f);
            }

            return valid;
        }
    }
}
