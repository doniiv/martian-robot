using MartianRobot.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MartianRobot.Helpers
{
    public static class FileHelper
    {
        public static IList<RobotInfo> GetRobotInfos(string filePath, out IList<string> gridScale)
        {
            IList<RobotInfo> robotInfos = new List<RobotInfo>();
            var fileInfo = new FileInfo(filePath);

            using (var stream = fileInfo.OpenText())
            {
                gridScale = stream.ReadLine().Split(' ').ToList();
                string line;
                while ((line = stream.ReadLine()) != null)
                {
                    var robotInfo = new RobotInfo
                    {
                        RobotPosition = line.Split(' ').ToList(),
                        Instructions = (line = stream.ReadLine()),
                    };

                    robotInfos.Add(robotInfo);
                }
            }

            return robotInfos;
        }
    }
}
