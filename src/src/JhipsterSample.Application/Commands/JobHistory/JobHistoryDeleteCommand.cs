using JhipsterSample.Domain.Entities;
using MediatR;

namespace JhipsterSample.Application.Commands;

public class JobHistoryDeleteCommand : IRequest<Unit>
{
    public long Id { get; set; }
}
