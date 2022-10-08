
using JhipsterSample.Domain.Entities;
using MediatR;
using JhipsterSample.Dto;

namespace JhipsterSample.Application.Commands;

public class RegionUpdateCommand : IRequest<Region>
{
    public RegionDto RegionDto { get; set; }
}
