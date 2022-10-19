namespace Demo.Libs.MultiTenancy.Abstractions.Infrastructure;

public class CurrentTenant : ICurrentTenant
{
    public string? Name { get; set; }
}
