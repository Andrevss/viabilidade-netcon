using ViabilidadeNetcon.Application.DTOs;
using ViabilidadeNetcon.Application.Services;
using ViabilidadeNetcon.Domain.Interfaces;
using ViabilidadeNetcon.Domain.ValueObjects;

public class ViabilidadeService : IViabilidadeService
{
    private readonly IRepositorioAtivo _repositorio;

    public ViabilidadeService(IRepositorioAtivo repositorio)
    {
        _repositorio = repositorio;
    }

    public IEnumerable<AtivoResponseDto> BuscarAtivosNoRaio(
        double latitude,
        double longitude,
        int raio)
    {
        var coordenada = new Coordenada(latitude, longitude);
        var raioObj = new Raio(raio);

        var ativos = _repositorio.ObterTodos();

        var ativosNoRaio = ativos
            .Select(ativo => new
            {
                Ativo = ativo,
                Distancia = ativo.CalcularDistancia(coordenada.Latitude, coordenada.Longitude)
            })
            .Where(x => x.Distancia <= raioObj.Metros)
            .OrderBy(x => x.Distancia)
            .Select(x => new AtivoResponseDto
            {
                Id = x.Ativo.Id,
                Nome = x.Ativo.Nome,
                Latitude = x.Ativo.Latitude,
                Longitude = x.Ativo.Longitude,
                Radius = Math.Round(x.Distancia, 2)
            });

        return ativosNoRaio;
    }
}
