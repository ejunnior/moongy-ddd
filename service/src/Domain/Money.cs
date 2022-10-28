namespace Domain;

public class Money : ValueObject<Money>
{
    public Money(
        int oneCentCount,
        int tenCentCount,
        int quarterCount,
        int oneEuroCount,
        int fiveEuroCount,
        int twentyEuroCount)
    {
        OneCentCount += oneCentCount;
        TenCentCount += tenCentCount;
        QuarterCount += quarterCount;
        OneEuroCount += oneEuroCount;
        FiveEuroCount += fiveEuroCount;
        TwentyEuroCount += twentyEuroCount;
    }

    public int FiveEuroCount { get; private set; }
    public int OneCentCount { get; private set; }
    public int OneEuroCount { get; private set; }
    public int QuarterCount { get; private set; }
    public int TenCentCount { get; private set; }
    public int TwentyEuroCount { get; private set; }

    public static Money operator +(Money money1, Money money2)
    {
        return new Money(
            oneCentCount: money1.OneCentCount + money2.OneCentCount,
            tenCentCount: money1.TenCentCount + money2.TenCentCount,
            quarterCount: money1.QuarterCount + money2.QuarterCount,
            oneEuroCount: money1.OneEuroCount + money2.OneEuroCount,
            fiveEuroCount: money1.FiveEuroCount + money2.FiveEuroCount,
            twentyEuroCount: money1.TwentyEuroCount + money2.TwentyEuroCount);
    }

    protected override bool EqualsCore(Money other)
    {
        return
            OneCentCount == other.OneCentCount
            && TenCentCount == other.TenCentCount
            && QuarterCount == other.QuarterCount
            && OneEuroCount == other.OneEuroCount
            && FiveEuroCount == other.FiveEuroCount
            && TwentyEuroCount == other.TwentyEuroCount;
    }

    protected override int GetHashCodeCore()
    {
        unchecked
        {
            int hashCode = OneCentCount;
            hashCode = (hashCode * 397) ^ TenCentCount;
            hashCode = (hashCode * 397) ^ QuarterCount;
            hashCode = (hashCode * 397) ^ OneEuroCount;
            hashCode = (hashCode * 397) ^ FiveEuroCount;
            hashCode = (hashCode * 397) ^ TwentyEuroCount;
            return hashCode;
        }
    }
}