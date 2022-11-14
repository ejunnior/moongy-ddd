namespace Domain.SnackMachines;

using FluentNHibernate.Mapping;

public class SnackMap : ClassMap<Snack>
{
    public SnackMap()
    {
        Id(x => x.Id);
        Map(x => x.Name);
    }
}