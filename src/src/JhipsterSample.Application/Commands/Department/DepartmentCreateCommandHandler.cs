
using AutoMapper;
using System.Linq;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Domain.Repositories.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JhipsterSample.Application.Commands;

public class DepartmentCreateCommandHandler : IRequestHandler<DepartmentCreateCommand, Department>
{
    private IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public DepartmentCreateCommandHandler(
        IMapper mapper,
        IDepartmentRepository departmentRepository)
    {
        _mapper = mapper;
        _departmentRepository = departmentRepository;
    }

    public async Task<Department> Handle(DepartmentCreateCommand command, CancellationToken cancellationToken)
    {
        Department department = _mapper.Map<Department>(command.DepartmentDto);
        var entity = await _departmentRepository.CreateOrUpdateAsync(department);
        await _departmentRepository.SaveChangesAsync();
        return entity;
    }
}
