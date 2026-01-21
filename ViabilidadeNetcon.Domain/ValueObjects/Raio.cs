using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViabilidadeNetcon.Domain.ValueObjects
{
    public class Raio
    {
        public int Metros { get; }

        public Raio(int metros)
        {
            if (metros < 10 || metros > 1000)
                throw new ArgumentException("Raio deve estar entre 10 e 1000 metros", nameof(metros));

            Metros = metros;
        }
    }
}

