using FizzWare.NBuilder;
using FluentAssertions;
using MartianRobot.Mappers;
using MartianRobot.Models;
using MartianRobot.Services.Implementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobot.UnitTests.Services
{
    [TestClass]
    public class ProcessingServiceTests
    {
        private readonly AutoMocker mocker;
        private readonly ProcessingService processingService;

        public ProcessingServiceTests()
        {
            this.mocker = new AutoMocker();
            this.processingService = this.mocker.CreateInstance<ProcessingService>();
        }

        [TestMethod]
        public void Given_ProcessingService_When_ProcessingSucceeds_Then_CallsExpected()
        {
            var grid = FizzWare.NBuilder.Builder<Grid>
                .CreateNew()
                .With(x => x.XScale = 1)
                .With(x => x.YScale = 2)
                .Build();

            var robotPosition = Builder<string>
                .CreateListOfSize(3)
                .TheFirst(1)
                .WithFactory(x => new string("1"))
                .TheNext(1)
                .WithFactory(x => new string("2"))
                .TheNext(1)
                .WithFactory(x => new string("S"))
                .Build();

            var instructions = Builder<string>
                .CreateNew()
                .WithFactory(() => new string("L"))
                .Build();

            var robot = Builder<Robot>
                .CreateNew()
                .With(x => x.XPosition = int.Parse(robotPosition[0]))
                .With(x => x.YPosition = int.Parse(robotPosition[1]))
                .With(x => x.CurrentDirection = (Direction)Enum.Parse(typeof(Direction), robotPosition[2]))
                .Build();

            var instruction = Builder<Instruction>
                .CreateNew()
                .With(x => x.InstructionString = instructions)
                .Build();

            var mockRobotMapper = this.mocker.GetMock<IRobotMapper>();
            var mockInstructionMapper = this.mocker.GetMock<IInstructionMapper>();

            mockRobotMapper.Setup(x => x.Map(It.IsAny<IList<string>>())).Returns(robot);
            mockInstructionMapper.Setup(x => x.Map(It.IsAny<string>())).Returns(instruction);

            var result = this.processingService.ProcessRobot(ref grid, robotPosition, instructions);

            result.Should().NotBeNull();
            result.Should().Be($"1 2 {(Direction)2} ");
            mockRobotMapper.Verify(x => x.Map(It.IsAny<IList<string>>()), Times.Once);
            mockInstructionMapper.Verify(x => x.Map(It.IsAny<string>()), Times.Once);

        }

        [TestMethod]
        public void Given_ProcessingService_When_GridIsNull_Then_ThrowsException()
        {
            Grid grid = null;

            var robotPosition = Builder<string>
                .CreateListOfSize(3)
                .TheFirst(1)
                .WithFactory(x => new string("1"))
                .TheNext(1)
                .WithFactory(x => new string("2"))
                .TheNext(1)
                .WithFactory(x => new string("S"))
                .Build();

            var instructions = Builder<string>
                .CreateNew()
                .WithFactory(() => new string("L"))
                .Build();


            Func<string> action = ()=> this.processingService.ProcessRobot(ref grid, robotPosition, instructions);

            action.Should().ThrowExactly<ArgumentNullException>();

        }

        [TestMethod]
        public void Given_ProcessingService_When_RobotPositionsIsNull_Then_ThrowsException()
        {
            var grid = FizzWare.NBuilder.Builder<Grid>
                .CreateNew()
                .With(x => x.XScale = 1)
                .With(x => x.YScale = 2)
                .Build();

            IList<string> robotPosition = null;

            var instructions = Builder<string>
                .CreateNew()
                .WithFactory(() => new string("L"))
                .Build();


            Func<string> action = () => this.processingService.ProcessRobot(ref grid, robotPosition, instructions);

            action.Should().ThrowExactly<ArgumentNullException>();

        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void Given_ProcessingService_When_InstructionsIsEmpty_Then_ThrowsException(string instructions)
        {
            var grid = FizzWare.NBuilder.Builder<Grid>
                .CreateNew()
                .With(x => x.XScale = 1)
                .With(x => x.YScale = 2)
                .Build();

            var robotPosition = Builder<string>
                .CreateListOfSize(3)
                .TheFirst(1)
                .WithFactory(x => new string("1"))
                .TheNext(1)
                .WithFactory(x => new string("2"))
                .TheNext(1)
                .WithFactory(x => new string("S"))
                .Build();


            Func<string> action = () => this.processingService.ProcessRobot(ref grid, robotPosition, instructions);

            action.Should().Throw<ArgumentException>();

        }



    }
}
