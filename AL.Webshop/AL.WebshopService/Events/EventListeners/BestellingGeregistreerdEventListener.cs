using AL.WebshopService.DAL;
using Minor.Miffy.MicroServices.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AL.WebshopService.Events
{
    
    public class BestellingGeregistreerdEventListener
    {
        private readonly WebshopContext _webshopContext;
        public BestellingGeregistreerdEventListener(WebshopContext webshopContext)
        {
            _webshopContext = webshopContext;
        }

        [EventListener]
        [Topic("#.BestellingGeregistreed")]
        public void Handle(BestellingGeregistreedEvent bestellingGeregistreerdEvent)
        {
            var bestelling = _webshopContext.Bestellingen.SingleOrDefault(b => b.BestellingNummer == bestellingGeregistreerdEvent.Bestelling.BestellingNummer);

            if(bestelling != null)
            {
                bestelling.status = BestellingStatus.Geplaatst;
                _webshopContext.SaveChanges();
            }
        }


    }
}
