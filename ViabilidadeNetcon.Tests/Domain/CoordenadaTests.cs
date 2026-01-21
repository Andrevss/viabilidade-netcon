using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViabilidadeNetcon.Domain.ValueObjects;

namespace ViabilidadeNetcon.Tests.Domain
{
    public class CoordenadaTests
    {
        [Fact]
        public void Coordenada_DeveAceitarValoresValidos()
        {
            // Act & Assert
            var coordenada = new Coordenada(-23.556456, -46.635653);
            Assert.Equal(-23.556456, coordenada.Latitude);
            Assert.Equal(-46.635653, coordenada.Longitude);
        }

        [Theory]
        [InlineData(-91, -46.635653)]
        [InlineData(91, -46.635653)]
        [InlineData(-100, -46.635653)]
        public void Coordenada_DeveRejeitarLatitudeInvalida(double lat, double lon)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Coordenada(lat, lon));
        }

        [Theory]
        [InlineData(-23.556456, -181)]
        [InlineData(-23.556456, 181)]
        [InlineData(-23.556456, 200)]
        public void Coordenada_DeveRejeitarLongitudeInvalida(double lat, double lon)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Coordenada(lat, lon));
        }
    }
}