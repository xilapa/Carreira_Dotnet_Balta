

using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace DataAccess_Dapper.Repositories;

public class DapperRepository
{
    private readonly string _connectionString;
    public DapperRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    /// <summary>
    /// A func passada já é esperada dentro do método, assim não é nescessário
    /// passar uma func <see langword="async"/>.
    /// </summary>
    /// <typeparam name="T">Tipo do retorno</typeparam>
    /// <param name="func">Consulta usando IDbConnection e retornando uma Task de <typeparamref name="T"/></param>
    /// <returns>Task de <typeparamref name="T"/></returns>
    [SuppressMessage("Suggestion", "IDE0063: 'using' statement can be simplified", Justification = "Using simplicado dificulta o entendimento do código")]
    public async Task<T> UsingConnectionAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> func)
    {
        using(var connection = new SqlConnection(_connectionString))
        {
            if(connection.State == ConnectionState.Closed)
                await connection.OpenAsync();

            var transaction = await connection.BeginTransactionAsync();

            try
            {
                var result = await func(connection, transaction);
                await transaction.CommitAsync();
                return result;
            }
            catch(Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }            
        }
    }
}