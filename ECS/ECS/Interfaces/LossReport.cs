using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Interfaces;

namespace ECS.Reports
{
    public class LossReport : IReport
    {
        public void Generate()
        {
            Console.WriteLine("Generating equipment loss report...");
        }
    }
}