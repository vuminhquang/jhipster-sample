
using JhipsterSample.Domain.Entities;

namespace JhipsterSample.Domain.Repositories.Interfaces
{

    public interface IReadOnlyJobHistoryRepository : IReadOnlyGenericRepository<JobHistory, long>
    {
    }

}
