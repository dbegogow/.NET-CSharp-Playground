namespace GlobalExceptionsHandling.Configurations;

public static class ApplicationBuilderExceptions
{
    public static IApplicationBuilder AddGlobalErrorHandler(
        this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
}
