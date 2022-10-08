
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
    [Route("api/regions")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private const string EntityName = "region";
        private readonly ILogger<RegionsController> _log;
        private readonly IMediator _mediator;

        public RegionsController(ILogger<RegionsController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<RegionDto>> CreateRegion([FromBody] RegionDto regionDto)
        {
            _log.LogDebug($"REST request to save Region : {regionDto}");
            if (regionDto.Id != 0)
                throw new BadRequestAlertException("A new region cannot already have an ID", EntityName, "idexists");
            var region = await _mediator.Send(new RegionCreateCommand { RegionDto = regionDto });
            return CreatedAtAction(nameof(GetRegion), new { id = region.Id }, region)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, region.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRegion(long id, [FromBody] RegionDto regionDto)
        {
            _log.LogDebug($"REST request to update Region : {regionDto}");
            if (regionDto.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != regionDto.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            var region = await _mediator.Send(new RegionUpdateCommand { RegionDto = regionDto });
            return Ok(region)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, region.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegionDto>>> GetAllRegions(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of Regions");
            var result = await _mediator.Send(new RegionGetAllQuery { Page = pageable });
            return Ok(((IPage<RegionDto>)result).Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegion([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get Region : {id}");
            var result = await _mediator.Send(new RegionGetQuery { Id = id });
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete Region : {id}");
            await _mediator.Send(new RegionDeleteCommand { Id = id });
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
