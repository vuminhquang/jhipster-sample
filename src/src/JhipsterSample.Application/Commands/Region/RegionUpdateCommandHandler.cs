
using AutoMapper;
using System.Linq;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Commands;

public class RegionUpdateCommandHandler : IRequestHandler<RegionUpdateCommand, Region>
{
    private IRegionRepository _regionRepository;
    private readonly IMapper _mapper;

    public RegionUpdateCommandHandler(
        IMapper mapper,
        IRegionRepository regionRepository)
    {
        _mapper = mapper;
        _regionRepository = regionRepository;
    }

    public async Task<Region> Handle(RegionUpdateCommand command, CancellationToken cancellationToken)
    {
        Region region = _mapper.Map<Region>(command.RegionDto);
        var entity = await _regionRepository.CreateOrUpdateAsync(region);
        await _regionRepository.SaveChangesAsync();
        return entity;
    }
}
