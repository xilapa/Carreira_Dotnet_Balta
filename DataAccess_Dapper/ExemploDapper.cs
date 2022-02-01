using System;
using System.Collections.Generic;
using System.Linq;
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

    public static async Task OneToOne() 
    {
        var dapperRepo = new DapperRepository(Constants.SQL_SERVER_CONNECTION_STRING);

        var query = @"SELECT * FROM CareerItem 
                    INNER JOIN Course ON CareerItem.CourseId = Course.Id";

        var result = await dapperRepo.UsingConnectionAsync(
            (conn, tran) =>
            {
                return conn.QueryAsync<CareerItem, Course, CareerItem>(query,
                    (carerItem, course) => {
                        carerItem.Course = course;
                        return carerItem;
                    }
                 , transaction: tran, splitOn: "Id");
            }
        );
    }

     public static async Task OneToMany() 
    {
        var dapperRepo = new DapperRepository(Constants.SQL_SERVER_CONNECTION_STRING);

        var query = @"SELECT Career.Id, Career.Title, CareerItem.CareerId, CareerItem.Title
                    FROM Career 
                    INNER JOIN CareerItem ON CareerItem.CareerId = Career.Id";

        var careers = new List<Career>();
        var result = await dapperRepo.UsingConnectionAsync(
            (conn, tran) =>
            {
                return conn.QueryAsync<Career, CareerItem, Career>(query,
                    (career, careerItem) => {
                        var actualCareer = careers.SingleOrDefault(c => c.Id == career.Id);
                        if (actualCareer is null)
                        {
                            actualCareer = career;
                            actualCareer.Items.Add(careerItem);
                            careers.Add(actualCareer);
                        }
                        else
                        {
                            actualCareer.Items.Add(careerItem);
                        }          
                        career.Items.Add(careerItem);
                        return career;
                    }
                 , transaction: tran, splitOn: "CareerId");
            }
        );
    }

    public static async Task MultipleQueries()
    {
        var dapperRepo = new DapperRepository(Constants.SQL_SERVER_CONNECTION_STRING);
        var query = "SELECT * FROM Course; SELECT * FROM Category;";

        IEnumerable<Course> courses;
        IEnumerable<Category> categories;
        await dapperRepo.UsingConnectionAsync(
            async (conn, tran) =>
            {
                using (var reader = await conn.QueryMultipleAsync(query, transaction: tran))
                {
                    courses = await reader.ReadAsync<Course>();
                    categories = await reader.ReadAsync<Category>();
                }                
            }
        );        

    } 

}