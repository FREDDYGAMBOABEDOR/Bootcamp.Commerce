using Identity.Persistence.Database;
using Identity.Service.Queries.DTOs;
using Identity.Service.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;
using Service.Common.Paging;
using Service.Common.Mapping;
using Service.Common.Collection;

namespace Identity.Service.Queries.Services
{
    public class UserQueryService : IUserQueryService
    {
        private readonly IdentityDBContext _dbContext; 
        public UserQueryService(IdentityDBContext dbContext) { _dbContext = dbContext; }

        public async Task<DataCollection<UserDto>> GetPagedAsync(int page, int take, IEnumerable<int> users = null)
        {
            var collection = await _dbContext.Users
                .Where(x => users == null || users.Contains(x.Id))
                .OrderBy(or => or.UserName)
                .GetPagedAsync(page, take);

            return collection.MapTo<DataCollection<UserDto>>();
        }

        public async Task<UserDto> GetAsync(int id)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            return entity.MapTo<UserDto>();
        }
    }
}
