using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Services
{
    public class BarcodeScanner
    {
        public int ScannerID { get; set; }
        public string ScannerStatus { get; set; }

        public BarcodeScanner()
        {
            ScannerStatus = "Ready";
        }

        public string ScanBarcode(string barcode)
        {
            Console.WriteLine("Scanning barcode: " + barcode);
            return barcode;
        }

        public string ReturnToolID(string barcode)
        {
            Console.WriteLine("Barcode matched to tool ID: " + barcode);
            return barcode;
        }
    }
}