using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViabilidadeNetcon.Infrastructure.Security
{
    public interface IJwtService
    {
        string GerarToken(string username);
        bool ValidarCredenciais(string username, string password);
    }
}
