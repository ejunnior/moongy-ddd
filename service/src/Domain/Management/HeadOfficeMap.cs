namespace Domain.Management;

using FluentNHibernate.Mapping;

public class HeadOfficeMap : ClassMap<HeadOffice>
{
    public HeadOfficeMap()
    {
        Id(x => x.Id);

        Map(x => x.Balance);

        Component(x => x.Cash, y =>
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