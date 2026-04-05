using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Factories;
using ECS.Interfaces;


namespace ECS.Managers
{
    public class ReportManager
    {
        public string ReportType { get; set; }

        public ReportManager()
        {
            ReportType = string.Empty;
        }

        public void GenerateReport(string reportType)
        {
            IReport report = ReportFactory.CreateReport(reportType);
            report.Generate();
        }

        public void GenerateUsageReport()
        {
            GenerateReport("usage");
        }

        public void GenerateLossReport()
        {
            GenerateReport("loss");
        }

        public void FilterReport(string criteria)
        {
            Console.WriteLine("Filtering report by: " + criteria);
        }
    }
}