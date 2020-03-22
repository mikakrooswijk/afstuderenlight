using AL.Bestellingen.Models;
using Minor.Miffy.MicroServices.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AL.Bestellingen.Events
{
    public class BestellingGeregistreedEvent : DomainEvent
    {
        [JsonProperty]
        public Bestelling Bestelling { get; set; }

        public BestellingGeregistreedEvent() : base("#.BestellingGeregistreed")
        {
        }
    }
}
