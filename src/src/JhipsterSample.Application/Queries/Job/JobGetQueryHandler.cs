
using AutoMapper;
using System.Linq;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Queries;

public class JobGetQueryHandler : IRequestHandler<JobGetQuery, JobDto>
{
    private IReadOnlyJobRepository _jobRepository;
    private readonly IMapper _mapper;

    public JobGetQueryHandler(
        IMapper mapper,
        IReadOnlyJobRepository jobRepository)
    {
        _mapper = mapper;
        _jobRepository = jobRepository;
    }

    public async Task<JobDto> Handle(JobGetQuery request, CancellationToken cancellationToken)
    {
        var entity = await _jobRepository.QueryHelper()
            .Include(job => job.Chores)
            .Include(job => job.Employee)
            .GetOneAsync(job => job.Id == request.Id);
        return _mapper.Map<JobDto>(entity);
    }
}
