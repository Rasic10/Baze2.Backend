using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baze2.Backend.Models
{
    public class Zaposlen
    {
        public int sifraZaposlenog { get; set; }

        public string ime { get; set; }

        public string prezime { get; set; }

        public string jmbg { get; set; }

        public string brLicneKarte { get; set; }
    }
}
