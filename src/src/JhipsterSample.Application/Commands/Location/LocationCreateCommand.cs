
using JhipsterSample.Domain.Entities;
using MediatR;
using JhipsterSample.Dto;

namespace JhipsterSample.Application.Commands;

public class LocationCreateCommand : IRequest<Location>
{
    public LocationDto LocationDto { get; set; }
}
