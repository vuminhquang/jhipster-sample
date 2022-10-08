
using AutoMapper;
using System.Linq;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Commands;

public class CountryUpdateCommandHandler : IRequestHandler<CountryUpdateCommand, Country>
{
    private ICountryRepository _countryRepository;
    private readonly IMapper _mapper;

    public CountryUpdateCommandHandler(
        IMapper mapper,
        ICountryRepository countryRepository)
    {
        _mapper = mapper;
        _countryRepository = countryRepository;
    }

    public async Task<Country> Handle(CountryUpdateCommand command, CancellationToken cancellationToken)
    {
        Country country = _mapper.Map<Country>(command.CountryDto);
        var entity = await _countryRepository.CreateOrUpdateAsync(country);
        await _countryRepository.SaveChangesAsync();
        return entity;
    }
}
