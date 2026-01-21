
namespace ViabilidadeNetcon.Domain.Entities
{
    public class Ativo
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public Ativo(int id, string nome, double latitude, double longitude)
        {
            Id = id;
            Nome = nome;
            Latitude = latitude;
            Longitude = longitude;
        }

        public double CalcularDistancia(double latitudePonto, double longitudePonto)
        {
            const double raioTerrraKm = 6371; // Raio médio da Terra em km

            // Converte graus para radianos
            var lat1Rad = GrausParaRadianos(Latitude);
            var lat2Rad = GrausParaRadianos(latitudePonto);
            var deltaLat = GrausParaRadianos(latitudePonto - Latitude);
            var deltaLon = GrausParaRadianos(longitudePonto - Longitude);

            // Fórmula de Haversine
            var a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                    Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                    Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distanciaKm = raioTerrraKm * c;

            // Retorna em metros
            return distanciaKm * 1000;
        }

        private double GrausParaRadianos(double graus)
        {
            return graus * (Math.PI / 180);
        }
    }
}
