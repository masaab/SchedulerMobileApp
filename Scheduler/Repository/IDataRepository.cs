using System.Threading.Tasks;

namespace Scheduler.Repository
{
    public interface IDataRepository
    {
        Task<T> GetAsync<T>(string uri, string authToken = "");
        Task<T> PostAsync<T>(string uri, T data, string authToken = "");
        Task<T> PutAsync<T>(string uri, T data, string authToken = "");
        Task DeleteAsync(string uri, string authToken = "");
        Task<R> PostAsync<T, R>(string uri, T data, R returnValue, string authToken = "");
    }
}
