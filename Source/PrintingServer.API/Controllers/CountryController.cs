using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintingServer.Application.Common;
using PrintingServer.Application.Managment.Countries.Commands;
using PrintingServer.Application.Managment.Countries.Dtos;
using PrintingServer.Application.Managment.Countries.Queries;
using PrintingServer.Domain.Constants;

namespace PrintingServer.API.Controllers
{
    [ApiController]
    [Authorize(Roles = UserRoles.Manager)]
    [Route("api/Country")]
    public class CountryController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Get All Countries.
        /// </summary>
        /// <returns>IEnumerable of CountryDTO.</returns>
        /// <response code="200">Returns the list of CountryDTO</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet]
        [Route("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<CountryDTO>> GetCountryAsync()
        {
            var toreturn = await mediator.Send(new CountryGetAll());
            return toreturn;
        }
        /// <summary>
        /// Get All Active Countries.
        /// </summary>
        /// <returns>IEnumerable of CountryDTO.</returns>
        /// <response code="200">Returns the list of CountryDTO.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet]
        [Route("AllActive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<CountryDTO>> GetActiveCountryAsync()
        {
            var toreturn = await mediator.Send(new CountryGetAllActive());
            return toreturn;
        }
        /// <summary>
        /// Search Countries.
        /// </summary>
        /// <param name="countrySearch">The Search parameters as SearchDTO object.</param>
        /// <returns>PagedResult of CountryDTO.</returns>
        /// <response code="200">Returns PagedResult of CountryDTO</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<PagedResult<CountryDTO>> Search([FromBody] CountrySearch countrySearch)
        {
            var list = await mediator.Send(countrySearch);
            return (PagedResult<CountryDTO>)(list ?? new PagedResult<CountryDTO>());
        }
        /// <summary>
        /// Get Country by GUID.
        /// </summary>
        /// <param name="id">the GUID value.</param>
        /// <returns>the Country with all sub entities.</returns>
        /// <response code="200">Returns CountryDTO</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Country not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<CountryDTO> GetByID([FromRoute] Guid id)
        {
            var client = await mediator.Send(new CountryGetByID(id));
            return client;
        }
        /// <summary>
        /// Update Country data.
        /// </summary>
        /// <param name="id">Country GUID to update.</param>
        /// <param name="command">Country data To update.</param>
        /// <returns></returns>
        /// <response code="200">updated successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Country not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPatch("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Update([FromRoute] Guid id, CountryUpdateCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
        }
        /// <summary>
        /// Set Selected Country as Active.
        /// </summary>
        /// <param name="id">Country GUID to activate.</param>
        /// <returns></returns>
        /// <response code="200">activated successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Country not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPatch("Activate/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Activate([FromRoute] Guid id)
        {
            await mediator.Send(new CountryActivateCommand(id));
        }
        /// <summary>
        /// Set Selected Country as Inactive.
        /// </summary>
        /// <param name="id">Country GUID to deactivate.</param>
        /// <returns></returns>
        /// <response code="200">deactivated successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Country not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPatch("DeActivate/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task DeActivate([FromRoute] Guid id)
        {
            await mediator.Send(new CountryDeActivateCommand(id));
        }
        /// <summary>
        /// Delete Country.
        /// </summary>
        /// <param name="id">Country GUID to delete.</param>
        /// <returns></returns>
        /// <response code="200">deleted successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Country not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Delete([FromRoute] Guid id)
        {
            await mediator.Send(new CountryDeleteCommand(id));
        }
    }
}