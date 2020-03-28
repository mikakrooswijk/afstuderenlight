using AL.WebshopService.DAL;
using AL.WebshopService.Events;
using AL.WebshopService.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AL.WebshopService.UnitTest
{
    [TestClass]
    public class BestellingGeregistreerdEventListenerTest
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
        public void Handle_update_bestelling_status()
        {
            // Arrange 
            var context = new WebshopContext(_options);
            var guid = Guid.NewGuid();

            context.Bestellingen.Add(new Bestelling()
            {
                BestellingNummer = guid,
                status = BestellingStatus.InBehandling
            });

            context.SaveChanges();


            var target = new BestellingGeregistreerdEventListener(new WebshopContext(_options));

            // Act

            target.Handle(new BestellingGeregistreedEvent()
            {
                Bestelling = new Bestelling()
                {
                    BestellingNummer = guid,
                    status = BestellingStatus.Geplaatst
                }
            });

            // Assert
            Assert.AreEqual(BestellingStatus.Geplaatst, new WebshopContext(_options).Bestellingen.Single(b => b.BestellingNummer == guid).status);
        }
    }
}
