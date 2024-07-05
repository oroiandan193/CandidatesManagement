using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CandidatesManagement.Api.Middlewares
{
    internal sealed class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetailsService;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(IProblemDetailsService problemDetailsService, ILogger<GlobalExceptionHandler> logger)
        {
            _problemDetailsService = problemDetailsService;
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            ProblemDetails problemDetails;

            switch (exception) 
            {
                case ArgumentException _:
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Extensions = new Dictionary<string, object?>
                        {
                            { "Message", exception.Message }
                        }
                    };

                    _logger.LogError(exception, exception.Message);
                    break;
                case DbUpdateConcurrencyException concurrencyEx:
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Extensions = new Dictionary<string, object?>
                        {
                            { "Message", "The update record you attemped conflicts with another operation, please try again"}
                        }
                    };

                    break;

                case DbUpdateException _: //unique constraint violation

                    if (exception.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                    {
                        problemDetails = new ProblemDetails
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Extensions = new Dictionary<string, object?>
                            {
                                { "Message", "There is already a job candidate with this email"}
                            }
                        };
                        break;
                    }
                    //other constraint violation
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Extensions = new Dictionary<string, object?>
                        {
                            { "Message", "Could not perform the current operation"}
                        }
                    };

                    break;

                default:
                    problemDetails = new ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Extensions = new Dictionary<string, object?>
                        {
                            { "Message", "It seems we`ve encountered an unhandled error, we will look into this, please provide the correlationId for support" },
                            { "CorrelationId", "ToBeProvided" }
                        }
                    };
                    break;
            }

            await _problemDetailsService.WriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problemDetails,
                Exception = exception
            });

            return true;
        }
    }
}
