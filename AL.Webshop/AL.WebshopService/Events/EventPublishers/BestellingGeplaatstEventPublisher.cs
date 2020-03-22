using Minor.Miffy.MicroServices.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AL.WebshopService.Events.EventPublishers
{
    public class BestellingGeplaatstEventPublisher
    {
        private readonly IEventPublisher _eventPublisher;
        public BestellingGeplaatstEventPublisher(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void PublishBestellingGeplaatstEvent(BestellingGeplaatstEvent bestellingGeplaatstEvent)
        {
            _eventPublisher.Publish(bestellingGeplaatstEvent);
        }
    }
}
