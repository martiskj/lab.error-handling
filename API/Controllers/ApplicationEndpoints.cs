using System.ComponentModel.DataAnnotations;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class CreateApplicationRequest
{
    [Required]
    public string? Code { get; set; }
};

[ApiController]
public class ApplicationsController : ControllerBase
{
    [HttpPost("v1/applications")]
    public async Task<IActionResult> CreateApplication([FromBody] CreateApplicationRequest r, [FromServices] ApplicationService service)
    {
        var application = await service.Create(r.Code!);
        return Ok(application);
    }

    [HttpGet("v1/applications")]
    public async Task<IActionResult> GetApplicationsByQuery([FromServices] ApplicationService service)
    {
        var applications = await service.GetByQuery(new GetApplicationsQuery());
        return Ok(applications);
    }

    [HttpGet("v1/applications/{id}")]
    public async Task<IActionResult> GetApplicationById(Guid id, [FromServices] ApplicationService service)
    {
        var application = await service.GetById(id);
        if (application is null)
        {
            return Problem(
                    title: "Application not found",
                    detail: $"Application with id {id} not found",
                    statusCode: 404);
        }

        return Ok(application);
    }

    [HttpDelete("v1/applications/{id}")]
    public async Task<IActionResult> DeleteApplication(Guid id, [FromServices] ApplicationService service)
    {
        var application = await service.GetById(id);
        if (application is null)
        {
            return Problem(
                    title: "Application not found",
                    detail: $"Application with id {id} not found",
                    statusCode: 404);
        }

        await service.Delete(application);
        return NoContent();
    }

    [HttpPut("v1/applications/{id}/manualprocessing")]
    public async Task<IActionResult> SetApplicationToManualProcessing(Guid id, [FromServices] ApplicationService service, [FromServices] ApplicationStateService states)
    {
        var application = await service.GetById(id);
        if (application is null)
        {
            return Problem(
                    title: "Application not found",
                    detail: $"Application with id {id} not found",
                    statusCode: 404);
        }

        states.SetToManualProcessing(application);
        return NoContent();
    }
}
