
using JhipsterSample.Domain.Entities;
using MediatR;
using JhipsterSample.Dto;

namespace JhipsterSample.Application.Commands;

public class CountryCreateCommand : IRequest<Country>
{
    public CountryDto CountryDto { get; set; }
}
