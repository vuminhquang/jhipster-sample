
using AutoMapper;
using System.Linq;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Commands;

public class JobCreateCommandHandler : IRequestHandler<JobCreateCommand, Job>
{
    private IJobRepository _jobRepository;
    private readonly IMapper _mapper;

    public JobCreateCommandHandler(
        IMapper mapper,
        IJobRepository jobRepository)
    {
        _mapper = mapper;
        _jobRepository = jobRepository;
    }

    public async Task<Job> Handle(JobCreateCommand command, CancellationToken cancellationToken)
    {
        Job job = _mapper.Map<Job>(command.JobDto);
        var entity = await _jobRepository.CreateOrUpdateAsync(job);
        await _jobRepository.SaveChangesAsync();
        return entity;
    }
}
