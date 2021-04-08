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
    public class RobotMapperTest
    {
        private readonly RobotMapper mapper;

        public RobotMapperTest()
        {
            mapper = new RobotMapper();
        }

        [TestMethod]
        public void Given_MappingFrom_RobotPosition_When_MappingTo_Robot_Then_FieldsAreMapped()
        {
            var robotPosition = Builder<string>
                    .CreateListOfSize(3)
                    .TheFirst(1)
                    .WithFactory(x => new string("1"))
                    .TheNext(1)
                    .WithFactory(x => new string("2"))
                    .TheNext(1)
                    .WithFactory(x => new string("3"))
                    .Build();

            var result = mapper.Map(robotPosition);

            result.Should().NotBeNull();
            result.XPosition.Should().Be(int.Parse(robotPosition[0]));
            result.YPosition.Should().Be(int.Parse(robotPosition[1]));
        }


        [TestMethod]
        public void Given_MappingFrom_RobotPosition_When_MappingTo_Robot_AndRobotPositionIsNull_Then_ArgumentNullExceptionIsThrown()
        {

           Func<Robot> func = () => mapper.Map(null);

           func.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Given_MappingFrom_RobotPosition_When_MappingTo_Robot_AndRobotPositionIsEmpty_Then_ArgumentExceptionIsThrown()
        {

            Func<Robot> func = () => mapper.Map(new List<string>());

            func.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void Given_MappingFrom_RobotPosition_When_MappingTo_Robot_AndRobotPositionSizeIsLessThanThree_Then_ArgumentExceptionIsThrown()
        {
            var robotPosition = Builder<string>
                .CreateListOfSize(2)
                .All()
                .WithFactory(x => new string("1"))
                .Build();

            Func<Robot> func = () => mapper.Map(robotPosition);

            func.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void Given_MappingFrom_RobotPosition_When_MappingTo_Robot_AndRobotPositionSizeIsMoreThanThree_Then_ArgumentExceptionIsThrown()
        {
            var robotPosition = Builder<string>
                .CreateListOfSize(4)
                .All()
                .WithFactory(x => new string("1"))
                .Build();

            Func<Robot> func = () => mapper.Map(robotPosition);

            func.Should().Throw<ArgumentException>();
        }
    }
}
