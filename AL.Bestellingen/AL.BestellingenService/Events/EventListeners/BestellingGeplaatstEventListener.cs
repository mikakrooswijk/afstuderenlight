using AL.Bestellingen.Events.EventPublisher;
using AL.WebshopService.DAL;
using Microsoft.EntityFrameworkCore;
using Minor.Miffy.MicroServices.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AL.Bestellingen.Events.EventListeners
{
    public class BestellingGeplaatstEventListener
    {
        private readonly WebshopContext _webshopContext;
        private readonly BestellingGeregistreerdEventPublisher _eventPublisher;
        public BestellingGeplaatstEventListener(WebshopContext webshopContext, BestellingGeregistreerdEventPublisher eventPublisher)
        {
            _webshopContext = webshopContext;
            _eventPublisher = eventPublisher;
        }

        [EventListener]
        [Topic("#.BestellingGeplaatst")]
        public void Handle(BestellingGeregistreedEvent bestellingGeplaatstEvent)
        {
            bestellingGeplaatstEvent.Bestelling.status = BestellingStatus.Geplaatst;
            bestellingGeplaatstEvent.Bestelling.BestellingId = 0;
            _webshopContext.Bestellingen.Add(bestellingGeplaatstEvent.Bestelling);

            _webshopContext.SaveChanges();

            var regEvent = new BestellingGeregistreedEvent()
            {
                Bestelling = bestellingGeplaatstEvent.Bestelling
            };

            _eventPublisher.PublishBestellingGeregistreedEvent(regEvent);

        }
    }
}
