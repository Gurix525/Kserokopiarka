using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesGroup
{
    internal class Fax : Scanner, IFax
    {
        public int FaxCounter { get; set; } = 0;
        public void SendFax(in string adress)
        {
            if (state == IDevice.State.on)
            {
                IDocument document = null;
                Scan(out document);
                Console.WriteLine(DateTime.Now.ToString() + " Fax to " + adress + ": " + document.GetFileName());
            }
        }
    }
}
