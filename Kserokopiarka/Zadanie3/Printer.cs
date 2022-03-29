using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesGroup
{
    public class Printer : BaseDevice, IPrinter
    {
        public int PrintCounter { get; set; } = 0;
        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                PrintCounter++;
                Console.WriteLine(DateTime.Now.ToString() + " Print: " + document.GetFileName());
            }
        }
    }
}
