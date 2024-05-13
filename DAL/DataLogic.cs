using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace DAL;

public static class Logic
{
    private static readonly IConfiguration Configuration;

    static Logic()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true);

        Configuration = builder.Build();
    }

    public static List<IDictionary<string, object>> ExecuteQuery(string query, params MySqlParameter[] parameters)
    {
        var results = new List<IDictionary<string, object>>();

        using var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection"));
        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddRange(parameters);

        connection.Open();

        if (query.TrimStart().StartsWith("INSERT", StringComparison.OrdinalIgnoreCase))
        {
            command.ExecuteNonQuery();
            var lastInsertedId = Convert.ToInt32(command.LastInsertedId);
            results.Add(new Dictionary<string, object> { { "LastInsertedId", lastInsertedId } });
        }
        else
        {
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var result = new Dictionary<string, object>();
                for (var i = 0; i < reader.FieldCount; i++) result.Add(reader.GetName(i), reader.GetValue(i));
                results.Add(result);
            }
        }

        return results;
    }
}