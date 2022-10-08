
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using MediatR;

namespace JhipsterSample.Application.Queries;

public class JobGetQuery : IRequest<JobDto>
{
    public long Id { get; set; }
}
