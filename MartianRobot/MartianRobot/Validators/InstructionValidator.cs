using FluentValidation;
using MartianRobot.Constants;
using MartianRobot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobot.Validators
{
    public class InstructionValidator : AbstractValidator<Instruction>
    {
        private const string InstructionPattern = "[RFLrfl]+";

        public InstructionValidator()
        {
            RuleFor(instruction => instruction.InstructionString)
                .NotEmpty()
                .Matches(InstructionPattern)
                .WithMessage("Instruction string should not be null and should contain R F L letters (case insensitive)");

            When(x => x.InstructionString != null, () => {
                RuleFor(instruction => instruction.InstructionString)
                    .Must(x => x.Length > 0 && x.Length <= MartianRobotConstants.MaxInstructionSize);
            });
        }
    }
}
