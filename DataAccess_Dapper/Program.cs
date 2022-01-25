using System.Threading.Tasks;

namespace DataAccess_Dapper;

class Program
{

    static async Task Main(string[] args)
    {
        await new ExemploADONet().Rodar();
    }
}

