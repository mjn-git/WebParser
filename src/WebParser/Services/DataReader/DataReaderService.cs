using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebParser.Services.DataReader
{
    public class DataReaderService : IDataReaderService
    {
        public async Task<string> ReadRequestContentAsync(Stream fileData)
        {
            var content = new StreamContent(fileData);
            var contentString = await content.ReadAsStringAsync();

            return contentString;
        }
    }
}
