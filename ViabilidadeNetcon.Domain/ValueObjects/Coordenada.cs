using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViabilidadeNetcon.Domain.ValueObjects
{
    public class Coordenada
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public Coordenada(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90)
                throw new ArgumentException("Latitude deve estar entre -90 e 90", nameof(latitude));

            if (longitude < -180 || longitude > 180)
                throw new ArgumentException("Longitude deve estar entre -180 e 180", nameof(longitude));

            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
