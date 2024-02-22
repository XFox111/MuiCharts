using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MuiCharts.Api.Controllers;

/// <summary>
/// Base class for API controllers that provides common functionality and error handling.
/// </summary>
/// <typeparam name="T">The type of the derived controller.</typeparam>
[ApiController]
[Route("[controller]")]
public abstract class ApiControllerBase<T>(ILogger<T> logger)
	: ControllerBase where T : ApiControllerBase<T>
{
	/// <summary>
	/// Gets the logger instance used for logging.
	/// </summary>
	protected ILogger<T> Logger { get; } = logger;

	/// <summary>
	/// Handles the response for a list of errors.
	/// </summary>
	/// <param name="errors">The list of errors.</param>
	/// <returns>An <see cref="IActionResult"/> representing the response.</returns>
	protected IActionResult Problem(List<Error> errors)
	{
		if (errors.All(error => error.Type == ErrorType.Validation))
		{
			ModelStateDictionary modelState = new();

			foreach (Error error in errors)
				modelState.AddModelError(error.Code, error.Description);

			return ValidationProblem(modelState);
		}

		Error firstError = errors[0];

		Logger.LogWarning("An error occured during request processing: {Error}", firstError);

		int statusCode = firstError.Type switch
		{
			ErrorType.Validation => StatusCodes.Status400BadRequest,
			ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
			ErrorType.Forbidden => StatusCodes.Status403Forbidden,
			ErrorType.NotFound => StatusCodes.Status404NotFound,
			ErrorType.Conflict => StatusCodes.Status409Conflict,
			_ => StatusCodes.Status500InternalServerError
		};

		return Problem(
			statusCode: statusCode,
			detail: firstError.Description
		);
	}
}
