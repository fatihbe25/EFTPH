using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Medewerker
    {
        public int AfdelingId { get; set; }

        //migratie fout
        public virtual Afdeling Afdeling { get; set; }

    }

    public class Afdeling
    {
        public int AfdelingId { get; set; }
        public string AfdelingCode { get; set; }
        public string AfdelingNaam { get; set; }
        public string AfdelingTekst { get; set; }

        public virtual ICollection<Medewerker> Medewerkers { get; set; }
    }

}
