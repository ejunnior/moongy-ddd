namespace Domain.Utils;

using Common;
using NHibernate.Event;

public class EventListener :
    IPostInsertEventListener,
    IPostDeleteEventListener,
    IPostUpdateEventListener,
    IPostCollectionUpdateEventListener
{
    public void OnPostDelete(
        PostDeleteEvent @event)
    {
        DispatchEvents(@event.Entity as AggregateRoot);
    }

    public Task OnPostDeleteAsync(
        PostDeleteEvent @event,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void OnPostInsert(
        PostInsertEvent @event)
    {
        DispatchEvents(@event.Entity as AggregateRoot);
    }

    public Task OnPostInsertAsync(
                    PostInsertEvent @event,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void OnPostUpdate(
        PostUpdateEvent @event)
    {
        DispatchEvents(@event.Entity as AggregateRoot);
    }

    public Task OnPostUpdateAsync(
            PostUpdateEvent @event,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void OnPostUpdateCollection(
        PostCollectionUpdateEvent @event)
    {
        DispatchEvents(@event.AffectedOwnerIdOrNull as AggregateRoot);
    }

    public Task OnPostUpdateCollectionAsync(
            PostCollectionUpdateEvent @event,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private void DispatchEvents(AggregateRoot aggregateRoot)
    {
        foreach (var @event in aggregateRoot.DomainEvents)
        {
            DomainEvents.Dispatch(@event);
        }

        aggregateRoot.ClearEvents();
    }
}