namespace Domain.Atms;

using Common;
using NHibernate;
using Utils;

public class AtmRepository : Repository<Atm>
{
    public IReadOnlyList<AtmDto> GetAtmList()
    {
        using (ISession session = SessionFactory.OpenSession())
        {
            return session.Query<Atm>()
                .ToList() // Fetch data into memory
                .Select(x => new AtmDto(x.Id, x.MoneyInside.Amount))
                .ToList();
        }
    }
}