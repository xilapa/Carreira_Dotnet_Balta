using System;
using System.Threading.Tasks;
using Dapper;
using DataAccess_Dapper.Models;
using DataAccess_Dapper.Repositories;

namespace DataAccess_Dapper;

public class ExemploDapper
{
    public static async Task Rodar()
    {
        var dapperRepo = new DapperRepository(Constants.SQL_SERVER_CONNECTION_STRING);

        var queryInsert = @"INSERT INTO Category
                            OUTPUT inserted.Id
                            VALUES(NEWID(), @Title, @Url, @Summary, @Order, @Description, @Featured);";

        #region SINGLE INSERT RETORNANDO O ID
        var categoria = new Category
                                    {
                                        Title = "Categoria Teste retornando Id",
                                        Url = "link-do-bom.com",
                                        Summary = "muito bom",
                                        Order = 1,
                                        Description = "sem palavras pra essa categoria",
                                        Featured = true
                                    };

        var IdInserido = await dapperRepo
                                    .UsingConnectionAsync(
                                        (conn, tran) => conn.ExecuteScalarAsync<Guid>(queryInsert, categoria, tran)
                                    );
        Console.WriteLine($"O Id inserido foi: {IdInserido}");
        #endregion

        #region LIST INSERT

        var queryInsertlist = @"INSERT INTO Category
                                VALUES(@Id, @Title, @Url, @Summary, @Order, @Description, @Featured);";

        var listaCategorias = new Category[] {
            new Category {
                    Id = Guid.NewGuid(),
                    Title = "Categoria Teste 9 - com transaction e lista",
                    Url = "link-do-bom.com",
                    Summary = "muito bom",
                    Order = 1,
                    Description = "sem palavras pra essa categoria",
                    Featured = true
            } ,
            new Category {
                    Id = Guid.NewGuid(),
                    Title = "Categoria Teste 10 - com transaction e lista",
                    Url = "link-do-bom.com",
                    Summary = "muito bom",
                    Order = 1,
                    Description = "sem palavras pra essa categoria",
                    Featured = true
            }
        };

        var listInsertResult =  await dapperRepo
                                    .UsingConnectionAsync(
                                        (conn, tran) => conn.ExecuteAsync(queryInsertlist, listaCategorias, tran)
                                    );
        #endregion

        #region LEITURA
        var queryLeitura = "SELECT Id, Title FROM Category";
        var categories = await dapperRepo
                                    .UsingConnectionAsync(
                                        (conn, tran) => conn.QueryAsync<Category>(queryLeitura, null, tran)
                                    );

        foreach (var category in categories)
        {
            Console.WriteLine($"{category.Id} - {category.Title}");
        }
        #endregion
    }

    public static async Task MultiplasConexoesAoBanco()
    {
        var tarefa_um = Task.Run( async () => 
        {
            return await DapperRepository
            .UsingStaticConnectionAsync(Constants.SQL_SERVER_CONNECTION_STRING, 
            (connection, transaction) => {
                return connection.QueryAsync("SELECT Id, Title FROM Category", param: null, transaction);
            }
            );
        });

        var tarefa_dois = Task.Run(async () =>
        {
            return await DapperRepository
                .UsingStaticConnectionAsync(Constants.SQL_SERVER_CONNECTION_STRING,
                (connection, transaction) =>
                {
                    return connection.QueryAsync("SELECT Id, Name FROM Author", param: null, transaction);
                }
                );
        });

        var tarefa_tres = DapperRepository
            .UsingStaticConnectionAsync(Constants.SQL_SERVER_CONNECTION_STRING, 
            (connection, transaction) => {
                return connection.QueryAsync("SELECT Id, Title FROM Career", param: null, transaction);
            }
            );

        var tarefa_quatro = DapperRepository
            .UsingStaticConnectionAsync(Constants.SQL_SERVER_CONNECTION_STRING, 
            (connection, transaction) => {
                return connection.QueryAsync("SELECT Id, Title FROM Course", param: null, transaction);
            }
            );

        var results = await Task.WhenAll(tarefa_um, tarefa_dois, tarefa_tres, tarefa_quatro);

    }   
}