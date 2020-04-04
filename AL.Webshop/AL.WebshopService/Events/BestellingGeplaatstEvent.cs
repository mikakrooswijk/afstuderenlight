using AL.WebshopService.Models;
using Minor.Miffy.MicroServices.Events;
using Newtonsoft.Json;

namespace AL.WebshopService.Events
{
    public class BestellingGeplaatstEvent: DomainEvent
    {
        [JsonProperty]
        public Bestelling Bestelling { get; set; }

        public BestellingGeplaatstEvent() : base("#.BestellingGeplaatst")
        {
        }
    }
}
