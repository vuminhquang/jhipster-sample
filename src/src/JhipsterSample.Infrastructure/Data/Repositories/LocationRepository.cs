using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JHipsterNet.Core.Pagination;
using JHipsterNet.Core.Pagination.Extensions;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Domain.Repositories.Interfaces;
using JhipsterSample.Infrastructure.Data.Extensions;

namespace JhipsterSample.Infrastructure.Data.Repositories
{
    public class LocationRepository : GenericRepository<Location, long>, ILocationRepository
    {
        public LocationRepository(IUnitOfWork context) : base(context)
        {
        }

        public override async Task<Location> CreateOrUpdateAsync(Location location)
        {
            List<Type> entitiesToBeUpdated = new List<Type>();
            return await base.CreateOrUpdateAsync(location, entitiesToBeUpdated);
        }
    }
}
