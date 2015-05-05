using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace IP2Country.Import
{
    public class AzureStorageImporter : IRecordImporter
    {
        private readonly CloudStorageAccount _account;
        private readonly string _containerName;
        private readonly string _blobName;

        public AzureStorageImporter(CloudStorageAccount account, string containerName, string blobName)
        {
            _account = account;
            _containerName = containerName;
            _blobName = blobName;
        }

        public async Task<IEnumerable<RawRecord>> GetRecords()
        {
            // Create the blob client.
            CloudBlobClient blobClient = _account.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(_containerName);

            // Retrieve reference to a blob named "photo1.jpg".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(_blobName);

            var list = new List<RawRecord>();
            using (var reader = new StreamReader(blockBlob.OpenRead()))
            {
                var csv = new CsvReader(reader);
                while (csv.Read())
                {
                    list.Add(new RawRecord()
                    {
                        StartAddress = csv.GetField(0),
                        EndAddress = csv.GetField(1),
                        CountryCode = csv.GetField(2)
                    });
                }
            }

            return list;
        }
    }
}
