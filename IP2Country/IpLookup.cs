using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.BulkInsert.Extensions;
using IP2Country.Import;

namespace IP2Country
{
    public class IpLookup
    {
        private static IpLookup _ipLookup;
        private static string _databaseConnectionName;

        public static IpLookup Current
        {
            get { return _ipLookup; }
        }

        public static async void Initialize(IRecordImporter importer, string databaseConnectionName = "DefaultConnection")
        {
            _databaseConnectionName = databaseConnectionName;
            _ipLookup = new IpLookup();

            if (!_ipLookup.DoesDatabaseExist())
            {
                var records = await importer.GetRecords();
                var convertedRecords = _ipLookup.ConvertToBytes(records.ToList()).ToList();

                using (var db = new IpDbContext(_databaseConnectionName))
                {
                    db.BulkInsert(convertedRecords);
                }
            }
        }

        public async Task<string> GetCountryFromIp(string ipAddress)
        {
            var ip = IPAddress.Parse(ipAddress).MapToIPv6();
            using (var db = new IpDbContext(_databaseConnectionName))
            {
                var record = await db.IpRecords.SqlQuery("SELECT TOP 1 * FROM IpRecords WHERE StartAddress <= @p0 ORDER BY StartAddress DESC", ip.GetAddressBytes()).FirstOrDefaultAsync();

                if (record == null)
                {
                    return null;
                }

                return record.CountryCode;
            }
        }

        private bool DoesDatabaseExist()
        {
            using (var db = new IpDbContext(_databaseConnectionName))
            {
                try
                {
                    var records = db.IpRecords.Count();
                    return records > 0;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        private IEnumerable<IpRecord> ConvertToBytes(IEnumerable<RawRecord> rawRecords)
        {
            return rawRecords.Select(ConvertToBytes).ToList();
        }

        private IpRecord ConvertToBytes(RawRecord rawRecord)
        {
            var ipRecord = new IpRecord
            {
                StartAddress = IPAddress.Parse(rawRecord.StartAddress).MapToIPv6().GetAddressBytes(),
                EndAddress = IPAddress.Parse(rawRecord.EndAddress).MapToIPv6().GetAddressBytes(),
                CountryCode = rawRecord.CountryCode
            };

            return ipRecord;
        }
    }
}
