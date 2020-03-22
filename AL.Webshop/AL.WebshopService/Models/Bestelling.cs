using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AL.WebshopService.Models
{
    public class Bestelling
    {
        public int BestellingId { get; set; }
        public Guid BestellingNummer { get; set; }
        public string Klant { get; set; }
        public string Product { get; set; }
        public DateTime BestelDatum { get; set; }
        public BestellingStatus status {get; set;}
    }
}
