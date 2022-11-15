namespace Domain.SnackMachines;

using Common;
using NHibernate;
using Utils;

public class SnackMachineRepository : Repository<SnackMachine>
{
    public IReadOnlyList<SnackMachineDto> GetSnackMachineList()
    {
        using (ISession session = SessionFactory.OpenSession())
        {
            return session.Query<SnackMachine>()
                .ToList() // Fetch data into memory
                .Select(x => new SnackMachineDto(x.Id, x.MoneyInside.Amount))
                .ToList();
        }
    }
}