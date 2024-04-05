namespace Core.CrossCuttingConcerns.Serilog.ConfigurationModels;

public class MsSqlLogConfiguration
{
    public string TableName { get; set; }
    public string ConnectionString { get; set; }
    public bool AutoCreateSqlTable { get; set; }
    public bool AutoCreateSqlDatabase { get; set; }

    public MsSqlLogConfiguration()
    {
        TableName = string.Empty;
        ConnectionString = string.Empty;
        AutoCreateSqlTable = true;
        AutoCreateSqlDatabase = false;
    }

    public MsSqlLogConfiguration(string tableName, string connectionString, bool autoCreateSqlTable,
        bool autoCreateSqlDatabase)
    {
        TableName = tableName;
        ConnectionString = connectionString;
        AutoCreateSqlTable = autoCreateSqlTable;
        AutoCreateSqlDatabase = autoCreateSqlDatabase;
    }
}
