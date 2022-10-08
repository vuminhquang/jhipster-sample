
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
    [Route("api/piece-of-works")]
    [ApiController]
    public class PieceOfWorksController : ControllerBase
    {
        private const string EntityName = "pieceOfWork";
        private readonly ILogger<PieceOfWorksController> _log;
        private readonly IMediator _mediator;

        public PieceOfWorksController(ILogger<PieceOfWorksController> log, IMediator mediator)
        {
            _log = log;
            _mediator = mediator;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<PieceOfWorkDto>> CreatePieceOfWork([FromBody] PieceOfWorkDto pieceOfWorkDto)
        {
            _log.LogDebug($"REST request to save PieceOfWork : {pieceOfWorkDto}");
            if (pieceOfWorkDto.Id != 0)
                throw new BadRequestAlertException("A new pieceOfWork cannot already have an ID", EntityName, "idexists");
            var pieceOfWork = await _mediator.Send(new PieceOfWorkCreateCommand { PieceOfWorkDto = pieceOfWorkDto });
            return CreatedAtAction(nameof(GetPieceOfWork), new { id = pieceOfWork.Id }, pieceOfWork)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, pieceOfWork.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdatePieceOfWork(long id, [FromBody] PieceOfWorkDto pieceOfWorkDto)
        {
            _log.LogDebug($"REST request to update PieceOfWork : {pieceOfWorkDto}");
            if (pieceOfWorkDto.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != pieceOfWorkDto.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            var pieceOfWork = await _mediator.Send(new PieceOfWorkUpdateCommand { PieceOfWorkDto = pieceOfWorkDto });
            return Ok(pieceOfWork)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, pieceOfWork.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PieceOfWorkDto>>> GetAllPieceOfWorks(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of PieceOfWorks");
            var result = await _mediator.Send(new PieceOfWorkGetAllQuery { Page = pageable });
            return Ok(((IPage<PieceOfWorkDto>)result).Content).WithHeaders(result.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPieceOfWork([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get PieceOfWork : {id}");
            var result = await _mediator.Send(new PieceOfWorkGetQuery { Id = id });
            return ActionResultUtil.WrapOrNotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePieceOfWork([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete PieceOfWork : {id}");
            await _mediator.Send(new PieceOfWorkDeleteCommand { Id = id });
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
