
using AutoMapper;
using System.Linq;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;

namespace JhipsterSample.Application.Queries;

public class JobGetAllQueryHandler : IRequestHandler<JobGetAllQuery, Page<JobDto>>
{
    private IReadOnlyJobRepository _jobRepository;
    private readonly IMapper _mapper;

    public JobGetAllQueryHandler(
        IMapper mapper,
        IReadOnlyJobRepository jobRepository)
    {
        _mapper = mapper;
        _jobRepository = jobRepository;
    }

    public async Task<Page<JobDto>> Handle(JobGetAllQuery request, CancellationToken cancellationToken)
    {
        var page = await _jobRepository.QueryHelper()
            .Include(job => job.Chores)
            .Include(job => job.Employee)
            .GetPageAsync(request.Page);
        return new Page<JobDto>(page.Content.Select(entity => _mapper.Map<JobDto>(entity)).ToList(), request.Page, page.TotalElements);
    }
}
