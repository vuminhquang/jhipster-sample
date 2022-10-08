
using JhipsterSample.Domain.Entities;
using MediatR;
using JhipsterSample.Dto;

namespace JhipsterSample.Application.Commands;

public class PieceOfWorkCreateCommand : IRequest<PieceOfWork>
{
    public PieceOfWorkDto PieceOfWorkDto { get; set; }
}
