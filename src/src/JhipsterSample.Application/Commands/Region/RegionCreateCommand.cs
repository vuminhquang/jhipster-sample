
using JhipsterSample.Domain.Entities;
using MediatR;
using JhipsterSample.Dto;

namespace JhipsterSample.Application.Commands;

public class RegionCreateCommand : IRequest<Region>
{
    public RegionDto RegionDto { get; set; }
}
