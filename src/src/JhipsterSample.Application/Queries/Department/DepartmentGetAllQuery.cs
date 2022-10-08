
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace JhipsterSample.Application.Queries;

public class DepartmentGetAllQuery : IRequest<Page<DepartmentDto>>
{
    public IPageable Page { get; set; }
}
