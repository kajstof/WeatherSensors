using System.Threading.Tasks;
using WeatherSensors.Api.Models;

namespace WeatherSensors.Api.Services.Interfaces
{
    public interface IBlobDownloaderService
    {
        Task Download(string fileName, BlobInfo blobInfo);
    }
}
