using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace DataAccess_Dapper;

class Program
{
    [SuppressMessage("Suggestion", "IDE0060: Remove unused parameter 'args'", Justification = "Não to afim")]
    static async Task Main(string[] args)
    {
        // await new ExemploADONet().Rodar();
        await ExemploDapper.Rodar();
    }
}

