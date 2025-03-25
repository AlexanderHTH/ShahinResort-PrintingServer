using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintingServer.Application.Common;
using PrintingServer.Application.Managment.PrinterErrorLogs.Commands;
using PrintingServer.Application.Managment.PrinterErrorLogs.Dtos;
using PrintingServer.Application.Managment.PrinterErrorLogs.Queries;

namespace PrintingServer.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/PrinterErrorLog")]
    public class PrinterErrorLogController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Get All Printer Error Logs.
        /// </summary>
        /// <returns>IEnumerable of PrinterErrorLogsDTO.</returns>
        /// <response code="200">Returns the list of PrinterErrorLogsDTO</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<PrinterErrorLogDTO>> GetAll()
        {
            var printers = await mediator.Send(new PrinterErrorLogGetAll());
            return printers;
        }
        /// <summary>
        /// Search Printer Error Logs.
        /// </summary>
        /// <param name="printerErrorLogSearch">The Search parameters as SearchDTO object.</param>
        /// <returns>PagedResult of PrinterErrorLogsDTO.</returns>
        /// <response code="200">Returns PagedResult of PrinterErrorLogsDTO.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<PagedResult<PrinterErrorLogDTO>> Search([FromBody] PrinterErrorLogSearch printerErrorLogSearch)
        {
            var list = await mediator.Send(printerErrorLogSearch);
            return (PagedResult<PrinterErrorLogDTO>)(list ?? new PagedResult<PrinterErrorLogDTO>());
        }
        /// <summary>
        /// Add new Printer Error Logs.
        /// </summary>
        /// <param name="command">Printer Error Logs initial data to create.</param>
        /// <returns>Created Printer Error Logs GUID.</returns>
        /// <response code="200">returns the created Printer Error Logs GUID.</response>
        /// <response code="302">the Printer Error Logs already added.</response>
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
        public async Task<Guid> Create([FromBody] PrinterErrorLogCreate command)
        {
            Guid id = await mediator.Send(command);
            return id;
        }
    }
}