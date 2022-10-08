
using JhipsterSample.Domain.Entities;

namespace JhipsterSample.Domain.Repositories.Interfaces
{

    public interface IReadOnlyDepartmentRepository : IReadOnlyGenericRepository<Department, long>
    {
    }

}
