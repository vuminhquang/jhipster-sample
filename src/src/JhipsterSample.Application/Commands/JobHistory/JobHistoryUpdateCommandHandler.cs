
using AutoMapper;
using System.Linq;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Commands;

public class JobHistoryUpdateCommandHandler : IRequestHandler<JobHistoryUpdateCommand, JobHistory>
{
    private IJobHistoryRepository _jobHistoryRepository;
    private readonly IMapper _mapper;

    public JobHistoryUpdateCommandHandler(
        IMapper mapper,
        IJobHistoryRepository jobHistoryRepository)
    {
        _mapper = mapper;
        _jobHistoryRepository = jobHistoryRepository;
    }

    public async Task<JobHistory> Handle(JobHistoryUpdateCommand command, CancellationToken cancellationToken)
    {
        JobHistory jobHistory = _mapper.Map<JobHistory>(command.JobHistoryDto);
        var entity = await _jobHistoryRepository.CreateOrUpdateAsync(jobHistory);
        await _jobHistoryRepository.SaveChangesAsync();
        return entity;
    }
}
