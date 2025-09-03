using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Taran.Shared.Core.Repository;
using Taran.Shared.Infrastructure.Configurations;
using System.Data;

namespace Taran.Shared.Infrastructure.Repositories;

public class RawQueryExecuter : IRawQueryExecuter
{
    private readonly ConnectionStringsConfiguration connectionStringConfiguration;

    public RawQueryExecuter(IOptions<ConnectionStringsConfiguration> connectionStringConfiguration)
    {
        this.connectionStringConfiguration = connectionStringConfiguration.Value;
    }

    public string Execute(string query)
    {
        DataTable dataTable = new DataTable();

        using (SqlConnection connection = new(connectionStringConfiguration.ApplicationDb))
        {
            using (SqlDataAdapter adapter = new(query, connection))
            {
                connection.Open();
                adapter.Fill(dataTable);
            }
        }

        var paginatedData = dataTable.AsEnumerable();

        JArray objectsArray = new JArray();

        foreach (DataRow row in dataTable.Rows)
        {
            JObject obj = new();

            foreach (DataColumn col in dataTable.Columns)
            {
                obj[col.ColumnName] = row[col.ColumnName].ToString();
            }

            objectsArray.Add(obj);
        }

        return objectsArray.ToString();
    }
}