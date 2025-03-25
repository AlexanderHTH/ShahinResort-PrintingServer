using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintingServer.Application.Common;
using PrintingServer.Application.Managment.ClientReports.Commands;
using PrintingServer.Application.Managment.ClientReports.Dtos;
using PrintingServer.Application.Managment.ClientReports.Queries;
using PrintingServer.Application.Managment.Printeds.Dtos;
using PrintingServer.Domain.Constants;

namespace PrintingServer.API.Controllers
{
    [ApiController]
    [Authorize(Roles = UserRoles.Manager)]
    [Route("api/Client_Report")]
    public class ClientReportController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Get all Client-Reports
        /// </summary>
        /// <returns>List of Client-Report.</returns>
        /// <response code="200">returns a list of all Cient-Report.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<ClientReportDTO>> ClientReportGetAll()
        {
            var list = await mediator.Send(new ClientReportGetAll());
            return list;
        }
        /// <summary>
        /// Get all active Client-Reports
        /// </summary>
        /// <returns>List of active Client-Report.</returns>
        /// <response code="200">returns a list of all active Client-Report.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("AllActive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<ClientReportDTO>> ClientReportGetAllActive()
        {
            var list = await mediator.Send(new ClientReportGetAllActive());
            return list;
        }
        /// <summary>
        /// Search Client-Report's.
        /// </summary>
        /// <param name="clientReportSearch">The Search parameters as SearchDTO object.</param>
        /// <returns>PagedResult of ClientReportDTO.</returns>
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
        public async Task<PagedResult<ClientReportDTO>> Search([FromBody] ClientReportSearch clientReportSearch)
        {
            var list = await mediator.Send(clientReportSearch);
            return (PagedResult<ClientReportDTO>)(list ?? new PagedResult<ClientReportDTO>());
        }
        /// <summary>
        /// Get Client-Report by GUID.
        /// </summary>
        /// <param name="id">Client-Report GUID.</param>
        /// <returns>Client-Report with all sub-entities.</returns>
        /// <response code="200">The found Client-Report with all sub-entities.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Client-Report not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ClientReportDTO> GetByID([FromRoute] Guid id)
        {
            var client = await mediator.Send(new ClientReportGetByID(id));
            return client;
        }
        /// <summary>
        /// Get all printed orders of a Client-Report.
        /// </summary>
        /// <param name="id">Client-Report GUID to find.</param>
        /// <returns>List of Printed.</returns>
        /// <response code="200">returns a list of Printed for the Client-Report.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Client-Report not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("{id}/AllPrinted")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<PrintedDTO>> GetAllPrinted([FromRoute] Guid id)
        {
            var result = await mediator.Send(new ClientReportGetAllPrinted(id));
            return result.ToList();
        }

        /// <summary>
        /// Add new Client-Client.
        /// </summary>
        /// <param name="command">Client-Report initial data to create.</param>
        /// <returns>Created Client-Report GUID.</returns>
        /// <response code="200">returns the created Client-Report GUID.</response>
        /// <response code="302">the Client-Report already added.</response>
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
        public async Task<Guid> Create([FromBody] ClientReportCreate command)
        {
            Guid id = await mediator.Send(command);
            return id;
        }
        /// <summary>
        /// Update Client-Report.
        /// </summary>
        /// <param name="id">Client-Report GUID to update.</param>
        /// <param name="command">Client-Report updated data.</param>
        /// <returns>Created client GUID.</returns>
        /// <response code="200">Update done successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Client-Report not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPatch("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Update([FromRoute] Guid id, ClientReportUpdate command)
        {
            command.Id = id;
            await mediator.Send(command);
        }

        /// <summary>
        /// Activate a Client-Report.
        /// </summary>
        /// <param name="id">Client-Report GUID to activate.</param>
        /// <response code="200">Activated successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Client-Report not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPatch("Activate/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Activate([FromRoute] Guid id)
        {
            await mediator.Send(new ClientReportActivateCommand(id));
        }

        /// <summary>
        /// Deactivate Client-Report.
        /// </summary>
        /// <param name="id">Client-Report GUID to deactivate.</param>
        /// <response code="200">Deactivated Successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Client not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPatch("DeActivate/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task DeActivate([FromRoute] Guid id)
        {
            await mediator.Send(new ClientReportDeActivateCommand(id));
        }
        /// <summary>
        /// Delete Client-Report.
        /// </summary>
        /// <param name="id">ClientReport GUID to delete.</param>
        /// <response code="200">Deleted Successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Client-Report not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Delete([FromRoute] Guid id)
        {
            await mediator.Send(new ClientReportDeleteCommand(id));
        }
    }
}