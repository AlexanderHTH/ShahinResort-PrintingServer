using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintingServer.Application.Common;
using PrintingServer.Application.Managment.Clients.Commands;
using PrintingServer.Application.Managment.Clients.Dtos;
using PrintingServer.Application.Managment.Clients.Queries;
using PrintingServer.Application.Managment.Reports.Dtos;
using PrintingServer.Domain.Constants;

namespace PrintingServer.API.Controllers
{
    [ApiController]
    [Authorize(Roles = UserRoles.Manager + "," + UserRoles.Administrator)]
    [Route("api/Client")]
    public class ClientController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Get All Clients.
        /// </summary>
        /// <returns>IEnumerable of ClientDTO.</returns>
        /// <response code="200">Returns the list of ClientDTO</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<ClientDTO>> GetAll()
        {
            var clients = await mediator.Send(new ClientGetAll());
            return clients;
        }
        /// <summary>
        /// Get All Active Clients.
        /// </summary>
        /// <returns>IEnumerable of ClientDTO.</returns>
        /// <response code="200">Returns the list of ClientDTO</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("AllActive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<ClientDTO>> GetAllActive()
        {
            var clients = await mediator.Send(new ClientGetAllActive());
            return clients;
        }
        /// <summary>
        /// Search Clients.
        /// </summary>
        /// <param name="clientSearch">The Search parameters as SearchDTO object.</param>
        /// <returns>PagedResult of ClientDTO.</returns>
        /// <response code="200">Returns PagedResult of ClientDTO</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous]
        public async Task<PagedResult<ClientDTO>> Search([FromBody] ClientSearch clientSearch)
        {
            var clients = await mediator.Send(clientSearch);
            return (PagedResult<ClientDTO>)(clients ?? new PagedResult<ClientDTO>());
        }
        /// <summary>
        /// Get Client by GUID.
        /// </summary>
        /// <param name="id">the GUID value.</param>
        /// <returns>the Client with all sub entities.</returns>
        /// <response code="200">Returns ClientDTO</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Client not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ClientDTO> GetByID([FromRoute] Guid id)
        {
            var client = await mediator.Send(new ClientGetByID(id));
            return client;
        }
        /// <summary>
        /// Get All reports allowed to printed by selected client.
        /// </summary>
        /// <param name="id">Client GUID.</param>
        /// <returns>List of Reports.</returns>
        /// <response code="200">Returns List of ReportDTO</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Client not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("{id}/AllowedReports")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<ReportDTO>> AllowedReports([FromRoute] Guid id)
        {
            var result = await mediator.Send(new ClientAllowedReports(id));
            return result.ToList();
        }
        /// <summary>
        /// Add new Client.
        /// </summary>
        /// <param name="command">Client initial data to create.</param>
        /// <returns>Created client GUID.</returns>
        /// <response code="200">returns the created Client GUID.</response>
        /// <response code="302">the Client already added.</response>
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
        public async Task<Guid> Create([FromBody] ClientCreateCommand command)
        {
            Guid id = await mediator.Send(command);
            return id;
        }
        /// <summary>
        /// Update Client data.
        /// </summary>
        /// <param name="id">Client GUID to update.</param>
        /// <param name="command">Client data To update.</param>
        /// <returns></returns>
        /// <response code="200">updated successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Client not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPatch("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Update([FromRoute] Guid id, ClientUpdateCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
        }
        /// <summary>
        /// Set Selected Client as Active.
        /// </summary>
        /// <param name="id">Client GUID to activate</param>
        /// <returns></returns>
        /// <response code="200">activated successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Client not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPatch("Activate/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Activate([FromRoute] Guid id)
        {
            await mediator.Send(new ClientActivateCommand(id));
        }
        /// <summary>
        /// Set Selected Client as Inactive.
        /// </summary>
        /// <param name="id">Client GUID to deactivate</param>
        /// <returns></returns>
        /// <response code="200">deactivated successfully.</response>
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
            await mediator.Send(new ClientDeActivateCommand(id));
        }
        /// <summary>
        /// Delete client.
        /// </summary>
        /// <param name="id">Client GUID to delete.</param>
        /// <returns></returns>
        /// <response code="200">deleted successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Client not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Delete([FromRoute] Guid id)
        {
            await mediator.Send(new ClientDeleteCommand(id));
        }
    }
}