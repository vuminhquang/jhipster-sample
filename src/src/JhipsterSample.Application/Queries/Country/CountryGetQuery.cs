
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using MediatR;

namespace JhipsterSample.Application.Queries;

public class CountryGetQuery : IRequest<CountryDto>
{
    public long Id { get; set; }
}
