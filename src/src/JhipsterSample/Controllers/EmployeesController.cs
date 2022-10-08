
using MediatR;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Crosscutting.Exceptions;
using JhipsterSample.Dto;
using JhipsterSample.Web.Extensions;
using JhipsterSample.Web.Filters;
using JhipsterSample.Web.Rest.Utilities;
using JhipsterSample.Application.Queries;
using JhipsterSample.Application.Commands;
using JhipsterSample.Infrastructure.Web.Rest.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace JhipsterSample.Controllers
{
    [Authorize]
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private const string EntityName = "employee";
        private readonly ILogger<EmployeesController> _log;
        private readonly IMediator _mediator;

        public EmployeesController(ILogger<EmployeesController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            _log.LogDebug($"REST request to save Employee : {employeeDto}");
            if (employeeDto.Id != 0)
                throw new BadRequestAlertException("A new employee cannot already have an ID", EntityName, "idexists");
            var employee = await _mediator.Send(new EmployeeCreateCommand { EmployeeDto = employeeDto });
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, employee.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateEmployee(long id, [FromBody] EmployeeDto employeeDto)
        {
            _log.LogDebug($"REST request to update Employee : {employeeDto}");
            if (employeeDto.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != employeeDto.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            var employee = await _mediator.Send(new EmployeeUpdateCommand { EmployeeDto = employeeDto });
            return Ok(employee)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, employee.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllEmployees(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of Employees");
            var result = await _mediator.Send(new EmployeeGetAllQuery { Page = pageable });
            return Ok(((IPage<EmployeeDto>)result).Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get Employee : {id}");
            var result = await _mediator.Send(new EmployeeGetQuery { Id = id });
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete Employee : {id}");
            await _mediator.Send(new EmployeeDeleteCommand { Id = id });
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
