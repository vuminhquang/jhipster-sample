
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
    [Route("api/locations")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private const string EntityName = "location";
        private readonly ILogger<LocationsController> _log;
        private readonly IMediator _mediator;

        public LocationsController(ILogger<LocationsController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<LocationDto>> CreateLocation([FromBody] LocationDto locationDto)
        {
            _log.LogDebug($"REST request to save Location : {locationDto}");
            if (locationDto.Id != 0)
                throw new BadRequestAlertException("A new location cannot already have an ID", EntityName, "idexists");
            var location = await _mediator.Send(new LocationCreateCommand { LocationDto = locationDto });
            return CreatedAtAction(nameof(GetLocation), new { id = location.Id }, location)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, location.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateLocation(long id, [FromBody] LocationDto locationDto)
        {
            _log.LogDebug($"REST request to update Location : {locationDto}");
            if (locationDto.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != locationDto.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            var location = await _mediator.Send(new LocationUpdateCommand { LocationDto = locationDto });
            return Ok(location)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, location.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetAllLocations(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of Locations");
            var result = await _mediator.Send(new LocationGetAllQuery { Page = pageable });
            return Ok(((IPage<LocationDto>)result).Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocation([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get Location : {id}");
            var result = await _mediator.Send(new LocationGetQuery { Id = id });
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete Location : {id}");
            await _mediator.Send(new LocationDeleteCommand { Id = id });
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
