﻿using Dapper;
using Identity.WebApp.Securitys;
using Microsoft.AspNetCore.Identity;
using System.Data.Common;
using System.Data.SqlClient;

namespace Identity.WebApp.Authorization
{
    public class MyUserStore : IUserStore<MyUser>, IUserPasswordStore<MyUser>
    {


        public async Task<IdentityResult> CreateAsync(MyUser user, CancellationToken cancellationToken)
        {
            using (var con = GetOpenConnection())
            {
                await con.ExecuteAsync("insert into Users (Id, UserName, NormalizedUserName, PasswordHash)" +
                    " values (@id, @userName, @normalizedUserName, @passwordHash)",
                    new
                    {
                        id = user.Id,
                        userName = user.UserName,
                        normalizedUserName = user.NormalizedUserName,
                        passwordHash = user.PasswordHash
                    });
            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(MyUser user, CancellationToken cancellationToken)
        {
            using (var con = GetOpenConnection())
            {
                await con.ExecuteAsync("update Users set UserName = @userName, NormalizedUserName = @normalizedUserName, PasswordHash = @passwordHash " +
                    "where Id = @id",
                    new
                    {
                        id = user.Id,
                        userName = user.UserName,
                        normalizedUserName = user.NormalizedUserName,
                        passwordHash = user.PasswordHash
                    });
            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(MyUser user, CancellationToken cancellationToken)
        {
            using (var con = GetOpenConnection())
            {
                await con.ExecuteAsync("delete from Users where Id = @id",
                    new
                    {
                        id = user.Id
                    });
            }
            return IdentityResult.Success;
        }

        public void Dispose()
        {

        }

        public async Task<MyUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            using (var con = GetOpenConnection())
            {
                return await con.QueryFirstOrDefaultAsync<MyUser?>("select * from Users where Id = @id", new { id = userId });
            }
        }

        public async Task<MyUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            using (var con = GetOpenConnection())
            {
                return await con.QueryFirstOrDefaultAsync<MyUser?>("select * from Users where NormalizedUserName LIKE @nome",
                    new { nome = normalizedUserName });
            }
        }



        public Task<string?> GetNormalizedUserNameAsync(MyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(MyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string?> GetUserNameAsync(MyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(MyUser user, string? normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(MyUser user, string? userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public static DbConnection GetOpenConnection()
        {
            var connection = new SqlConnection(@"Data Source=RMX\SQLEXPRESS;Initial Catalog=identityCurso;Integrated Security=True;");

            return connection;
        }




        public Task SetPasswordHashAsync(MyUser user, string? passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string?> GetPasswordHashAsync(MyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(MyUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(
                !string.IsNullOrEmpty(user.PasswordHash)
                );
        }
    }
}
