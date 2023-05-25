using Microsoft.EntityFrameworkCore;
using Order.Persistence.Database;
using Order.Service.Queries.DTOs;
using Order.Service.Queries.Interfaces;
using Service.Common.Paging;
using Service.Common.Mapping;
using Service.Common.Collection;

namespace Order.Service.Queries.Services
{
    public class OrderQueryService : IOrderQueryService
    {
        private readonly OrderDbContext _dbContext;

        public OrderQueryService(OrderDbContext dbContext) { _dbContext = dbContext; }

        public async Task<DataCollection<OrderDto>> GetPagedAsync(int page, int take)
        {
            var collection = await _dbContext.Orders
               .Include(i => i.Items)
               .OrderBy(or => or.OrderId)
               .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<OrderDto>>();
        }

        public async Task<OrderDto> GetAsync(int id)
        {
            var entity = await _dbContext.Orders.Include(i => i.Items).FirstOrDefaultAsync(x => x.OrderId == id);
            return entity.MapTo<OrderDto>();
        }
    }
}
