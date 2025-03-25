using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrintingServer.Application.Common;
using PrintingServer.Application.Managment.Reports.Commands;
using PrintingServer.Application.Managment.Reports.Dtos;
using PrintingServer.Application.Managment.Reports.Queries;
using PrintingServer.Domain.Constants;

namespace PrintingServer.API.Controllers
{
    [ApiController]
    [Route("api/Report")]
    [Authorize(Roles = UserRoles.Manager)]
    public class ReportController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Get All Reports.
        /// </summary>
        /// <returns>IEnumerable of ReportDTO.</returns>
        /// <response code="200">Returns the list of ReportDTO.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<ReportDTO>> GetAll()
        {
            var reports = await mediator.Send(new ReportGetAll());
            return reports;
        }
        /// <summary>
        /// Get All Active Reports.
        /// </summary>
        /// <returns>IEnumerable of ReportDTO.</returns>
        /// <response code="200">Returns the list of ReportDTO.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("AllActive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<ReportDTO>> GetAllActive()
        {
            var reports = await mediator.Send(new ReportGetAllActive());
            return reports;
        }
        /// <summary>
        /// Search Reports.
        /// </summary>
        /// <param name="reportSearch">The Search parameters as SearchDTO object.</param>
        /// <returns>PagedResult of ReportDTO.</returns>
        /// <response code="200">Returns PagedResult of ReportDTO.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPost("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<PagedResult<ReportDTO>> Search([FromBody] ReportSearch reportSearch)
        {
            var list = await mediator.Send(reportSearch);
            return (PagedResult<ReportDTO>)(list ?? new PagedResult<ReportDTO>());
        }
        /// <summary>
        /// Get Report by GUID.
        /// </summary>
        /// <param name="id">the GUID value.</param>
        /// <returns>the Report with all sub entities.</returns>
        /// <response code="200">Returns ReportDTO.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Report not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ReportDTO> GetByID([FromRoute] Guid id)
        {
            var reports = await mediator.Send(new ReportGetByID(id));
            return reports;
        }
        /// <summary>
        /// Add new Report.
        /// </summary>
        /// <param name="command">Report initial data to create.</param>
        /// <returns>Created Report GUID.</returns>
        /// <response code="200">returns the created Report GUID.</response>
        /// <response code="302">the Report already added.</response>
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
        public async Task<Guid> Create([FromBody] ReportCreate command)
        {
            Guid id = await mediator.Send(command);
            return id;
        }
        /// <summary>
        /// Update Report data.
        /// </summary>
        /// <param name="id">Report GUID to update.</param>
        /// <param name="command">Report data To update.</param>
        /// <returns></returns>
        /// <response code="200">updated successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Report not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPatch("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Update([FromRoute] Guid id, ReportUpdate command)
        {
            command.Id = id;
            await mediator.Send(command);
        }
        /// <summary>
        /// Set Selected Report as Active.
        /// </summary>
        /// <param name="id">Report GUID to activate</param>
        /// <returns></returns>
        /// <response code="200">activated successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Report not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPatch("Activate/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Activate([FromRoute] Guid id)
        {
            await mediator.Send(new ReportActivate(id));
        }
        /// <summary>
        /// Set Selected Report as Inactive.
        /// </summary>
        /// <param name="id">Report GUID to deactivate</param>
        /// <returns></returns>
        /// <response code="200">deactivated successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Report not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpPatch("DeActivate/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task DeActivate([FromRoute] Guid id)
        {
            await mediator.Send(new ReportDeActivate(id));
        }
        /// <summary>
        /// Delete Report.
        /// </summary>
        /// <param name="id">Report GUID to delete.</param>
        /// <returns></returns>
        /// <response code="200">deleted successfully.</response>
        /// <response code="401">Unauthorized user access.</response>
        /// <response code="403">Forbidden without user.</response>
        /// <response code="404">Report not found.</response>
        /// <response code="500">If there is a server error.</response>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task Delete([FromRoute] Guid id)
        {
            await mediator.Send(new ReportDeleteCommand(id));
        }
    }
}
