using FizzWare.NBuilder;
using FluentAssertions;
using MartianRobot.Mappers.Implementation;
using MartianRobot.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MartianRobot.UnitTests.Mappers
{
    [TestClass]
    public class InstructionMapperTests
    {
        private readonly InstructionMapper mapper;

        public InstructionMapperTests()
        {
            mapper = new InstructionMapper();
        }

        [TestMethod]
        public void Given_MappingFrom_Instructions_When_MappingTo_Instructions_Then_FieldsAreMapped()
        {
            var instructions = Builder<string>
                    .CreateNew()
                    .WithFactory(() => new string("L"))
                    .Build();

            var result = mapper.Map(instructions);

            result.Should().NotBeNull();
            result.InstructionString.Should().Be(instructions);
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void Given_MappingFrom_Instructions_When_MappingTo_Instructions_AndInstructionsIsEmpty_Then_ArgumentExceptionIsThrown(string instructions)
        {
            Func<Instruction> func = () => mapper.Map(instructions);

            func.Should().Throw<ArgumentException>();
        }


    }
}
