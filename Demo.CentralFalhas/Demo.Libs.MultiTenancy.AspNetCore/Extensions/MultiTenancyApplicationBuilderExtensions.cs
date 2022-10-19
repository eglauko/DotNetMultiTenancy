using Demo.Libs.MultiTenancy.AspNetCore;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Extensions methods for <see cref="IApplicationBuilder"/>.
/// </summary>
public static class MultiTenancyApplicationBuilderExtensions
{
    /// <summary>
    /// Utiliza o Middleware de Multitenancy, para atribuir o tenant do escopo atual.
    /// </summary>
    /// <param name="app">The AspNetCore application builder.</param>
    public static void UseMultiTenancy(this IApplicationBuilder app)
    {
        app.UseMiddleware<MultiTenancyMiddleware>();
    }
}