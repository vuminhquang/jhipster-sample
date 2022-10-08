
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
    [Route("api/jobs")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private const string EntityName = "job";
        private readonly ILogger<JobsController> _log;
        private readonly IMediator _mediator;

        public JobsController(ILogger<JobsController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<JobDto>> CreateJob([FromBody] JobDto jobDto)
        {
            _log.LogDebug($"REST request to save Job : {jobDto}");
            if (jobDto.Id != 0)
                throw new BadRequestAlertException("A new job cannot already have an ID", EntityName, "idexists");
            var job = await _mediator.Send(new JobCreateCommand { JobDto = jobDto });
            return CreatedAtAction(nameof(GetJob), new { id = job.Id }, job)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, job.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateJob(long id, [FromBody] JobDto jobDto)
        {
            _log.LogDebug($"REST request to update Job : {jobDto}");
            if (jobDto.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != jobDto.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            var job = await _mediator.Send(new JobUpdateCommand { JobDto = jobDto });
            return Ok(job)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, job.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDto>>> GetAllJobs(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of Jobs");
            var result = await _mediator.Send(new JobGetAllQuery { Page = pageable });
            return Ok(((IPage<JobDto>)result).Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJob([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get Job : {id}");
            var result = await _mediator.Send(new JobGetQuery { Id = id });
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete Job : {id}");
            await _mediator.Send(new JobDeleteCommand { Id = id });
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
