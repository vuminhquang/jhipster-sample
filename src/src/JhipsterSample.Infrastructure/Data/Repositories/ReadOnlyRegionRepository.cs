using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JHipsterNet.Core.Pagination;
using JHipsterNet.Core.Pagination.Extensions;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Domain.Repositories.Interfaces;
using JhipsterSample.Infrastructure.Data.Extensions;

namespace JhipsterSample.Infrastructure.Data.Repositories
{
    public class ReadOnlyRegionRepository : ReadOnlyGenericRepository<Region, long>, IReadOnlyRegionRepository
    {
        public ReadOnlyRegionRepository(IUnitOfWork context) : base(context)
        {
        }
    }
}
