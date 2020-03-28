using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AL.WebshopService.DAL;
using AL.WebshopService.Events;
using AL.WebshopService.Events.EventPublishers;
using AL.WebshopService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AL.WebshopService.Controllers
{
    [Route("api/[controller]")]

    public class BestellingenController : Controller
    {
        private WebshopContext _webshopContext;
        private IBestellingGeplaatstEventPublisher _bestellingGeplaatstEventPublisher;

        public BestellingenController(WebshopContext webshopContext, IBestellingGeplaatstEventPublisher eventPublisher)
        {
            _bestellingGeplaatstEventPublisher = eventPublisher;
            _webshopContext = webshopContext;
        }


        public JsonResult Index()
        {
            return Json(_webshopContext.Bestellingen.ToList());
        }

        [HttpPost]
        public IActionResult Create([FromBody]Bestelling bestelling)
        {
            bestelling.BestelDatum = DateTime.Now;
            bestelling.status = BestellingStatus.InBehandling;
            bestelling.BestellingNummer = Guid.NewGuid();
            _webshopContext.Bestellingen.Add(bestelling);
            _webshopContext.SaveChanges();

            var bestellingEvent = new BestellingGeplaatstEvent()
            {
                Bestelling = bestelling,

            };

            _bestellingGeplaatstEventPublisher.PublishBestellingGeplaatstEvent(bestellingEvent);
            return Ok();
        }
    }
}