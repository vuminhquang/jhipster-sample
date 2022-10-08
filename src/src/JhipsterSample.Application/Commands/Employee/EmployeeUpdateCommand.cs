
using JhipsterSample.Domain.Entities;
using MediatR;
using JhipsterSample.Dto;

namespace JhipsterSample.Application.Commands;

public class EmployeeUpdateCommand : IRequest<Employee>
{
    public EmployeeDto EmployeeDto { get; set; }
}
