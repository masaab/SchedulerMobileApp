using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduler.Constants;
using Scheduler.Managers.Abstraction;
using Scheduler.Models;
using Scheduler.Repository;

namespace Scheduler.Managers
{
    public class ClientManager : IClientManager
    {
        private IDataRepository DataRepository { get; }
        public ClientManager(IDataRepository dataRepository)
        {
            DataRepository = dataRepository;
        }
        public async Task<Client> AddClient(Client client)
        {
            var urlPath = GetUriBuilder(ApiConstants.SchedulerBaseUrl);
            await DataRepository.PostAsync($"{urlPath}", client);
            return client;
        }

        public async Task DeleteClient(string Id)
        {
            var urlPath = GetUriBuilder(ApiConstants.SchedulerBaseUrl);

            await DataRepository.DeleteAsync($"{urlPath}/{Id}");
        }
        public Task<IEnumerable<Client>> GetAllClient() 
        {
            var urlPath = GetUriBuilder(ApiConstants.SchedulerBaseUrl);

            var data = DataRepository.GetAsync<IEnumerable<Client>>(urlPath);

            return data;
        }

        public Task<Client> GetSingleClient(string Id)
        {
            throw new NotImplementedException();
        }

        private string GetUriBuilder(string path)
            => new UriBuilder(ApiConstants.BaseApiUrl) { Path = path }.ToString();
    }
}
