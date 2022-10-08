
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace JhipsterSample.Application.Queries;

public class EmployeeGetAllQuery : IRequest<Page<EmployeeDto>>
{
    public IPageable Page { get; set; }
}
