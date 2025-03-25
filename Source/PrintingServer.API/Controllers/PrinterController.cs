using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintingServer.Application.Common;
using PrintingServer.Application.Managment.PrinterErrorLogs.Dtos;
using PrintingServer.Application.Managment.Printers.Commands;
using PrintingServer.Application.Managment.Printers.Dtos;
using PrintingServer.Application.Managment.Printers.Queries;
using PrintingServer.Domain.Constants;

namespace PrintingServer.API.Controllers
{
    [ApiController]
    [Route("api/Printer")]
    [Authorize(Roles = UserRoles.Manager)]
    public class PrinterController(IMediator mediator) : ControllerBase
    {

        /// <summary>
        /// Get All Printers.
        /// </summary>
        /// <returns>IEnumerable of PrinterDTO.</returns>
        /// <response code="200">Returns the list of PrinterDTO.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<PrinterDTO>> GetAll()
        {
            var printers = await mediator.Send(new PrinterGetAll());
            return printers;
        }
        /// <summary>
        /// Get All Active Printers.
        /// </summary>
        /// <returns>IEnumerable of PrinterDTO.</returns>
        /// <response code="200">Returns the list of PrinterDTO</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("AllActive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<PrinterDTO>> GetAllActive()
        {
            var printers = await mediator.Send(new PrinterGetAllActive());
            return printers;
        }
        /// <summary>
        /// Search Printers.
        /// </summary>
        /// <param name="printerSearch">The Search parameters as SearchDTO object.</param>
        /// <returns>PagedResult of PrinterDTO.</returns>
        /// <response code="200">Returns PagedResult of PrinterDTO.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<PagedResult<PrinterDTO>> Search([FromBody] PrinterSearch printerSearch)
        {
            var list = await mediator.Send(printerSearch);
            return (PagedResult<PrinterDTO>)(list ?? new PagedResult<PrinterDTO>());
        }
        /// <summary>
        /// Get Printer by GUID.
        /// </summary>
        /// <param name="id">the GUID value.</param>
        /// <returns>the Printer with all sub entities.</returns>
        /// <response code="200">Returns PrinterDTO</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Printer not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<PrinterDTO> GetByID([FromRoute] Guid id)
        {
            var printer = await mediator.Send(new PrinterGetByID(id));
            return printer;
        }
        /// <summary>
        /// Get Printer Error logs by GUID.
        /// </summary>
        /// <param name="id">the GUID value.</param>
        /// <returns>the Printer Error Logs.</returns>
        /// <response code="200">Returns PrinterErrorLogDTO</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Printer not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("{id}/ErrorLog")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<PrinterErrorLogDTO>> GetErrorLogs([FromRoute] Guid id)
        {
            var toreturn = await mediator.Send(new PrinterGetErrorLogs(id));
            return toreturn.ToList();
        }
        /// <summary>
        /// Add new Printer.
        /// </summary>
        /// <param name="command">Printer initial data to create.</param>
        /// <returns>Created printer GUID.</returns>
        /// <response code="200">returns the created Printer GUID.</response>
        /// <response code="302">the Printer already added.</response>
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
        public async Task<Guid> Create([FromBody] PrinterCreateCommand command)
        {
            Guid id = await mediator.Send(command);
            return id;
        }
        /// <summary>
        /// Update Printer data.
        /// </summary>
        /// <param name="id">Printer GUID to update.</param>
        /// <param name="command">Printer data To update.</param>
        /// <returns></returns>
        /// <response code="200">updated successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Printer not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPatch("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Update([FromRoute] Guid id, PrinterUpdateCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
        }
        /// <summary>
        /// Set Selected Printer as Active.
        /// </summary>
        /// <param name="id">Printer GUID to activate</param>
        /// <response code="200">activated successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Printer not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPatch("Activate/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Activate([FromRoute] Guid id)
        {
            await mediator.Send(new PrinterActivateCommand(id));
        }
        /// <summary>
        /// Set Selected Printer as Inactive.
        /// </summary>
        /// <param name="id">Printer GUID to deactivate</param>
        /// <response code="200">deactivated successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Printer not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPatch("DeActivate/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task DeActivate([FromRoute] Guid id)
        {
            await mediator.Send(new PrinterDeActivateCommand(id));
        }
        /// <summary>
        /// Delete Printer.
        /// </summary>
        /// <param name="id">Printer GUID to delete.</param>
        /// <response code="200">deleted successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Printer not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Delete([FromRoute] Guid id)
        {
            await mediator.Send(new PrinterDeleteCommand(id));
        }
    }
}
