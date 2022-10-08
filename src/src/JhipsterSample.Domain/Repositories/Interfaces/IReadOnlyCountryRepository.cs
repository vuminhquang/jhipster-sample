
using JhipsterSample.Domain.Entities;

namespace JhipsterSample.Domain.Repositories.Interfaces
{

    public interface IReadOnlyCountryRepository : IReadOnlyGenericRepository<Country, long>
    {
    }

}
