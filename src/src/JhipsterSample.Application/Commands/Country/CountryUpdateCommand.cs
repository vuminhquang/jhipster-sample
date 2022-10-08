
using JhipsterSample.Domain.Entities;
using MediatR;
using JhipsterSample.Dto;

namespace JhipsterSample.Application.Commands;

public class CountryUpdateCommand : IRequest<Country>
{
    public CountryDto CountryDto { get; set; }
}
