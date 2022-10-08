
using JhipsterSample.Domain.Entities;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Commands;

public class DepartmentDeleteCommandHandler : IRequestHandler<DepartmentDeleteCommand, Unit>
{
    private IDepartmentRepository _departmentRepository;

    public DepartmentDeleteCommandHandler(
        IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<Unit> Handle(DepartmentDeleteCommand command, CancellationToken cancellationToken)
    {
        await _departmentRepository.DeleteByIdAsync(command.Id);
        await _departmentRepository.SaveChangesAsync();
        return Unit.Value;
    }
}
