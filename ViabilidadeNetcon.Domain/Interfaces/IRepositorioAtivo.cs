using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViabilidadeNetcon.Domain.Entities;

namespace ViabilidadeNetcon.Domain.Interfaces
{
    public interface IRepositorioAtivo
    {
        IEnumerable<Ativo> ObterTodos();
    }
}

