
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
    [Route("api/departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private const string EntityName = "department";
        private readonly ILogger<DepartmentsController> _log;
        private readonly IMediator _mediator;

        public DepartmentsController(ILogger<DepartmentsController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<DepartmentDto>> CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            _log.LogDebug($"REST request to save Department : {departmentDto}");
            if (departmentDto.Id != 0)
                throw new BadRequestAlertException("A new department cannot already have an ID", EntityName, "idexists");
            var department = await _mediator.Send(new DepartmentCreateCommand { DepartmentDto = departmentDto });
            return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, department.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateDepartment(long id, [FromBody] DepartmentDto departmentDto)
        {
            _log.LogDebug($"REST request to update Department : {departmentDto}");
            if (departmentDto.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != departmentDto.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            var department = await _mediator.Send(new DepartmentUpdateCommand { DepartmentDto = departmentDto });
            return Ok(department)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, department.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllDepartments(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of Departments");
            var result = await _mediator.Send(new DepartmentGetAllQuery { Page = pageable });
            return Ok(((IPage<DepartmentDto>)result).Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get Department : {id}");
            var result = await _mediator.Send(new DepartmentGetQuery { Id = id });
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete Department : {id}");
            await _mediator.Send(new DepartmentDeleteCommand { Id = id });
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
