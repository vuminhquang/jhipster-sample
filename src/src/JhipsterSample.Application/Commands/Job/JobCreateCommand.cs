
using JhipsterSample.Domain.Entities;
using MediatR;
using JhipsterSample.Dto;

namespace JhipsterSample.Application.Commands;

public class JobCreateCommand : IRequest<Job>
{
    public JobDto JobDto { get; set; }
}
