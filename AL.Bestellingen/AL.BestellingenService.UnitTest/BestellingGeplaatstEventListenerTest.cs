using AL.Bestellingen.Events;
using AL.Bestellingen.Events.EventListeners;
using AL.Bestellingen.Events.EventPublisher;
using AL.Bestellingen.Models;
using AL.WebshopService.DAL;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AL.BestellingenService.UnitTest
{
    [TestClass]
    public class BestellingGeplaatstEventListenerTest
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
        public void Handle_add_bestelling_to_database()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var bestellingGeplaatstEvent = new BestellingGeregistreedEvent()
            {
                Bestelling = new Bestelling()
                {
                    BestellingNummer = guid
                }
            };
            var publisherMock = new Mock<IBestellingGeregistreerdEventPublisher>();

            var target = new BestellingGeplaatstEventListener(new WebshopContext(_options), publisherMock.Object);


            // Act
            target.Handle(bestellingGeplaatstEvent);

            // Assert
            Assert.AreEqual(1, new WebshopContext(_options).Bestellingen.Count(b => b.BestellingNummer == guid));
        }

        [TestMethod]
        public void Handle_add_bestelling_to_database_state_is_Geplaatst()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var bestellingGeplaatstEvent = new BestellingGeregistreedEvent()
            {
                Bestelling = new Bestelling()
                {
                    BestellingNummer = guid
                }
            };
            var publisherMock = new Mock<IBestellingGeregistreerdEventPublisher>();

            var target = new BestellingGeplaatstEventListener(new WebshopContext(_options), publisherMock.Object);


            // Act
            target.Handle(bestellingGeplaatstEvent);

            // Assert
            Assert.AreEqual(1, new WebshopContext(_options).Bestellingen.Count(b => b.BestellingNummer == guid));
            Assert.AreEqual(BestellingStatus.Geplaatst, new WebshopContext(_options).Bestellingen.Single(b => b.BestellingNummer == guid).status);
        }

        [TestMethod]
        public void Handle_publishes_event()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var bestellingGeplaatstEvent = new BestellingGeregistreedEvent()
            {
                Bestelling = new Bestelling()
                {
                    BestellingNummer = guid
                }
            };
            var publisherMock = new Mock<IBestellingGeregistreerdEventPublisher>();
            publisherMock.Setup(m => m.PublishBestellingGeregistreedEvent(It.IsAny<BestellingGeregistreedEvent>()));

            var target = new BestellingGeplaatstEventListener(new WebshopContext(_options), publisherMock.Object);


            // Act
            target.Handle(bestellingGeplaatstEvent);

            // Assert
            publisherMock.Verify(m => m.PublishBestellingGeregistreedEvent(It.IsAny<BestellingGeregistreedEvent>()), Times.Once);
        }
    }
}
