using FizzWare.NBuilder;
using FluentValidation.TestHelper;
using MartianRobot.Models;
using MartianRobot.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobot.UnitTests.Validators
{
    [TestClass]
    public class InstructionValidatorTests
    {
        private InstructionValidator validator => new InstructionValidator();

        [TestMethod]
        [DataRow("FRL")]
        [DataRow("frl")]
        [DataRow("frlabc")]
        public void Given_InstructionValidator_When_AllFieldsSuppliedAndAreValid_Then_PassValidation(string instructionString)
        {
            var instruction = Builder<Instruction>
                .CreateNew()
                .With(x => x.InstructionString = instructionString)
                .Build();

            this.validator.ShouldNotHaveValidationErrorFor(x => x.InstructionString, instruction);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("abc")]
        [DataRow("FRLFRLFRLFRLFLRFLRLFLRLFLRFLRLFRLFFLRLFRLRFLRFLRFLRLFRLFLRFLRFLRFLRFLRLFRFLRLFRLFFLRLFRLFFLRLFRLRFLRFLRFLRLFRLFLRFLRFLRFLRFLRLFRFLRLFRLF")]
        public void Given_InstructionValidator_When_RequiredFieldsAreInvalid_Then_FailValidation(string instructionString)
        {
            var instruction = Builder<Instruction>
                .CreateNew()
                .With(x => x.InstructionString = instructionString)
                .Build();

            this.validator.ShouldHaveValidationErrorFor(x => x.InstructionString, instruction);
        }
    }
}
