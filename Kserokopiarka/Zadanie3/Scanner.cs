using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesGroup
{
    internal class Scanner : BaseDevice, IScanner
    {
        public int ScanCounter { get; set; } = 0;
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
    }
}
