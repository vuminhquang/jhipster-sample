
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace JhipsterSample.Application.Queries;

public class JobHistoryGetAllQuery : IRequest<Page<JobHistoryDto>>
{
    public IPageable Page { get; set; }
}
