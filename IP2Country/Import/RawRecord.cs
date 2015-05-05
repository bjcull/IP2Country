using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP2Country.Import
{
    public class RawRecord
    {
        public string StartAddress { get; set; }
        public string EndAddress { get; set; }
        public string CountryCode { get; set; }
    }
}
