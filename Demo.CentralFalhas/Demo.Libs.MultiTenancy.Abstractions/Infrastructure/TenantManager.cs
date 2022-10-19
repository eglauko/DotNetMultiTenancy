using Microsoft.Extensions.Options;

namespace Demo.Libs.MultiTenancy.Abstractions.Infrastructure;

public class TenantManager : ITenantManager
{
    private readonly IOptions<MultiTenancyOptions> _options;
    private readonly IEnumerable<ITenantListener> _listeners;

    public TenantManager(IOptions<MultiTenancyOptions> options, IEnumerable<ITenantListener> listeners)
    {
        _options = options;
        _listeners = listeners;
    }

    public CurrentTenant CurrentTenant { get; } = new(); 
    
    public void SetCurrentTenant(string name)
    {
        CurrentTenant.Name = name ?? throw new ArgumentException(nameof(name));
        
        foreach (var listener in _listeners)
        {
            listener.OnCurrentTenantAssigned(name);
        }
    }

    public IEnumerable<string> GetAllTenants() => _options.Value.GetAllTenants();
}