using FluentValidation;
using MartianRobot.Constants;
using MartianRobot.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobot.Validators
{
    public class GridValidator : AbstractValidator<Grid>
    {
        private static string GreaterZeroErrorMessage = "The scale should be greater than 0";
        private static string LessThanMaximumErrorMessage = $"The scale should be less than {MartianRobotConstants.MaxScaleSize}";
        public GridValidator()
        {
            RuleFor(grid => grid.XScale)
                .GreaterThan(0)
                .WithMessage(GreaterZeroErrorMessage)
                .LessThan(MartianRobotConstants.MaxScaleSize)
                .WithMessage(LessThanMaximumErrorMessage);

            RuleFor(grid => grid.YScale)
                .GreaterThan(0)
                .WithMessage(GreaterZeroErrorMessage)
                .LessThan(MartianRobotConstants.MaxScaleSize)
                .WithMessage(LessThanMaximumErrorMessage);
        }
    }
}
