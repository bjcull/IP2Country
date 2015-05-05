using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP2Country
{
    public class IpRecord
    {
        [Key]
        [MaxLength(16)]
        public byte[] StartAddress { get; set; }

        [MaxLength(16)]
        public byte[] EndAddress { get; set; }

        [MaxLength(2)]
        public string CountryCode { get; set; }
    }
}
