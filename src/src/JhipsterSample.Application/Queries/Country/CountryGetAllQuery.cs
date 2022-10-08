
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace JhipsterSample.Application.Queries;

public class CountryGetAllQuery : IRequest<Page<CountryDto>>
{
    public IPageable Page { get; set; }
}
