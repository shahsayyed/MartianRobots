using System;
using MartianRobots.Core.Models;
using MartianRobots.Core.Instructions;

namespace MartianRobots.Core.Services
{
    public class RobotControllerService : IRobotControllerService
    {
        private const int MAX_GRID_SIZE = 50;
        private const int MAX_INSTRUCTIONS = 100;
        private readonly Dictionary<char, IInstruction> _instructionExecutors;

        public RobotControllerService()
        {
            _instructionExecutors = new Dictionary<char, IInstruction>
            {
                { 'L', new LeftInstruction() },
                { 'R', new RightInstruction() },
                { 'F', new ForwardInstruction() }
            };
        }
        public (bool isValid, string result) MoveRobots(string input)
        {
            var parseResults = ParseInput(input);

            if (!parseResults.isValid)
                return (false, string.Join("\n", parseResults.errors));

            var scents = new HashSet<(int, int)>();
            var results = new List<string>();

            foreach (var robotInput in parseResults.RobotsInputs)
            {
                var finalPosition = ExecuteRobotMovements(robotInput.initialPosition, parseResults.maxX, parseResults.MaxY, robotInput.instructions, scents);
                results.Add($"{finalPosition.X} {finalPosition.Y} {finalPosition.Orientation}" + (finalPosition.Lost ? " LOST" : ""));
            }

            return (true, string.Join("\n", results));
        }

        private Position ExecuteRobotMovements(Position initialPosition, int maxX, int maxY, string instructions, HashSet<(int, int)> scents)
        {
            var newPosition = new Position(initialPosition.X, initialPosition.Y, initialPosition.Orientation);
            foreach (char instruction in instructions)
            {
                if (_instructionExecutors.ContainsKey(instruction))
                {
                    if (newPosition.Lost)
                        break; // Stop processing if the robot has already lost
                    var instructionExecutor = _instructionExecutors[instruction];
                    instructionExecutor.Execute(newPosition, maxX, maxY, scents);
                }
            }
            return newPosition;
        }

        private (bool isValid, int maxX, int MaxY, (Position initialPosition, string instructions)[] RobotsInputs, List<string> errors) ParseInput(string input)
        {
            var errors = new List<string>();

            var lines = input.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length < 2) return (false, 0, 0, Array.Empty<(Position, string)>(), new List<string> { "Invalid input format." });

            // Parse the grid size
            var gridSize = lines[0].Split(' ');
            if (gridSize.Length != 2 || !int.TryParse(gridSize[0], out int width) || !int.TryParse(gridSize[1], out int height))
                return (false, 0, 0, Array.Empty<(Position, string)>(), new List<string> { "Invalid grid size." });

            if (width > MAX_GRID_SIZE || height > MAX_GRID_SIZE || width < 1 || height < 1)
                return (false, 0, 0, Array.Empty<(Position, string)>(), new List<string> { "Grid size exceeds maximum allowed size." });

            var robots = new (Position, string)[(lines.Length - 1) / 2];
            for (int i = 1; i < lines.Length; i += 2)
            {
                // Parse initial position
                var positionParts = lines[i].Split(' ');
                if (positionParts.Length != 3 || !int.TryParse(positionParts[0], out int x) || !int.TryParse(positionParts[1], out int y) || !"NWES".Contains(positionParts[2]))
                    return (false, 0, 0, Array.Empty<(Position, string)>(), new List<string> { $"Invalid robot position at line {i + 1}." });

                var initialPosition = new Position(x, y, positionParts[2][0]);

                // Get movement instructions
                var instructions = lines[i + 1];
                if (instructions.Length > MAX_INSTRUCTIONS)
                    return (false, 0, 0, Array.Empty<(Position, string)>(), new List<string> { $"Movement instructions exceed maximum allowed length at line {i + 2}." });

                robots[i / 2] = (initialPosition, instructions);
            }

            return (true, width, height, robots, errors);
        }
    }
}