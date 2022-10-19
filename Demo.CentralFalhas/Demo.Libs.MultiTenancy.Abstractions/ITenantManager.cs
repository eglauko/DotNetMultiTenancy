namespace Demo.Libs.MultiTenancy.Abstractions;

/// <summary>
/// Serviço para gerenciamento de Tenants.
/// </summary>
public interface ITenantManager
{
    /// <summary>
    /// Atribui o Tenant do escopo atual.
    /// </summary>
    /// <param name="name"></param>
    void SetCurrentTenant(string name);

    /// <summary>
    /// Obtém todos Tenants configurados para o aplicativo.
    /// </summary>
    /// <returns></returns>
    IEnumerable<string> GetAllTenants();
}