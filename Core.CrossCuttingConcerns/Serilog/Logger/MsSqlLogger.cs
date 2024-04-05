using Core.CrossCuttingConcerns.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Serilog.Messages;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Core.CrossCuttingConcerns.Serilog.Logger;

public class MsSqlLogger : LoggerServiceBase
{
    public MsSqlLogger(IConfiguration configuration)
    {
        var logConfig = configuration.GetSection("SerilogConfigurations:MsSqlLogConfiguration")
            .Get<MsSqlLogConfiguration>() ?? throw new NullReferenceException(SerilogMessages.NullOptionsMessage);

        MSSqlServerSinkOptions sinkOptions = new()
        {
            TableName = logConfig.TableName,
            AutoCreateSqlTable = logConfig.AutoCreateSqlTable,
            AutoCreateSqlDatabase = logConfig.AutoCreateSqlDatabase
        };

        ColumnOptions columnOptions = new();

        Logger = new LoggerConfiguration().WriteTo.MSSqlServer(
            connectionString: logConfig.ConnectionString,
            sinkOptions: sinkOptions,
            columnOptions: columnOptions
        ).CreateLogger();
    }
}
