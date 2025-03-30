using Microsoft.Data.SqlClient;

namespace Web.Api.Services
{
    public interface ISqlConnectionFactory
    {
        public SqlConnection Create();
    }

    public class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection Create()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
