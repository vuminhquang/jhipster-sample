
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace JhipsterSample.Application.Queries;

public class JobGetAllQuery : IRequest<Page<JobDto>>
{
    public IPageable Page { get; set; }
}
