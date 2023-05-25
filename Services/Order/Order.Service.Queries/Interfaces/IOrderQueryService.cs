using Order.Service.Queries.DTOs;
using Service.Common.Collection;

namespace Order.Service.Queries.Interfaces
{
    public interface IOrderQueryService
    {
        Task<DataCollection<OrderDto>> GetPagedAsync(int page, int take);
        Task<OrderDto> GetAsync(int id);
    }
}
