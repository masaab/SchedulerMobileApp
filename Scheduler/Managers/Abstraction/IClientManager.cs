using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Managers.Abstraction
{
    public interface IClientManager
    {
        Task<Client> AddClient(Client client);

        Task<IEnumerable<Client>> GetAllClient();

        Task DeleteClient(string Id);

        Task<Client> GetSingleClient(string Id);
    }
}
