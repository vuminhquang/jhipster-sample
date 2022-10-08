
using AutoMapper;
using System.Linq;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Commands;

public class JobHistoryCreateCommandHandler : IRequestHandler<JobHistoryCreateCommand, JobHistory>
{
    private IJobHistoryRepository _jobHistoryRepository;
    private readonly IMapper _mapper;

    public JobHistoryCreateCommandHandler(
        IMapper mapper,
        IJobHistoryRepository jobHistoryRepository)
    {
        _mapper = mapper;
        _jobHistoryRepository = jobHistoryRepository;
    }

    public async Task<JobHistory> Handle(JobHistoryCreateCommand command, CancellationToken cancellationToken)
    {
        JobHistory jobHistory = _mapper.Map<JobHistory>(command.JobHistoryDto);
        var entity = await _jobHistoryRepository.CreateOrUpdateAsync(jobHistory);
        await _jobHistoryRepository.SaveChangesAsync();
        return entity;
    }
}
