
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace JhipsterSample.Application.Queries;

public class PieceOfWorkGetAllQuery : IRequest<Page<PieceOfWorkDto>>
{
    public IPageable Page { get; set; }
}
