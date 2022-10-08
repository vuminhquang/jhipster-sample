using JhipsterSample.Domain.Entities;
using MediatR;

namespace JhipsterSample.Application.Commands;

public class LocationDeleteCommand : IRequest<Unit>
{
    public long Id { get; set; }
}
