using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP2Country.Import
{
    public interface IRecordImporter
    {
        Task<IEnumerable<RawRecord>> GetRecords();
    }
}
