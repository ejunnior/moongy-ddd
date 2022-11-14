namespace Domain.Atms;

using FluentNHibernate.Mapping;

public class AtmMap : ClassMap<Atm>
{
    public AtmMap()
    {
        Id(x => x.Id);

        Map(x => x.MoneyCharged);

        Component(x => x.MoneyInside, y =>
        {
            y.Map(x => x.OneCentCount);
            y.Map(x => x.TenCentCount);
            y.Map(x => x.QuarterCount);
            y.Map(x => x.OneEuroCount);
            y.Map(x => x.FiveEuroCount);
            y.Map(x => x.TwentyEuroCount);
        });
    }
}