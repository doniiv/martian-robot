using EnsureThat;
using MartianRobot.Constants;
using MartianRobot.Extensions;
using MartianRobot.Mappers;
using MartianRobot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MartianRobot.Services.Implementation
{
    public class ProcessingService : IProcessingService
    {
        private IRobotMapper robotMapper;
        private IInstructionMapper instructionMapper;

        public ProcessingService(IRobotMapper robotMapper, IInstructionMapper instructionMapper)
        {
            this.robotMapper = robotMapper;
            this.instructionMapper = instructionMapper;
        }

        public string ProcessRobot(ref Grid grid, IList<string> robotPosition, string instructions)
        {
            EnsureArg.IsNotNull(robotPosition, nameof(robotPosition));
            EnsureArg.IsNotNull(grid, nameof(grid));
            EnsureArg.IsNotNullOrEmpty(instructions, nameof(instructions));

            var robot = this.robotMapper.Map(robotPosition);
            var instruction = this.instructionMapper.Map(instructions);

            robot = this.ProcessInstructions(robot, instruction, ref grid);

            var lost = robot.IsLost ? MartianRobotConstants.LostLabel : string.Empty;

            return $"{robot.XPosition} {robot.YPosition} {robot.CurrentDirection} {lost}";
        }

        private Robot ProcessInstructions(Robot robot, Instruction instruction, ref Grid grid)
        {
            var directionEnumerator = Orientation.Directions.GetCircularEnumerator<Direction>();
            directionEnumerator.SetStartIndex(Orientation.Directions.IndexOf(robot.CurrentDirection));

            foreach (var inst in instruction.InstructionString)
            {
                var initialX = robot.XPosition;
                var initialY = robot.YPosition;

                switch (inst)
                {
                    case 'F': this.Move(robot); break;
                    case 'R': directionEnumerator.MoveNext(); robot.CurrentDirection = (Direction)directionEnumerator.Current; break;
                    case 'L': directionEnumerator.MovePrevious(); robot.CurrentDirection = (Direction)directionEnumerator.Current; break;
                }

                //Verify if current position/orientation is present in grid scents
                if (grid.Scents.Any(x => x[0].Equals(initialX) && x[1].Equals(initialY) && x[2].Equals((int)robot.CurrentDirection)))
                {
                    robot.XPosition = initialX;
                    robot.YPosition = initialY;
                    continue;
                }

                //Verify if the robot fell off from grid after the action
                if ((robot.IsLost = this.IsLost(robot, grid)) == true)
                {
                    grid.Scents.Add(new int[] { initialX, initialY, (int)robot.CurrentDirection });
                    robot.XPosition = initialX;
                    robot.YPosition = initialY;
                    return robot;
                }
            }

            return robot;
        }

        private void Move(Robot robot)
        {

            switch (robot.CurrentDirection)
            {
                case Direction.N: robot.YPosition++; break;
                case Direction.S: robot.YPosition--; break;
                case Direction.E: robot.XPosition++; break;
                case Direction.W: robot.XPosition--; break;
            }


        }

        private bool IsLost(Robot robot, Grid grid)
        {
            return !((robot.XPosition >= 0 && robot.XPosition <= grid.XScale) && (robot.YPosition >= 0 && robot.YPosition <= grid.YScale));
        }
    }
}
