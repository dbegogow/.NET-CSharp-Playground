using Microsoft.AspNetCore.Http;

namespace ApiKeyAuthentication.Authentication;

public class ApiKeyEndpointFilter : IEndpointFilter
{
    private readonly IConfiguration _configuration;

    public ApiKeyEndpointFilter(IConfiguration configuration)
        => this._configuration = configuration;

    public async ValueTask<object> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName,
            out var extractedApiKey))
        {
            return TypedResults.Unauthorized();
        }

        var apiKey = this._configuration.GetValue<string>(AuthConstants.ApiKeySectionName);

        if (!apiKey.Equals(extractedApiKey))
        {
            return TypedResults.Unauthorized();
        }

        return await next(context);
    }
}
