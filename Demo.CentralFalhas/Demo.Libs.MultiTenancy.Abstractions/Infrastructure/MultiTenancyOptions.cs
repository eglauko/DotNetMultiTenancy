
namespace Demo.Libs.MultiTenancy.Abstractions.Infrastructure;

/// <summary>
/// Opções de MultiTenancy.
/// </summary>
public class MultiTenancyOptions
{
    private const string TenantDefaultKey = "tenant";
    
    private string[]? _tenantsNames;

    /// <summary>
    /// <para>
    ///     Chave para identificação do tenant.
    /// </para>
    /// <para>
    ///     Pode ser utilizado em middlewares, filtros, decoradores, etc., para identificar o tenant atual.
    /// </para>
    /// </summary>
    public string Key { get; set; } = TenantDefaultKey;
    
    /// <summary>
    /// Obtém todos os tenants configurados/conhecidos.
    /// </summary>
    /// <returns></returns>
    public string[] GetAllTenants() => _tenantsNames ?? Array.Empty<string>();

    /// <summary>
    /// <para>
    ///     Atribui os Tenants conhecidos através de uma strings,
    ///     separando os nomes por vírgula (,) ou por ponto e vírgula (;),
    ///     sem espaços.
    /// </para> 
    /// </summary>
    /// <param name="tenants">Nomes dos tenants.</param>
    /// <exception cref="ArgumentNullException">
    ///     Se <paramref name="tenants"/> for nulo.
    /// </exception>
    public void SetTenants(string tenants)
    {
        if (tenants is null)
            throw new ArgumentNullException(nameof(tenants));

        if (tenants.Contains(','))
            _tenantsNames = tenants.Split(',');
        else if (tenants.Contains(';'))
            _tenantsNames = tenants.Split(';');
        else
            _tenantsNames = new[] { tenants };
    }

    /// <summary>
    /// Adiciona um novo tenant.
    /// </summary>
    /// <param name="newTenantName">Nome do novo tenant.</param>
    public void AddTenant(string newTenantName)
    {
        if (_tenantsNames is null)
        {
            _tenantsNames = new[] { newTenantName };
        }
        else
        {
            var aux = new string[_tenantsNames.Length + 1];
            Array.Copy(_tenantsNames, aux, _tenantsNames.Length);
            aux[^1] = newTenantName;
            
            _tenantsNames = aux;
        }
    }
}