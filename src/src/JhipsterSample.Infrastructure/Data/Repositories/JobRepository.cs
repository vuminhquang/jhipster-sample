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
    public class JobRepository : GenericRepository<Job, long>, IJobRepository
    {
        public JobRepository(IUnitOfWork context) : base(context)
        {
        }

        public override async Task<Job> CreateOrUpdateAsync(Job job)
        {
            List<Type> entitiesToBeUpdated = new List<Type>();

            await RemoveManyToManyRelationship("JobChores", "JobsId", "ChoresId", job.Id, job.Chores.Select(x => x.Id).ToList());
            entitiesToBeUpdated.Add(typeof(PieceOfWork));
            return await base.CreateOrUpdateAsync(job, entitiesToBeUpdated);
        }
    }
}
