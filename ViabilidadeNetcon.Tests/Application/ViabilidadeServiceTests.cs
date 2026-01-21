using Moq;
using ViabilidadeNetcon.Domain.Entities;
using ViabilidadeNetcon.Domain.Interfaces;
using ViabilidadeNetcon.Application.Services;
using Xunit;

namespace ViabilidadeNetcon.Tests.Application
{
    public class ViabilidadeServiceTests
    {
        [Fact]
        public void BuscarAtivosNoRaio_DeveRetornarApenasAtivosNoRaio()
        {
            // Arrange
            var mockRepo = new Mock<IRepositorioAtivo>();
            var ativos = new List<Ativo>
            {
                new Ativo(1, "CTO-001", -23.556456, -46.635653),
                new Ativo(2, "CTO-002", -23.600000, -46.700000)
            };
            mockRepo.Setup(r => r.ObterTodos())
                    .Returns(ativos);
            var service = new ViabilidadeService(mockRepo.Object);
            // Act
            var resultado = service.BuscarAtivosNoRaio(-23.556456, -46.635653, 100);
            // Assert
            var listaResultado = resultado.ToList();
            Assert.Single(listaResultado);
            Assert.Equal(1, listaResultado.First().Id);
        }

        [Fact]
        public void BuscarAtivosNoRaio_DeveRetornarListaVaziaSeNenhumAtivoNoRaio()
        {
            // Arrange
            var mockRepo = new Mock<IRepositorioAtivo>();
            var ativos = new List<Ativo>
            {
                new Ativo(1, "CTO-001", -23.600000, -46.700000)
            };
            mockRepo.Setup(r => r.ObterTodos())
                    .Returns(ativos);
            var service = new ViabilidadeService(mockRepo.Object);
            // Act
            var resultado = service.BuscarAtivosNoRaio(-23.556456, -46.635653, 10);
            // Assert
            Assert.Empty(resultado);
        }

        [Fact]
        public void BuscarAtivosNoRaio_DeveLancarExcecaoParaLatitudeInvalida()
        {
            // Arrange
            var mockRepo = new Mock<IRepositorioAtivo>();
            var service = new ViabilidadeService(mockRepo.Object);
            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => service.BuscarAtivosNoRaio(100, -46.635653, 100));
        }
    }
}