using System.Text;
using Demo.Libs.ConnectionStrings.Options;

namespace Demo.Libs.ConnectionStrings.Exceptions;

public class ConnectionStringNotFoundException : Exception
{
    public ConnectionStringNotFoundException(ConnectionStringOptions options, string? tenant, Exception? cause)
        :base(CreateMessage(options, tenant, cause), cause)
    { }

    private static string CreateMessage(ConnectionStringOptions options, string? tenant, Exception? cause)
    {
        var sb = new StringBuilder("The connection string was not found")
            .Append(", format: ").Append(options.Configurations.Format)
            .Append(", configuration key: ").Append(options.Configurations.ConfigurationKey)
            .Append(", tenant: ").Append(tenant ?? "'null'")
            .Append(", database: ").Append(options.Database ?? "'null'");

        if (cause is not null)
            sb.Append(", cause: ").Append(cause.Message);

        return sb.ToString();
    }
}