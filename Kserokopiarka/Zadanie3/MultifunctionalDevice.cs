using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesGroup
{
    public class MultifunctionalDevice : Copier
    {
        private Fax _fax = new Fax();
        public int FaxCounter => _fax.FaxCounter;
        public void SendFax()
        {
            _fax.SendFax("123-456-7890");
        }
    }
}
