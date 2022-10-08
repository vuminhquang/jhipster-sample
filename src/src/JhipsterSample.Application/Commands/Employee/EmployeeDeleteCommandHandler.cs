
using JhipsterSample.Domain.Entities;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Commands;

public class EmployeeDeleteCommandHandler : IRequestHandler<EmployeeDeleteCommand, Unit>
{
    private IEmployeeRepository _employeeRepository;

    public EmployeeDeleteCommandHandler(
        IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<Unit> Handle(EmployeeDeleteCommand command, CancellationToken cancellationToken)
    {
        await _employeeRepository.DeleteByIdAsync(command.Id);
        await _employeeRepository.SaveChangesAsync();
        return Unit.Value;
    }
}
