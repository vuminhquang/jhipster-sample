
using JhipsterSample.Domain.Entities;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Commands;

public class PieceOfWorkDeleteCommandHandler : IRequestHandler<PieceOfWorkDeleteCommand, Unit>
{
    private IPieceOfWorkRepository _pieceOfWorkRepository;

    public PieceOfWorkDeleteCommandHandler(
        IPieceOfWorkRepository pieceOfWorkRepository)
    {
        _pieceOfWorkRepository = pieceOfWorkRepository;
    }

    public async Task<Unit> Handle(PieceOfWorkDeleteCommand command, CancellationToken cancellationToken)
    {
        await _pieceOfWorkRepository.DeleteByIdAsync(command.Id);
        await _pieceOfWorkRepository.SaveChangesAsync();
        return Unit.Value;
    }
}
