
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using MediatR;

namespace JhipsterSample.Application.Queries;

public class RegionGetQuery : IRequest<RegionDto>
{
    public long Id { get; set; }
}
