using Minor.Miffy.MicroServices.Events;

namespace AL.WebshopService.Events.EventPublishers
{
    public class BestellingGeplaatstEventPublisher : IBestellingGeplaatstEventPublisher
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
