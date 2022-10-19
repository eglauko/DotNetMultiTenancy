namespace Demo.Libs.ConnectionStrings;

/// <summary>
/// <para>
///     Provider de strings de conexão para multi-tenants.
/// </para>
/// </summary>
public interface IConnectionStringProvider
{
    /// <summary>
    /// Get the connection string for the current tenant database.
    /// </summary>
    /// <returns>The connection string.</returns>
    /// <exception cref="ArgumentException">
    /// <para>
    ///     If there none connection string configured for the Tenent.
    /// </para>
    /// </exception>
    string GetConnectionString();
}

/// <summary>
/// <para>
///     Um <see cref="IConnectionStringProvider"/> para aplicativos com acesso a multiplos bancos de dados.
/// </para>
/// </summary>
/// <typeparam name="TContext">Um tipo que define o contexto para uma base de dados.</typeparam>
public interface IConnectionStringProvider<TContext> : IConnectionStringProvider { }