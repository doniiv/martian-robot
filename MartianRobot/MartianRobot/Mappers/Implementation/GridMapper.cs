using EnsureThat;
using FluentValidation;
using MartianRobot.Models;
using MartianRobot.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobot.Mappers.Implementation
{
    public class GridMapper : IGridMapper
    {
        public Grid Map(IList<string> gridScale)
        {
            EnsureArg.IsNotNull(gridScale, nameof(gridScale));
            EnsureArg.SizeIs(gridScale, 2, nameof(gridScale));

            var grid = new Grid
            {
                XScale = int.Parse(gridScale[0]),
                YScale = int.Parse(gridScale[1])
            };

            var gridValidator = new GridValidator();
            gridValidator.ValidateAndThrow(grid);

            return grid;
        }
    }
}
