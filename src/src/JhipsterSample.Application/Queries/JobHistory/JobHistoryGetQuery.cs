
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using MediatR;

namespace JhipsterSample.Application.Queries;

public class JobHistoryGetQuery : IRequest<JobHistoryDto>
{
    public long Id { get; set; }
}
