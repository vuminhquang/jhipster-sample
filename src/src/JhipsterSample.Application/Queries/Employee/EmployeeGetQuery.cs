
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using MediatR;

namespace JhipsterSample.Application.Queries;

public class EmployeeGetQuery : IRequest<EmployeeDto>
{
    public long Id { get; set; }
}
