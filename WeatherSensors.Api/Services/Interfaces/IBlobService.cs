using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherSensors.Api.Models;

namespace WeatherSensors.Api.Services.Interfaces
{
    public interface IBlobService
    {
        Task<BlobInfo> GetBlobAsync(string name);
        Task<IEnumerable<string>> ListBlobsAsync();
    }
}
