using EnsureThat;
using FluentValidation;
using MartianRobot.Models;
using MartianRobot.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobot.Mappers.Implementation
{
    public class InstructionMapper : IInstructionMapper
    {
        public Instruction Map(string instructions)
        {
            EnsureArg.IsNotNullOrEmpty(instructions, nameof(instructions));

            var instruction = new Instruction { InstructionString = instructions };

            var instructionValidator = new InstructionValidator();
            instructionValidator.ValidateAndThrow(instruction);

            return instruction;

        }
    }
}
