using FizzWare.NBuilder;
using FluentValidation.TestHelper;
using MartianRobot.Constants;
using MartianRobot.Models;
using MartianRobot.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobot.UnitTests.Validators
{
    [TestClass]
    public class GridValidatorTests
    {
        private GridValidator validator => new GridValidator();

        [TestMethod]
        public void Given_GridValidator_When_AllFieldsSuppliedAndAreValid_Then_PassValidation()
        {
            var grid = Builder<Grid>
                .CreateNew()
                .With(x => x.XScale = 1)
                .With(x => x.YScale = 2)
                .Build();

            this.validator.ShouldNotHaveValidationErrorFor(x => x.XScale, grid);
            this.validator.ShouldNotHaveValidationErrorFor(x => x.YScale, grid);
        }

        [TestMethod]
        public void Given_GridValidator_When_RequiredFieldsNotSupplied_Then_FailValidation()
        {
            var grid = new Grid();
            this.validator.ShouldHaveValidationErrorFor(x => x.XScale, grid)
                .WithErrorMessage("The scale should be greater than 0");
            this.validator.ShouldHaveValidationErrorFor(x => x.YScale, grid)
                .WithErrorMessage("The scale should be greater than 0");
        }

        [TestMethod]
        [DataRow(0, "The scale should be greater than 0")]
        [DataRow(-1, "The scale should be greater than 0")]
        [DataRow(MartianRobotConstants.MaxScaleSize + 1, "The scale should be less than 50")]
        public void Given_GridValidator_When_InvalidRequiredFields_Then_FailValidation(int scale, string errorMessage)
        {
            var grid = Builder<Grid>
                .CreateNew()
                .With(x => x.XScale = scale)
                .With(x => x.YScale = scale)
                .Build();

            this.validator.ShouldHaveValidationErrorFor(x => x.XScale, grid)
                .WithErrorMessage(errorMessage);
            this.validator.ShouldHaveValidationErrorFor(x => x.YScale, grid)
                .WithErrorMessage(errorMessage);
        }
    }
}
