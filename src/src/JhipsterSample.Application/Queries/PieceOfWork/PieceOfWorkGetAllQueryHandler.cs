
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

public class PieceOfWorkGetAllQueryHandler : IRequestHandler<PieceOfWorkGetAllQuery, Page<PieceOfWorkDto>>
{
    private IReadOnlyPieceOfWorkRepository _pieceOfWorkRepository;
    private readonly IMapper _mapper;

    public PieceOfWorkGetAllQueryHandler(
        IMapper mapper,
        IReadOnlyPieceOfWorkRepository pieceOfWorkRepository)
    {
        _mapper = mapper;
        _pieceOfWorkRepository = pieceOfWorkRepository;
    }

    public async Task<Page<PieceOfWorkDto>> Handle(PieceOfWorkGetAllQuery request, CancellationToken cancellationToken)
    {
        var page = await _pieceOfWorkRepository.QueryHelper()
            .GetPageAsync(request.Page);
        return new Page<PieceOfWorkDto>(page.Content.Select(entity => _mapper.Map<PieceOfWorkDto>(entity)).ToList(), request.Page, page.TotalElements);
    }
}
