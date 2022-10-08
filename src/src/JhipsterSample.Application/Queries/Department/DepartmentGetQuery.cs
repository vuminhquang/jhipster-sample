
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using MediatR;

namespace JhipsterSample.Application.Queries;

public class DepartmentGetQuery : IRequest<DepartmentDto>
{
    public long Id { get; set; }
}
