
using AutoMapper;
using System.Linq;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Queries;

public class DepartmentGetQueryHandler : IRequestHandler<DepartmentGetQuery, DepartmentDto>
{
    private IReadOnlyDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public DepartmentGetQueryHandler(
        IMapper mapper,
        IReadOnlyDepartmentRepository departmentRepository)
    {
        _mapper = mapper;
        _departmentRepository = departmentRepository;
    }

    public async Task<DepartmentDto> Handle(DepartmentGetQuery request, CancellationToken cancellationToken)
    {
        var entity = await _departmentRepository.QueryHelper()
            .Include(department => department.Location)
            .GetOneAsync(department => department.Id == request.Id);
        return _mapper.Map<DepartmentDto>(entity);
    }
}
