
using AutoMapper;
using System.Linq;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Queries;

public class LocationGetQueryHandler : IRequestHandler<LocationGetQuery, LocationDto>
{
    private IReadOnlyLocationRepository _locationRepository;
    private readonly IMapper _mapper;

    public LocationGetQueryHandler(
        IMapper mapper,
        IReadOnlyLocationRepository locationRepository)
    {
        _mapper = mapper;
        _locationRepository = locationRepository;
    }

    public async Task<LocationDto> Handle(LocationGetQuery request, CancellationToken cancellationToken)
    {
        var entity = await _locationRepository.QueryHelper()
            .Include(location => location.Country)
            .GetOneAsync(location => location.Id == request.Id);
        return _mapper.Map<LocationDto>(entity);
    }
}
