using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ECS.Services
{
    public class AuditLog
    {
        /// <summary>
        /// Records a system action into the audit log.
        /// Each entry includes a timestamp and a descriptive message.
        /// </summary>
        public void RecordAction(string action)
        {
            // Create timestamped log entry for traceability
            string logEntry = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + action;

            // Store log entry in the simulated database
            DatabaseManager.Instance.AuditEntries.Add(logEntry);

            // Provide immediate feedback in console
            Console.WriteLine("Audit Log Entry Recorded.");
        }

        /// <summary>
        /// Displays all audit log entries stored in the system.
        /// Used for monitoring system activity and accountability.
        /// </summary>
        public void ViewLog()
        {
            // Check if there are any audit entries to display
            if (DatabaseManager.Instance.AuditEntries.Count == 0)
            {
                Console.WriteLine("No audit log entries found.");
                return;
            }

            // Display each recorded audit entry
            foreach (string entry in DatabaseManager.Instance.AuditEntries)
            {
                Console.WriteLine(entry);
            }
        }
    }
}