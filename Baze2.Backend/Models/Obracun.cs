using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baze2.Backend.Models
{
    public class Obracun
    {
        public string SifraObracuna { get; set; }

        public DateTime DatumObracuna { get; set; }

        public string PeriodOd { get; set; }

        public string PeriodDo { get; set; }

        public int Litara { get; set; }

        public string SifraMlekare { get; set; }

        public string NazivMlekare { get; set; }
    }
}
