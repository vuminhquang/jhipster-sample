
using JhipsterSample.Domain.Entities;

namespace JhipsterSample.Domain.Repositories.Interfaces
{

    public interface IReadOnlyJobRepository : IReadOnlyGenericRepository<Job, long>
    {
    }

}
