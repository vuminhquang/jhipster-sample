
using AutoMapper;
using System.Linq;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Dto;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;

namespace JhipsterSample.Application.Queries;

public class DepartmentGetAllQueryHandler : IRequestHandler<DepartmentGetAllQuery, Page<DepartmentDto>>
{
    private IReadOnlyDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public DepartmentGetAllQueryHandler(
        IMapper mapper,
        IReadOnlyDepartmentRepository departmentRepository)
    {
        _mapper = mapper;
        _departmentRepository = departmentRepository;
    }

    public async Task<Page<DepartmentDto>> Handle(DepartmentGetAllQuery request, CancellationToken cancellationToken)
    {
        var page = await _departmentRepository.QueryHelper()
            .Include(department => department.Location)
            .GetPageAsync(request.Page);
        return new Page<DepartmentDto>(page.Content.Select(entity => _mapper.Map<DepartmentDto>(entity)).ToList(), request.Page, page.TotalElements);
    }
}
