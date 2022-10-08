
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using MediatR;

namespace JhipsterSample.Application.Queries;

public class LocationGetQuery : IRequest<LocationDto>
{
    public long Id { get; set; }
}
