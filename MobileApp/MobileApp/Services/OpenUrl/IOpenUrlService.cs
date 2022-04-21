using System.Threading.Tasks;

namespace MobileApp.Services.OpenUrl
{
    public interface IOpenUrlService
    {
        Task OpenUrl(string url);
    }
}