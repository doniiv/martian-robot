using EnsureThat;
using MartianRobot.Helpers;
using MartianRobot.Mappers;
using MartianRobot.Models;
using MartianRobot.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MartianRobot.Main
{
    public class Program
    {
        protected static IServiceProvider serviceProvider;
        protected static IServiceCollection serviceCollection { get; } = new ServiceCollection(); 

        public static void Main(string[] args)
        {
            EnsureArg.IsNotNull(args, nameof(args));
            EnsureArg.SizeIs(args, 1, nameof(args));

            var filePath = args[0];
            //var filePath = @"C:\test\test.txt"; - for debugging purpose

            IList<string> gridScale;
            var robotInfos = FileHelper.GetRobotInfos(filePath, out gridScale);

            SetupServiceProvider();

            var processingService = serviceProvider.GetService<IProcessingService>();
            var gridMapper = serviceProvider.GetService<IGridMapper>();

            var grid = gridMapper.Map(gridScale);

            foreach (var robotInfo in robotInfos)
            {
                var result = processingService.ProcessRobot(ref grid, robotInfo.RobotPosition, robotInfo.Instructions);

                Console.WriteLine(result);
            }
        }

        protected static void SetupServiceProvider()
        {
            IOCBindings.Configure(serviceCollection);
            serviceProvider = serviceCollection.BuildServiceProvider();
        }
    }
}
