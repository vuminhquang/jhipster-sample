using JhipsterSample.Domain.Entities;
using MediatR;

namespace JhipsterSample.Application.Commands;

public class PieceOfWorkDeleteCommand : IRequest<Unit>
{
    public long Id { get; set; }
}
