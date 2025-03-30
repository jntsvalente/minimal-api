using Dapper;
using Web.Api.Models;
using Web.Api.Services;

namespace Web.Api.Endpoints
{
    public static class CustomerEndpoints
    {
        public static void MapCustomerEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("v1/customers");

            group.MapGet("", async (ISqlConnectionFactory sqlConnectionFactory) =>
            {
                using var connection = sqlConnectionFactory.Create();

                const string sql = "SELECT Id, FistName, LastName, DateOfBirth FROM Customer";

                var customers = await connection.QueryAsync<Customer>(sql);

                return Results.Ok(customers);
            });

            group.MapGet("{customerId:int}", async (int customerId, ISqlConnectionFactory sqlConnectionFactory) =>
            {
                using var connection = sqlConnectionFactory.Create();

                const string sql = """
                SELECT Id, FistName, LastName, DateOfBirth
                FROM Customer
                WHERE Id = @CustomerId
                """;

                var customer = await connection.QuerySingleOrDefaultAsync<Customer>(
                    sql,
                    new { CustomerId = customerId });

                return customer is not null ? Results.Ok(customer) : Results.NotFound();
            });

            group.MapPost("", async (Customer customer, ISqlConnectionFactory sqlConnectionFactory) =>
            {
                using var connection = sqlConnectionFactory.Create();

                const string sql = """
                    INSERT INTO Customer (FirstName, LastName, Email, DateOfBirth)
                    VALUES (@FirstName, @LastName, @Email, @DateOfBirth)
                """;

                await connection.ExecuteAsync(sql, customer);

                return Results.Ok();
            });

            group.MapPut("{customerId:int}", async (int customerId, Customer customer, ISqlConnectionFactory sqlConnectionFactory) =>
            {
                using var connection = sqlConnectionFactory.Create();

                customer.Id = customerId;

                const string sql = """
                    UPDATE Customer
                    SET FirstName = @FirstName,
                        LastName = @LastName,
                        Email = @Email,
                        DateOfBirth = @DateOfBirth
                    WHERE Id = @Id
                """;

                await connection.ExecuteAsync(sql, customer);

                return Results.NoContent();
            });

            group.MapDelete("{customerId:int}", async (int customerId, Customer customer, ISqlConnectionFactory sqlConnectionFactory) =>
            {
                using var connection = sqlConnectionFactory.Create();

                const string sql = "DELETE FROM Customer WHERE Id = @CustomerId";

                await connection.ExecuteAsync(sql, new { CustomerId = customerId });

                return Results.NoContent();
            });
        }
    }
}
