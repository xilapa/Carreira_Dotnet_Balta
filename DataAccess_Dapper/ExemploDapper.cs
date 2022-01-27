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
                            VALUES(@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

        #region SINGLE INSERT
        var categoria = new Category
                                    {
                                        Id = Guid.NewGuid(),
                                        Title = "Categoria Teste 5 - com transaction",
                                        Url = "link-do-bom.com",
                                        Summary = "muito bom",
                                        Order = 1,
                                        Description = "sem palavras pra essa categoria",
                                        Featured = true
                                    };

        var singleInsertResult = await dapperRepo
                                    .UsingConnectionAsync(
                                        (conn, tran) => conn.ExecuteAsync(queryInsert, categoria, tran)
                                    );
        #endregion
        
        #region LIST INSERT
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
                                        (conn, tran) => conn.ExecuteAsync(queryInsert, listaCategorias, tran)
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
}