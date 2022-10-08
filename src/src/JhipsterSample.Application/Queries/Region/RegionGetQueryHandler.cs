
using AutoMapper;
using System.Linq;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Queries;

public class RegionGetQueryHandler : IRequestHandler<RegionGetQuery, RegionDto>
{
    private IReadOnlyRegionRepository _regionRepository;
    private readonly IMapper _mapper;

    public RegionGetQueryHandler(
        IMapper mapper,
        IReadOnlyRegionRepository regionRepository)
    {
        _mapper = mapper;
        _regionRepository = regionRepository;
    }

    public async Task<RegionDto> Handle(RegionGetQuery request, CancellationToken cancellationToken)
    {
        var entity = await _regionRepository.QueryHelper()
            .GetOneAsync(region => region.Id == request.Id);
        return _mapper.Map<RegionDto>(entity);
    }
}
