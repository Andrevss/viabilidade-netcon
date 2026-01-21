using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViabilidadeNetcon.Domain.ValueObjects;

namespace ViabilidadeNetcon.Tests.Domain
{
    public class RaioTests
    {
        [Theory]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(500)]
        [InlineData(1000)]
        public void Raio_DeveAceitarValoresValidos(int metros)
        {
            // Act & Assert
            var raio = new Raio(metros);
            Assert.Equal(metros, raio.Metros);
        }

        [Theory]
        [InlineData(9)]
        [InlineData(1001)]
        [InlineData(0)]
        [InlineData(-100)]
        public void Raio_DeveRejeitarValoresInvalidos(int metros)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Raio(metros));
        }
    }
}
