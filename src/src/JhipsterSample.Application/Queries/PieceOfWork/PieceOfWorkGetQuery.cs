
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using MediatR;

namespace JhipsterSample.Application.Queries;

public class PieceOfWorkGetQuery : IRequest<PieceOfWorkDto>
{
    public long Id { get; set; }
}
