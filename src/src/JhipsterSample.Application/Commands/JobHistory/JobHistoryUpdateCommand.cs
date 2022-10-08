
using JhipsterSample.Domain.Entities;
using MediatR;
using JhipsterSample.Dto;

namespace JhipsterSample.Application.Commands;

public class JobHistoryUpdateCommand : IRequest<JobHistory>
{
    public JobHistoryDto JobHistoryDto { get; set; }
}
