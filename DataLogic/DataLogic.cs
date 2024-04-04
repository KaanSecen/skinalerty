using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace DataLogic
{
    public static class Logic
    {
        private static readonly IConfiguration Configuration;

        static Logic()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public static List<IDictionary<string, object>> ExecuteQuery(string query, params MySqlParameter[] parameters)
        {
            var results = new List<IDictionary<string, object>>();

            using var connection = new MySqlConnection(Configuration.GetConnectionString("DefaultConnection"));
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddRange(parameters);

            connection.Open();

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var result = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    result.Add(reader.GetName(i), reader.GetValue(i));
                }
                results.Add(result);
            }

            return results;
        }
    }
}