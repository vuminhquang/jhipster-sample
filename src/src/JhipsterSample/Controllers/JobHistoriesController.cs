
using MediatR;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using JhipsterSample.Domain.Entities;
using JhipsterSample.Crosscutting.Enums;
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
    [Route("api/job-histories")]
    [ApiController]
    public class JobHistoriesController : ControllerBase
    {
        private const string EntityName = "jobHistory";
        private readonly ILogger<JobHistoriesController> _log;
        private readonly IMediator _mediator;

        public JobHistoriesController(ILogger<JobHistoriesController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<JobHistoryDto>> CreateJobHistory([FromBody] JobHistoryDto jobHistoryDto)
        {
            _log.LogDebug($"REST request to save JobHistory : {jobHistoryDto}");
            if (jobHistoryDto.Id != 0)
                throw new BadRequestAlertException("A new jobHistory cannot already have an ID", EntityName, "idexists");
            var jobHistory = await _mediator.Send(new JobHistoryCreateCommand { JobHistoryDto = jobHistoryDto });
            return CreatedAtAction(nameof(GetJobHistory), new { id = jobHistory.Id }, jobHistory)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, jobHistory.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateJobHistory(long id, [FromBody] JobHistoryDto jobHistoryDto)
        {
            _log.LogDebug($"REST request to update JobHistory : {jobHistoryDto}");
            if (jobHistoryDto.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != jobHistoryDto.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            var jobHistory = await _mediator.Send(new JobHistoryUpdateCommand { JobHistoryDto = jobHistoryDto });
            return Ok(jobHistory)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, jobHistory.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobHistoryDto>>> GetAllJobHistories(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of JobHistories");
            var result = await _mediator.Send(new JobHistoryGetAllQuery { Page = pageable });
            return Ok(((IPage<JobHistoryDto>)result).Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobHistory([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get JobHistory : {id}");
            var result = await _mediator.Send(new JobHistoryGetQuery { Id = id });
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobHistory([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete JobHistory : {id}");
            await _mediator.Send(new JobHistoryDeleteCommand { Id = id });
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
