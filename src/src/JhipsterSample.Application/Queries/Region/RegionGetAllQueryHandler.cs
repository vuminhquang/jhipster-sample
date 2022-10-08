
using AutoMapper;
using System.Linq;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;

namespace JhipsterSample.Application.Queries;

public class RegionGetAllQueryHandler : IRequestHandler<RegionGetAllQuery, Page<RegionDto>>
{
    private IReadOnlyRegionRepository _regionRepository;
    private readonly IMapper _mapper;

    public RegionGetAllQueryHandler(
        IMapper mapper,
        IReadOnlyRegionRepository regionRepository)
    {
        _mapper = mapper;
        _regionRepository = regionRepository;
    }

    public async Task<Page<RegionDto>> Handle(RegionGetAllQuery request, CancellationToken cancellationToken)
    {
        var page = await _regionRepository.QueryHelper()
            .GetPageAsync(request.Page);
        return new Page<RegionDto>(page.Content.Select(entity => _mapper.Map<RegionDto>(entity)).ToList(), request.Page, page.TotalElements);
    }
}
