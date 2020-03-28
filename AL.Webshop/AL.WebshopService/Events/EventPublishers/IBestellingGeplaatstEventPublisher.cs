namespace AL.WebshopService.Events.EventPublishers
{
    public interface IBestellingGeplaatstEventPublisher
    {
        void PublishBestellingGeplaatstEvent(BestellingGeplaatstEvent bestellingGeplaatstEvent);
    }
}