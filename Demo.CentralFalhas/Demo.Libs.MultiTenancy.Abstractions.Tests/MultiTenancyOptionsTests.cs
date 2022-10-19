using Demo.Libs.MultiTenancy.Abstractions.Infrastructure;
using FluentAssertions;

namespace Demo.Libs.MultiTenancy.Abstractions.Tests;

public class MultiTenancyOptionsTests
{
    [Fact]
    public void GetAllTenats_Must_NotReturnNull_And_BeEmpty_When_NotConfigured()
    {
        // prepare
        var options = new MultiTenancyOptions();

        // act
        var names = options.GetAllTenants();
        
        // assert
        names.Should().NotBeNull().And.BeEmpty();
    }

    [Theory]
    [InlineData("A", 1)]
    [InlineData("A,B", 2)]
    [InlineData("A;B", 2)]
    public void SetTenants_Must_Accept_Comma_And_Semicolon(string names, int qtd)
    {
        // prepare
        var options = new MultiTenancyOptions();
        
        // act
        options.SetTenants(names);
        
        // assert
        var tenants = options.GetAllTenants();
        tenants.Should().HaveCount(qtd);
    }

    [Theory]
    [InlineData(null, "new", 1)]
    [InlineData("A", "new", 2)]
    [InlineData("A,B", "new", 3)]
    public void AddTenant_Must_IncrementTenatsCount_And_BeTheLast(string? names, string newTenantName, int qtd)
    {
        // prepare
        var options = new MultiTenancyOptions();
        if (names is not null)
            options.SetTenants(names);
        
        // act
        options.AddTenant(newTenantName);
        
        // assert
        var tenants = options.GetAllTenants();
        tenants.Should().HaveCount(qtd);
        tenants.Last().Should().Be(newTenantName);
    }
}