using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintingServer.Application.Common;
using PrintingServer.Application.Managment.Printeds.Commands;
using PrintingServer.Application.Managment.Printeds.Dtos;
using PrintingServer.Application.Managment.Printeds.Queries;

namespace PrintingServer.API.Controllers
{
    [ApiController]
    [Route("api/Printed")]
    [Authorize]
    public class PrintedController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Get All Printed.
        /// </summary>
        /// <returns>IEnumerable of PrintedDTO.</returns>
        /// <response code="200">Returns the list of PrintedDTO.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<PrintedDTO>> GetAll()
        {
            var printers = await mediator.Send(new PrintedGetAll());
            return printers;
        }
        /// <summary>
        /// Search Printed.
        /// </summary>
        /// <param name="printedSearch">The Search parameters as SearchDTO object.</param>
        /// <returns>PagedResult of ClientDTO.</returns>
        /// <response code="200">Returns PagedResult of ClientDTO</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<PagedResult<PrintedDTO>> Search([FromBody] PrintedSearch printedSearch)
        {
            var list = await mediator.Send(printedSearch);
            return (PagedResult<PrintedDTO>)(list ?? new PagedResult<PrintedDTO>());
        }
        /// <summary>
        /// Get Printed by GUID.
        /// </summary>
        /// <param name="id">the GUID value.</param>
        /// <returns>the Printed with all sub entities.</returns>
        /// <response code="200">Returns PrintedDTO</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Printed not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<PrintedDTO> GetByID([FromRoute] Guid id)
        {
            var printer = await mediator.Send(new PrintedGetByID(id));
            return printer;
        }
        /// <summary>
        /// Add new Printed.
        /// </summary>
        /// <param name="command">Printed initial data to create.</param>
        /// <returns>Created printed GUID.</returns>
        /// <response code="200">returns the created Printed GUID.</response>
        /// <response code="302">the Printed already added.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost("New")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<Guid> Create([FromBody] PrintedCreateCommand command)
        {
            Guid id = await mediator.Send(command);
            return id;
        }
    }
}