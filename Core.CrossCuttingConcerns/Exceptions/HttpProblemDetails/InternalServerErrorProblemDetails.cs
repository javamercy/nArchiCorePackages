using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public class InternalServerErrorProblemDetails : ProblemDetails
{
    public InternalServerErrorProblemDetails()
    {
        Title = "Internal Server Error";
        Detail = "An unexpected error occurred while processing your request.";
        Status = StatusCodes.Status500InternalServerError;
        Type = "https://example.com/probs/internal-server-error";
    }
}