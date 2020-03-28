using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AL.BestellingenService.DAL;
using Microsoft.AspNetCore.Mvc;

namespace AL.Bestellingen.Controllers
{
    [Route("api/[controller]")]
    public class BestellingenController : Controller
    { 

        private WebshopContext _webshopContext;

        public BestellingenController(WebshopContext webshopContext)
        {
            _webshopContext = webshopContext;
        }
        public JsonResult Index()
        {
            return Json(_webshopContext.Bestellingen.ToList());
        }
    }
}