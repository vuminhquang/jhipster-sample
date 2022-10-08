
using JhipsterSample.Domain.Entities;

namespace JhipsterSample.Domain.Repositories.Interfaces
{

    public interface IReadOnlyEmployeeRepository : IReadOnlyGenericRepository<Employee, long>
    {
    }

}
