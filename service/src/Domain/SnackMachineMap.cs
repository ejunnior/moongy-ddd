namespace Domain;

using FluentNHibernate;
using FluentNHibernate.Mapping;

public class SnackMachineMap : ClassMap<SnackMachine>
{
    public SnackMachineMap()
    {
        Id(x => x.Id);

        Component(x => x.MoneyInside, y =>
        {
            y.Map(x => x.OneCentCount);
            y.Map(x => x.TenCentCount);
            y.Map(x => x.QuarterCount);
            y.Map(x => x.OneEuroCount);
            y.Map(x => x.FiveEuroCount);
            y.Map(x => x.TwentyEuroCount);
        });

        HasMany<Slot>(Reveal.Member<SnackMachine>("Slots"))
            .Cascade
            .SaveUpdate()
            .Not
            .LazyLoad();
    }
}