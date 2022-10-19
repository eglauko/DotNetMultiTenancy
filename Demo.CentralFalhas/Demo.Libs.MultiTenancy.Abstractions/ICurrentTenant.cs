namespace Demo.Libs.MultiTenancy.Abstractions;

/// <summary>
/// Serviço que disponibiliza o Tenant do escopo atual.
/// </summary>
public interface ICurrentTenant
{
    /// <summary>
    /// Obtém o tenant atual.
    /// </summary>
    public string? Name { get; }
}