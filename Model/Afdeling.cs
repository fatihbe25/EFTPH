namespace Model
{
    public class Afdeling
    {
        public int AfdelingId { get; set; }
        public string AfdelingCode { get; set; }
        public string AfdelingNaam { get; set; }
        public string AfdelingTekst { get; set; }

        public virtual ICollection<Medewerker> Medewerkers { get; set; }
    }

}
