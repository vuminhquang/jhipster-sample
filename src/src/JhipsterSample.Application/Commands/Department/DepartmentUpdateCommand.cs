
using JhipsterSample.Domain.Entities;
using MediatR;
using JhipsterSample.Dto;

namespace JhipsterSample.Application.Commands;

public class DepartmentUpdateCommand : IRequest<Department>
{
    public DepartmentDto DepartmentDto { get; set; }
}
