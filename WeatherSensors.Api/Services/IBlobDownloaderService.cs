using System.Threading.Tasks;
using WeatherSensors.Api.Models;

namespace WeatherSensors.Api.Services
{
    public interface IBlobDownloaderService
    {
        Task Download(string fileName, BlobInfo blobInfo);
    }
}
