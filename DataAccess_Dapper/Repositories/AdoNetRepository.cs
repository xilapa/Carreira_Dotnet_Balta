using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;
using System;

namespace DataAccess_Dapper.Repositories;

public class AdoNetRepository
{
    private readonly string _connectionString;
    public AdoNetRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task UsingConnectionAsync(string query, Action<SqlDataReader> action)
    {
        using(var connection = new SqlConnection(_connectionString))
        using(var command = new SqlCommand(query, connection))
        {
            await connection.OpenAsync();
            command.CommandType = CommandType.Text;
            var reader = await command.ExecuteReaderAsync();
            
            // SQLDataReader possui um cursor que avança os dados, linha a linha
            // Não tem como voltar, só avançar
            while(reader.Read())
            {
                // Para ler cada dado basta informar a coluna
                action(reader);
            }
        }
    }

    public async Task<List<T>> UsingConnectionAsync<T>(string query, Func<SqlDataReader, T> func)
    {
        var retorno = new List<T>();
        using(var connection = new SqlConnection(_connectionString))
        using(var command = new SqlCommand(query, connection))
        {
            await connection.OpenAsync();
            command.CommandType = CommandType.Text;
            var reader = await command.ExecuteReaderAsync();
            
            while(reader.Read())
            {
                retorno.Add(func(reader));
            }
        }
        return retorno;
    }
}