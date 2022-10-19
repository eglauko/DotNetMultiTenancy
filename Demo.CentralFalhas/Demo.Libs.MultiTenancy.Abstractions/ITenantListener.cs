namespace Demo.Libs.MultiTenancy.Abstractions;

/// <summary>
/// <para>
///     Interface para componentes que queiram escutar eventos do <see cref="ITenantManager"/>.
/// </para>
/// <para>
///     Se estiver utilizadno injeção de dependência,
///     implemente esta interface e registre-a como serviço de <see cref="ITenantListener"/>.
/// </para>
/// </summary>
public interface ITenantListener
{
    /// <summary>
    /// <para>
    ///     Executed 
    /// </para>
    /// </summary>
    /// <param name="tenantName"></param>
    void OnCurrentTenantAssigned(string tenantName);
}