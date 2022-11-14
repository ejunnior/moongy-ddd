namespace Domain.Common;

using Utils;

public class Repository<T>
    where T : AggregateRoot
{
    public T GetById(long id)
    {
        using (var session = SessionFactory.OpenSession())
        {
            return session.Get<T>(id);
        }
    }

    public void Save(T aggregateRoot)
    {
        using (var session = SessionFactory.OpenSession())
        using (var transaction = session.BeginTransaction())
        {
            session.SaveOrUpdate(aggregateRoot);
            transaction.Commit();
        }
    }
}