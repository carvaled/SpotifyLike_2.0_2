using Dapper;
using SpotifyLike.STS.Data.Interfaces;
using SpotifyLike.STS.Data.Options;
using SpotifyLike.STS.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace SpotifyLike.STS.Data
{

    internal class IdentityRepository : IIdentityRepository
    {
        private readonly string? connectionString;

        public IdentityRepository(IOptions<DataBaseOptions> options)
        {
            this.connectionString = options.Value.DefaultConnectionString;
        }

        public async Task<User> FindByIdAsync(Guid Id)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var user = await connection.QueryFirstAsync<User>(IdentityQuery.FindById(), new { id = Id });
                return user;
            }
        }

        public async Task<User> FindByEmail(string email)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var user = await connection.QueryFirstOrDefaultAsync<User>(IdentityQuery.FindByEmail(), new { email = email });
                return user;
            }
        }
    }

    internal static class IdentityQuery
    {
        public static string FindById() =>
            @"SELECT [Id]
                ,[Email]
                ,[Nome]
          FROM [dbo].[Usuario] 
          WHERE id = @id";

        public static string FindByEmail() =>
            @"SELECT [Id]
                ,[Email]
                ,[Senha]
                ,[Nome]
          FROM [dbo].[Usuario]
          WHERE email = @email";
    }
}