namespace Domain.Common;

public abstract class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

    public void ClearEvents()
    {
        _domainEvents.Clear();
    }

    protected void AddDomainEvent(IDomainEvent newEvent)
    {
        _domainEvents.Add(newEvent);
    }
}