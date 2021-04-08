using MartianRobot.Mappers;
using MartianRobot.Mappers.Implementation;
using MartianRobot.Services;
using MartianRobot.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobot.Main
{
    public static class IOCBindings
    {
        public static void Configure(IServiceCollection services)
        {
            services.TryAddScoped<IProcessingService, ProcessingService>();

            services.TryAddScoped<IGridMapper, GridMapper>();
            services.TryAddScoped<IInstructionMapper, InstructionMapper>();
            services.TryAddScoped<IRobotMapper, RobotMapper>();
        }
    }
}
