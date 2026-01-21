using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViabilidadeNetcon.Application.DTOs;

namespace ViabilidadeNetcon.Application.Services
{
    public interface IViabilidadeService
    {
        IEnumerable<AtivoResponseDto> BuscarAtivosNoRaio(
            double latitude,
            double longitude,
            int raio);
    }
}
