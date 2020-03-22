using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AL.WebshopService.DAL;
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
        public IActionResult Index()
        {
            return Json(_webshopContext.Bestellingen);
        }
    }
}