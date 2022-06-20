using Microsoft.Azure.Cosmos;
using PaymentService.Entities;

namespace PaymentService.Repositories.Interface
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<ItemResponse<T>> Create(T t);
        Task<ItemResponse<T>> Update(string id, T t);
    }
}
