
using JhipsterSample.Domain.Entities;
using MediatR;
using JhipsterSample.Dto;

namespace JhipsterSample.Application.Commands;

public class PieceOfWorkUpdateCommand : IRequest<PieceOfWork>
{
    public PieceOfWorkDto PieceOfWorkDto { get; set; }
}
