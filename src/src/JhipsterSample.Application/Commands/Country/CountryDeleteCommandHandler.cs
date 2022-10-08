
using JhipsterSample.Domain.Entities;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Commands;

public class CountryDeleteCommandHandler : IRequestHandler<CountryDeleteCommand, Unit>
{
    private ICountryRepository _countryRepository;

    public CountryDeleteCommandHandler(
        ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<Unit> Handle(CountryDeleteCommand command, CancellationToken cancellationToken)
    {
        await _countryRepository.DeleteByIdAsync(command.Id);
        await _countryRepository.SaveChangesAsync();
        return Unit.Value;
    }
}
