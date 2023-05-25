using Customer.Service.Queries.DTOs;
using Service.Common.Collection;

namespace Customer.Service.Queries.Interfaces
{
    public interface IClientQueryService
    {
        Task<DataCollection<ClientDto>> GetPagedAsync(int page, int take, IEnumerable<int> clients = null);
        Task<IEnumerable<ClientDto>> GetAllAsync();
        Task<ClientDto> GetAsync(int id);
    }
}
