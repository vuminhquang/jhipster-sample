using JhipsterSample.Domain.Entities;
using MediatR;

namespace JhipsterSample.Application.Commands;

public class CountryDeleteCommand : IRequest<Unit>
{
    public long Id { get; set; }
}
