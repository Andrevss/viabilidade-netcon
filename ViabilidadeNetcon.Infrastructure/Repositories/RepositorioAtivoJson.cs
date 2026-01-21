using System.Text.Json;
using ViabilidadeNetcon.Domain.Entities;
using ViabilidadeNetcon.Domain.Interfaces;

namespace ViabilidadeNetcon.Infrastructure.Repositories
{
    public class RepositorioAtivoJson : IRepositorioAtivo
    {
        private readonly List<Ativo> _ativos;
        public RepositorioAtivoJson()
        {
            var caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "Data", "dataset.json");

            Console.WriteLine($"Tentando ler arquivo: {caminhoArquivo}");
            Console.WriteLine($"Arquivo existe? {File.Exists(caminhoArquivo)}");

            if (!File.Exists(caminhoArquivo))
            {
                Console.WriteLine($"BaseDirectory: {AppContext.BaseDirectory}");
                throw new FileNotFoundException($"Arquivo não encontrado: {caminhoArquivo}");
            }

            var json = File.ReadAllText(caminhoArquivo);
            Console.WriteLine($"JSON lido: {json}");

            var dados = JsonSerializer.Deserialize<List<AtivoJson>>(json);
            Console.WriteLine($"Total de ativos carregados: {dados?.Count ?? 0}");

            _ativos = dados.Select(d => new Ativo(
                d.id,
                d.nome,
                d.latitude,
                d.longitude
            )).ToList();
        }

        public IEnumerable<Ativo> ObterTodos()
        {
            Console.WriteLine($"ObterTodos retornando {_ativos.Count} ativos");
            return _ativos;
        }

        private class AtivoJson
        {
            public int id { get; set; }
            public string nome { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
        }
    }
}