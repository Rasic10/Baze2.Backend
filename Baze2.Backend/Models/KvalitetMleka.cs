using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baze2.Backend.Models
{
    public class KvalitetMleka
    {
        public DateTime datumKvaliteta { get; set; }

        public int rbParametra { get; set; }

        public int rbPodatakaOMuzi { get; set; }

        public string idZivotinje { get; set; }

        public DateTime datumPodaciKvaliteta { get; set; }

        public int kolicinaMleka { get; set; }
    }
}
