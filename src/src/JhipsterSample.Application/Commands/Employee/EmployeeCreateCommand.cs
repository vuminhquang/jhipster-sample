
using JhipsterSample.Domain.Entities;
using MediatR;
using JhipsterSample.Dto;

namespace JhipsterSample.Application.Commands;

public class EmployeeCreateCommand : IRequest<Employee>
{
    public EmployeeDto EmployeeDto { get; set; }
}
