using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViabilidadeNetcon.Application.DTOs
{
    public class AtivoResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Radius { get; set; } 
    }
}
