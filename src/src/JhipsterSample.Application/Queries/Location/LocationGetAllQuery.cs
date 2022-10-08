
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using JHipsterNet.Core.Pagination;
using MediatR;

namespace JhipsterSample.Application.Queries;

public class LocationGetAllQuery : IRequest<Page<LocationDto>>
{
    public IPageable Page { get; set; }
}
