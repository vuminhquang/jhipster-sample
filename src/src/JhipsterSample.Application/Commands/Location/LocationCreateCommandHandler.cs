
using AutoMapper;
using System.Linq;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Commands;

public class LocationCreateCommandHandler : IRequestHandler<LocationCreateCommand, Location>
{
    private ILocationRepository _locationRepository;
    private readonly IMapper _mapper;

    public LocationCreateCommandHandler(
        IMapper mapper,
        ILocationRepository locationRepository)
    {
        _mapper = mapper;
        _locationRepository = locationRepository;
    }

    public async Task<Location> Handle(LocationCreateCommand command, CancellationToken cancellationToken)
    {
        Location location = _mapper.Map<Location>(command.LocationDto);
        var entity = await _locationRepository.CreateOrUpdateAsync(location);
        await _locationRepository.SaveChangesAsync();
        return entity;
    }
}
