using System.Threading.Tasks;

namespace MobileApp.Services.RequestProvider
{
    public interface IRequestProviderService
    {
        Task<TResult> GetAsync<TResult>(string uri);

        Task<TResult> PostAsync<TResult>(string uri, TResult data, string header = "");

        Task<TResult> PostAsync<TInput, TResult>(string uri, TInput data, string header = "");
        Task<bool> PostLocationAsync<TInput>(string uri, TInput data, string header = "");

        Task<TResult> PutAsync<TResult>(string uri, TResult data, string header = "");

        Task DeleteAsync(string uri);
    }
}