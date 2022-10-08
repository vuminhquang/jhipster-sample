
using JhipsterSample.Domain.Entities;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Commands;

public class RegionDeleteCommandHandler : IRequestHandler<RegionDeleteCommand, Unit>
{
    private IRegionRepository _regionRepository;

    public RegionDeleteCommandHandler(
        IRegionRepository regionRepository)
    {
        _regionRepository = regionRepository;
    }

    public async Task<Unit> Handle(RegionDeleteCommand command, CancellationToken cancellationToken)
    {
        await _regionRepository.DeleteByIdAsync(command.Id);
        await _regionRepository.SaveChangesAsync();
        return Unit.Value;
    }
}
