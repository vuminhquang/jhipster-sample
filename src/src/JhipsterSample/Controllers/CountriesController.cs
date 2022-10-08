
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
    [Route("api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private const string EntityName = "country";
        private readonly ILogger<CountriesController> _log;
        private readonly IMediator _mediator;

        public CountriesController(ILogger<CountriesController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<CountryDto>> CreateCountry([FromBody] CountryDto countryDto)
        {
            _log.LogDebug($"REST request to save Country : {countryDto}");
            if (countryDto.Id != 0)
                throw new BadRequestAlertException("A new country cannot already have an ID", EntityName, "idexists");
            var country = await _mediator.Send(new CountryCreateCommand { CountryDto = countryDto });
            return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, country)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, country.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateCountry(long id, [FromBody] CountryDto countryDto)
        {
            _log.LogDebug($"REST request to update Country : {countryDto}");
            if (countryDto.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != countryDto.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            var country = await _mediator.Send(new CountryUpdateCommand { CountryDto = countryDto });
            return Ok(country)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, country.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetAllCountries(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of Countries");
            var result = await _mediator.Send(new CountryGetAllQuery { Page = pageable });
            return Ok(((IPage<CountryDto>)result).Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCountry([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get Country : {id}");
            var result = await _mediator.Send(new CountryGetQuery { Id = id });
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete Country : {id}");
            await _mediator.Send(new CountryDeleteCommand { Id = id });
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
