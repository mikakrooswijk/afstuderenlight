using Minor.Miffy.MicroServices.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AL.Bestellingen.Events.EventPublisher
{
    public class BestellingGeregistreerdEventPublisher
    {
        private readonly IEventPublisher _eventPublisher;
        public BestellingGeregistreerdEventPublisher(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }
        public void PublishBestellingGeregistreedEvent(BestellingGeregistreedEvent bestellingGeregistreerdEvent)
        {
            _eventPublisher.Publish(bestellingGeregistreerdEvent);
        }
    }
}
