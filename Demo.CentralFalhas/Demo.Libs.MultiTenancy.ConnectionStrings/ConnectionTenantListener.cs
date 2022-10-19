using Demo.Libs.ConnectionStrings.Infrastructure;
using Demo.Libs.MultiTenancy.Abstractions;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

internal class ConnectionTenantListener : ITenantListener
{
    private readonly ConnectionTenant _connectionTenant;

    public ConnectionTenantListener(ConnectionTenant connectionTenant)
    {
        _connectionTenant = connectionTenant;
    }

    public void OnCurrentTenantAssigned(string tenantName)
    {
        _connectionTenant.Name = tenantName;
    }
}