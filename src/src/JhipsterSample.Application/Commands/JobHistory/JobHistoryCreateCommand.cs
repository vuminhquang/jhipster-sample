
using JhipsterSample.Domain.Entities;
using MediatR;
using JhipsterSample.Dto;

namespace JhipsterSample.Application.Commands;

public class JobHistoryCreateCommand : IRequest<JobHistory>
{
    public JobHistoryDto JobHistoryDto { get; set; }
}
