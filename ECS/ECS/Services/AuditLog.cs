using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ECS.Services
{
    public class AuditLog
    {
        public int LogID { get; set; }
        public DateTime Timestamp { get; set; }
        public string ActionType { get; set; }
        public int UserID { get; set; }

        public AuditLog()
        {
            ActionType = string.Empty;
        }

        public void RecordAction(string action)
        {
            Timestamp = DateTime.Now;
            ActionType = action;
            DatabaseManager.Instance.AuditEntries.Add(Timestamp.ToString("u") + " - " + action);
            Console.WriteLine("Audit log recorded: " + action);
        }

        public void ViewLog()
        {
            Console.WriteLine("Audit Log:");
            foreach (string entry in DatabaseManager.Instance.AuditEntries)
            {
                Console.WriteLine(entry);
            }
        }
    }
}