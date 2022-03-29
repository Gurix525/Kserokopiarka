using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesGroup
{
    public class Copier : BaseDevice
    {
        private Printer _printer = new Printer();
        private Scanner _scanner = new Scanner();

        public int PrintCounter => _printer.PrintCounter;
        public int ScanCounter => _scanner.ScanCounter;

        public override void PowerOn()
        {
            base.PowerOn();
            _printer.PowerOn();
            _scanner.PowerOn();
            Console.WriteLine("Printer and scanner are on ...");
        }
        public override void PowerOff()
        {
            base.PowerOff();
            _printer.PowerOff();
            _scanner.PowerOff();
            Console.WriteLine("... Printer and scanner are off.");
        }
        public void Print(in IDocument document)
        {
            _printer.Print(document);
        }
        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            _scanner.Scan(out document, formatType);
        }
        public void ScanAndPrint()
        {
            IDocument document = null;
            Scan(out document);
            Print(document);
        }
    }
}