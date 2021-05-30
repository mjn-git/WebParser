using System.IO;
using System.Threading.Tasks;

namespace WebParser.Services.DataReader
{
    public interface IDataReaderService
    {
        Task<string> ReadRequestContentAsync(Stream fileData);
    }
}
