using AL.WebshopService.Controllers;
using AL.WebshopService.DAL;
using AL.WebshopService.Events;
using AL.WebshopService.Events.EventPublishers;
using AL.WebshopService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AL.WebshopService.UnitTest
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
            var target = new BestellingenController(new WebshopContext(_options), null);
            var guid = Guid.NewGuid();
            context.Bestellingen.Add(new Bestelling() { BestellingNummer = guid });
            context.SaveChanges();

            // Act
            var result = target.Index();

            // Assert
            Assert.IsInstanceOfType(result.Value, typeof(List<Bestelling>));
            Assert.AreEqual(1, (result.Value as List<Bestelling>).Count(b => b.BestellingNummer == guid));
        }

        [TestMethod]
        public void Create_adds_bestelling_to_database()
        {
            // Arrange
            var context = new WebshopContext(_options);
            context.RemoveRange(context.Bestellingen);

            var publisherMock = new Mock<IBestellingGeplaatstEventPublisher>();
            publisherMock.Setup(m => m.PublishBestellingGeplaatstEvent(It.IsAny<BestellingGeplaatstEvent>()));

            var target = new BestellingenController(new WebshopContext(_options), publisherMock.Object);
            var guid = Guid.NewGuid();

            // Act
            var result = target.Create(new Bestelling()
            {
                BestellingNummer = guid
            });

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
            Assert.AreEqual(1, new WebshopContext(_options).Bestellingen.Count());
        }

        [TestMethod]
        public void Create_publishes_event()
        {
            // Arrange
            var context = new WebshopContext(_options);
            context.RemoveRange(context.Bestellingen);

            var publisherMock = new Mock<IBestellingGeplaatstEventPublisher>();
            publisherMock.Setup(m => m.PublishBestellingGeplaatstEvent(It.IsAny<BestellingGeplaatstEvent>()));

            var target = new BestellingenController(new WebshopContext(_options), publisherMock.Object);
            var guid = Guid.NewGuid();

            // Act
            var result = target.Create(new Bestelling()
            {
                BestellingNummer = guid
            });

            // Assert
            publisherMock.Verify();
        }
    }
}
