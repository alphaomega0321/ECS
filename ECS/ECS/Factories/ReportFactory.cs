using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Interfaces;
using ECS.Reports;

namespace ECS.Factories
{
    public static class ReportFactory
    {
        public static IReport CreateReport(string reportType)
        {
            switch (reportType.ToLower())
            {
                case "usage":
                    return new UsageReport();
                case "loss":
                    return new LossReport();
                case "reservation":
                    return new ReservationReport();
                default:
                    throw new ArgumentException("Invalid report type.");
            }
        }
    }
}