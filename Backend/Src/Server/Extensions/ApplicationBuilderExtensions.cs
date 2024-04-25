namespace Server.Extensions;

internal static class ApplicationBuilderExtensions
{
    internal static void ConfigureSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}