using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baze2.Backend.Models
{
    public class Krava
    {
        public string IdZivotinje { get; set; }

        public string Ime { get; set; }

        public DateTime DatumRodjenja { get; set; }

        public string IdZivotinjeMajke { get; set; }

        public Pol Pol { get; set; }

        public Rasa Rasa { get; set; }

        public int TrenutnaKolicinaMleka { get; set; }
    }

    public class Rasa
    {
        public string Naziv { get; set; }

        public string Boja { get; set; }
    }

    public enum Pol
    {
        [PgName("M")]
        M = 0,
        [PgName("Z")]
        Z = 1,
    }

    // extensions method example
    static class PolExtensions
    {
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
