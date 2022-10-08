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
    public class DepartmentRepository : GenericRepository<Department, long>, IDepartmentRepository
    {
        public DepartmentRepository(IUnitOfWork context) : base(context)
        {
        }

        public override async Task<Department> CreateOrUpdateAsync(Department department)
        {
            List<Type> entitiesToBeUpdated = new List<Type>();
            return await base.CreateOrUpdateAsync(department, entitiesToBeUpdated);
        }
    }
}
