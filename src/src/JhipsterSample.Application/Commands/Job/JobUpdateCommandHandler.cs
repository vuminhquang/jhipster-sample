
using AutoMapper;
using System.Linq;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Commands;

public class JobUpdateCommandHandler : IRequestHandler<JobUpdateCommand, Job>
{
    private IJobRepository _jobRepository;
    private readonly IMapper _mapper;

    public JobUpdateCommandHandler(
        IMapper mapper,
        IJobRepository jobRepository)
    {
        _mapper = mapper;
        _jobRepository = jobRepository;
    }

    public async Task<Job> Handle(JobUpdateCommand command, CancellationToken cancellationToken)
    {
        Job job = _mapper.Map<Job>(command.JobDto);
        var entity = await _jobRepository.CreateOrUpdateAsync(job);
        await _jobRepository.SaveChangesAsync();
        return entity;
    }
}
