using FizzWare.NBuilder;
using FluentAssertions;
using MartianRobot.Mappers.Implementation;
using MartianRobot.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobot.UnitTests.Mappers
{
    [TestClass]
    public class GridMapperTests
    {

        private readonly GridMapper mapper;

        public GridMapperTests()
        {
            mapper = new GridMapper();
        }

        [TestMethod]
        public void Given_MappingFrom_GridScale_When_MappingTo_Grid_Then_FieldsAreMapped()
        {
            var gridScale = Builder<string>
                    .CreateListOfSize(2)
                    .TheFirst(1)
                    .WithFactory(x => new string("1"))
                    .TheNext(1)
                    .WithFactory(x => new string("2"))
                    .Build();

            var result = mapper.Map(gridScale);

            result.Should().NotBeNull();
            result.XScale.Should().Be(int.Parse(gridScale[0]));
            result.YScale.Should().Be(int.Parse(gridScale[1]));
        }


        [TestMethod]
        public void Given_MappingFrom_GridScale_When_MappingTo_Grid_AndGridScaleIsNull_Then_ArgumentNullExceptionIsThrown()
        {

            Func<Grid> func = () => mapper.Map(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Given_MappingFrom_GridScale_When_MappingTo_Grid_AndGridScaleIsEmpty_Then_ArgumentExceptionIsThrown()
        {

            Func<Grid> func = () => mapper.Map(new List<string>());

            func.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void Given_MappingFrom_GridScale_When_MappingTo_Grid_AndGridScaleSizeIsLessThanThree_Then_ArgumentExceptionIsThrown()
        {
            var gridScale = Builder<string>
                .CreateListOfSize(1)
                .All()
                .WithFactory(x => new string("1"))
                .Build();

            Func<Grid> func = () => mapper.Map(gridScale);

            func.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void Given_MappingFrom_GridScale_When_MappingTo_Grid_AndGridScaleSizeIsMoreThanThree_Then_ArgumentExceptionIsThrown()
        {
            var gridScale = Builder<string>
                .CreateListOfSize(3)
                .All()
                .WithFactory(x => new string("1"))
                .Build();

            Func<Grid> func = () => mapper.Map(gridScale);

            func.Should().Throw<ArgumentException>();
        }
    }
}
