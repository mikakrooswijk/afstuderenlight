namespace AL.Bestellingen.Events.EventPublisher
{
    public interface IBestellingGeregistreerdEventPublisher
    {
        void PublishBestellingGeregistreedEvent(BestellingGeregistreedEvent bestellingGeregistreerdEvent);
    }
}