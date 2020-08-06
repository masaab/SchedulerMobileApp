using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Scheduler.AppSetting;
using Scheduler.Managers.Abstraction;
using Scheduler.Models;
using Scheduler.Repository;

namespace Scheduler.Managers
{
    public class ClientManager : IClientManager
    {
        private IDataRepository DataRepository { get; }
        private AppSettingDetail AppSettingDetails { get; }
        public ClientManager(IDataRepository dataRepository, IApplicationSettingManager applicationSettingManager)
        {
            DataRepository = dataRepository;
            AppSettingDetails = applicationSettingManager.AppSettingDetails;
        }
        public async Task<Client> AddClient(Client client)
        {
            var urlPath = GetUriBuilder(AppSettingDetails.ClientBaseUrl);
            await DataRepository.PostAsync($"{urlPath}", client);
            return client;
        }

        public async Task DeleteClient(string Id)
        {
            var urlPath = GetUriBuilder(AppSettingDetails.ClientBaseUrl);

            await DataRepository.DeleteAsync($"{urlPath}/{Id}");
        }
        public Task<IEnumerable<Client>> GetAllClient() 
        {
            var urlPath = GetUriBuilder(AppSettingDetails.ClientBaseUrl);

            var data = DataRepository.GetAsync<IEnumerable<Client>>(urlPath);

            return data;
        }

        public Task<Client> GetSingleClient(string Id)
        {
            throw new NotImplementedException();
        }

        private string GetUriBuilder(string path)
            => new UriBuilder(AppSettingDetails.BackendUrl) { Path = path }.ToString();
    }
}
