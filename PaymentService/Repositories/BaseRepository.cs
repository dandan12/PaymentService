using Microsoft.Azure.Cosmos;
using System.Security.Claims;
using PaymentService.Contexts;
using PaymentService.Entities;
using PaymentService.Utils.Extensions;

namespace PaymentService.Repositories
{
    public class BaseRepository<T> where T : BaseEntity
    {
        private readonly CosmosDbContext _context;
        protected ClaimsPrincipal User;
        public BaseRepository(CosmosDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            User = httpContextAccessor.HttpContext.User;
        }

        protected string PartnerId => User.GetPartnerId();
        public Container Container => _context.Get<T>();

        public Task<ItemResponse<T>> Get(string id)
        {
            return Container.ReadItemAsync<T>(id, new PartitionKey(id));
        }

        public virtual Task<ItemResponse<T>> Create(T t)
        {
            t.Id = Guid.NewGuid().ToString();
            t.CreatedDate = DateTime.Now;
            t.ModifiedDate = DateTime.Now;

            return Container.CreateItemAsync(t, new PartitionKey(t.Id));
        }

        public virtual Task<ItemResponse<T>> Update(string id, T t)
        {
            t.ModifiedDate = DateTime.Now;
            return Container.ReplaceItemAsync(t, id, new PartitionKey(id));
        }

        public IOrderedQueryable<T> Linq()
        {
            return Container.GetItemLinqQueryable<T>(true);
        }
    }
}
