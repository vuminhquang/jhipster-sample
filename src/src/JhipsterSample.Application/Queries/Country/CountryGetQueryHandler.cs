
using AutoMapper;
using System.Linq;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Queries;

public class CountryGetQueryHandler : IRequestHandler<CountryGetQuery, CountryDto>
{
    private IReadOnlyCountryRepository _countryRepository;
    private readonly IMapper _mapper;

    public CountryGetQueryHandler(
        IMapper mapper,
        IReadOnlyCountryRepository countryRepository)
    {
        _mapper = mapper;
        _countryRepository = countryRepository;
    }

    public async Task<CountryDto> Handle(CountryGetQuery request, CancellationToken cancellationToken)
    {
        var entity = await _countryRepository.QueryHelper()
            .Include(country => country.Region)
            .GetOneAsync(country => country.Id == request.Id);
        return _mapper.Map<CountryDto>(entity);
    }
}
