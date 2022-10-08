
using AutoMapper;
using System.Linq;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Commands;

public class EmployeeCreateCommandHandler : IRequestHandler<EmployeeCreateCommand, Employee>
{
    private IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeCreateCommandHandler(
        IMapper mapper,
        IEmployeeRepository employeeRepository)
    {
        _mapper = mapper;
        _employeeRepository = employeeRepository;
    }

    public async Task<Employee> Handle(EmployeeCreateCommand command, CancellationToken cancellationToken)
    {
        Employee employee = _mapper.Map<Employee>(command.EmployeeDto);
        var entity = await _employeeRepository.CreateOrUpdateAsync(employee);
        await _employeeRepository.SaveChangesAsync();
        return entity;
    }
}
