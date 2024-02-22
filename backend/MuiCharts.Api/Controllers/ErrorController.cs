using Microsoft.AspNetCore.Mvc;

namespace MuiCharts.Api;

/// <summary>
/// Controller for handling errors.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ErrorController: ControllerBase
{
	/// <summary>
	/// Handles the HTTP GET request for the error endpoint.
	/// </summary>
	/// <returns>An IActionResult representing the error response.</returns>
	[HttpGet]
	[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
	public IActionResult Error() => Problem();
}
