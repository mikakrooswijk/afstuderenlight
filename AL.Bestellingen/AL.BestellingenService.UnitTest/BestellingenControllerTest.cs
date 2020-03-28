using AL.Bestellingen.Controllers;
using AL.Bestellingen.Models;
using AL.WebshopService.DAL;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AL.BestellingenService.UnitTest
{
    [TestClass]
    public class BestellingenControllerTest
    {

        private static DbContextOptions<WebshopContext> _options;

        [ClassInitialize]
        public static void TestInit(TestContext testContext)
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            _options = new DbContextOptionsBuilder<WebshopContext>()
                    .UseSqlite(connection)
                    .Options;

            using (var context = new WebshopContext(_options))
            {
                context.Database.EnsureCreated();
            }
        }


        [TestMethod]
        public void Index_returns_all_bestellingen()
        {
            // Arrange
            var context = new WebshopContext(_options);
            var target = new BestellingenController(new WebshopContext(_options));
            var guid = Guid.NewGuid();
            context.Bestellingen.Add(new Bestelling() { BestellingNummer = guid });
            context.SaveChanges();

            // Act
            var result = target.Index();

            // Assert
            Assert.IsInstanceOfType(result.Value, typeof(List<Bestelling>));
            Assert.AreEqual(1, (result.Value as List<Bestelling>).Count(b => b.BestellingNummer == guid));

        }
    }
}
