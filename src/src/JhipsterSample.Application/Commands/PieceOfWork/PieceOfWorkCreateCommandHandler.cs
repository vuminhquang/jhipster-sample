
using AutoMapper;
using System.Linq;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Commands;

public class PieceOfWorkCreateCommandHandler : IRequestHandler<PieceOfWorkCreateCommand, PieceOfWork>
{
    private IPieceOfWorkRepository _pieceOfWorkRepository;
    private readonly IMapper _mapper;

    public PieceOfWorkCreateCommandHandler(
        IMapper mapper,
        IPieceOfWorkRepository pieceOfWorkRepository)
    {
        _mapper = mapper;
        _pieceOfWorkRepository = pieceOfWorkRepository;
    }

    public async Task<PieceOfWork> Handle(PieceOfWorkCreateCommand command, CancellationToken cancellationToken)
    {
        PieceOfWork pieceOfWork = _mapper.Map<PieceOfWork>(command.PieceOfWorkDto);
        var entity = await _pieceOfWorkRepository.CreateOrUpdateAsync(pieceOfWork);
        await _pieceOfWorkRepository.SaveChangesAsync();
        return entity;
    }
}
