using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViabilidadeNetcon.Domain.Entities;
using Xunit;

namespace ViabilidadeNetcon.Tests.Domain
{
    public class AtivoTests
    {
        [Fact]
        public void CalcularDistancia_DeveFuncionarCorretamenteParaPontosProximos()
        {
            // Arrange
            var ativo = new Ativo(1, "CTO-001", -23.556456, -46.635653);
            var latPonto = -23.556500;
            var lonPonto = -46.635700;

            // Act
            var distancia = ativo.CalcularDistancia(latPonto, lonPonto);

            // Assert
            Assert.True(distancia < 10); // Deve ser muito próximo (menos de 10 metros)
        }

        [Fact]
        public void CalcularDistancia_DeveRetornarZeroParaMesmaCoordenada()
        {
            // Arrange
            var ativo = new Ativo(1, "CTO-001", -23.556456, -46.635653);

            // Act
            var distancia = ativo.CalcularDistancia(-23.556456, -46.635653);

            // Assert
            Assert.Equal(0, distancia, 1); // Tolerância de 1 metro
        }
    }
}
