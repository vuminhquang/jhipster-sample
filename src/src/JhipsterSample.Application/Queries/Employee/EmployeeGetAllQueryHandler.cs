
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

public class EmployeeGetAllQueryHandler : IRequestHandler<EmployeeGetAllQuery, Page<EmployeeDto>>
{
    private IReadOnlyEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeGetAllQueryHandler(
        IMapper mapper,
        IReadOnlyEmployeeRepository employeeRepository)
    {
        _mapper = mapper;
        _employeeRepository = employeeRepository;
    }

    public async Task<Page<EmployeeDto>> Handle(EmployeeGetAllQuery request, CancellationToken cancellationToken)
    {
        var page = await _employeeRepository.QueryHelper()
            .Include(employee => employee.Manager)
            .Include(employee => employee.Department)
            .GetPageAsync(request.Page);
        return new Page<EmployeeDto>(page.Content.Select(entity => _mapper.Map<EmployeeDto>(entity)).ToList(), request.Page, page.TotalElements);
    }
}
