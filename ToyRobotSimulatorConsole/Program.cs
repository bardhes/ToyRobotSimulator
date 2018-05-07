using System;
using System.Collections.Generic;
using System.IO;
using ToyRobotSimulatorConsole.Models;

namespace ToyRobotSimulatorConsole
{
    class Program
    {
        private static string CommandDataFolder = "CommandFiles";

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome!!!!");
            Console.WriteLine("Press enter to process the test data.");

            Console.ReadLine();

            try
            {
                foreach (string filePath in GetFilesToProcess(CommandDataFolder))
                {
                    string output = RunExampleCommandFiles(filePath);

                    if (!string.IsNullOrWhiteSpace(output))
                    {
                        Console.WriteLine(output);
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("I'm really sorry, but something went wrong.");
                Console.WriteLine("Maybe try again later?");
                
                // Log the problem
            }

            Console.ReadLine();
        }

        private static IEnumerable<string> GetFilesToProcess(string directory)
        {
            return Directory.GetFiles("CommandFiles");
        }

        private static string RunExampleCommandFiles(string filePath)
        {
            Simulator simulator = new Simulator();
            FileProcessor processor = new FileProcessor(filePath, simulator);
            return processor.ProcessFile();
        }

    }
}
