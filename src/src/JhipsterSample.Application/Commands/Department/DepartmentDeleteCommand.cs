using JhipsterSample.Domain.Entities;
using MediatR;

namespace JhipsterSample.Application.Commands;

public class DepartmentDeleteCommand : IRequest<Unit>
{
    public long Id { get; set; }
}
