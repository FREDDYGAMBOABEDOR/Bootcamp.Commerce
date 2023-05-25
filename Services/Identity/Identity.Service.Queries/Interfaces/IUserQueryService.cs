using Identity.Service.Queries.DTOs;
using Service.Common.Collection;

namespace Identity.Service.Queries.Interfaces
{
    public interface IUserQueryService
    {
        Task<DataCollection<UserDto>> GetPagedAsync(int page, int take, IEnumerable<int> users = null);
        Task<UserDto> GetAsync(int id);
    }
}
