using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesGroup
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set; } = 0;
        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                PrintCounter++;
                Console.WriteLine(DateTime.Now.ToString() + " Print: " + document.GetFileName());
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            if (state == IDevice.State.on)
            {
                ScanCounter++;
                switch (formatType)
                {
                    case IDocument.FormatType.TXT:
                        document = new TextDocument("TextScan" + ScanCounter + ".txt");
                        break;
                    case IDocument.FormatType.JPG:
                        document = new ImageDocument("ImageScan" + ScanCounter + ".jpg");
                        break;
                    case IDocument.FormatType.PDF:
                        document = new PDFDocument("PDFScan" + ScanCounter + ".pdf");
                        break;
                    default: throw new ArgumentException("Format type must be one of the following: TXT/JPG/PDF");
                }
                Console.WriteLine(DateTime.Now.ToString() + " Scan: " + document.GetFileName());
            }
            else document = null;
        }
        
        public void ScanAndPrint()
        {
            IDocument document = null;
            Scan(out document);
            Print(document);
        }
    }
}
