using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class ProblemDetailsTranslator : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetails;
    private readonly ILogger<ProblemDetailsTranslator> _logger;

    public ProblemDetailsTranslator(IProblemDetailsService problemDetails, ILogger<ProblemDetailsTranslator> logger)
    {
        _problemDetails = problemDetails;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is DomainException domain)
        {
            httpContext.Response.StatusCode = (int)domain.Code;
            return await _problemDetails.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails =
                {
                    Title = "An error occurred",
                    Detail = domain.Message,
                    Status = (int)domain.Code
                },
                Exception = domain
            });
        }

        return false;
    }
}
