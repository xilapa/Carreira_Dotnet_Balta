using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using DataAccess_Dapper.Models;
using DataAccess_Dapper.Repositories;
using Microsoft.Data.SqlClient;

namespace DataAccess_Dapper;

public class ExemploADONet
{
    [SuppressMessage("Suggestion", "IDE0039: Use local function", Justification = "O intuito é tipar o delegate.")]
    public static async Task Rodar()
    {        
        var repoAdoNet = new AdoNetRepository(Constants.SQL_SERVER_CONNECTION_STRING);
        var query = "SELECT Id, Title FROM Category";

        Console.WriteLine("Sem tipar retorno");
        Action<SqlDataReader> actiona = (reader) => Console.WriteLine($"{reader.GetGuid(0)} - {reader.GetString(1)}");
        await repoAdoNet.UsingConnectionAsync(query, actiona);

        Console.WriteLine("\nCom retorno tipado");
        Func<SqlDataReader, Category> func = (reader) => new Category { Id = reader.GetGuid(0), Title = reader.GetString(1)};
        var categories = await repoAdoNet.UsingConnectionAsync(query, func);
        foreach (var category in categories)
        {
            Console.WriteLine($"{category.Id} - {category.Title}");
        }
    }
}