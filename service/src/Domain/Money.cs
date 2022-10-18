namespace Domain;

using Microsoft.VisualBasic.CompilerServices;

public class Money
{
    public Money(
        int oneCentCount,
        int tenCentCount,
        int quarterCentCount,
        int oneEuroCount,
        int fiveEuroCount,
        int twentyEuroCount)
    {
        OneCentCount += oneCentCount;
        TenCentCount += tenCentCount;
        QuarterCentCount += quarterCentCount;
        OneEuroCount += oneEuroCount;
        FiveEuroCount += fiveEuroCount;
        TwentyEuroCount += twentyEuroCount;
    }

    public int FiveEuroCount { get; private set; }
    public int OneCentCount { get; private set; }
    public int OneEuroCount { get; private set; }
    public int QuarterCentCount { get; private set; }
    public int TenCentCount { get; private set; }
    public int TwentyEuroCount { get; private set; }

    public static Money operator +(Money money1, Money money2)
    {
        return new Money(
            oneCentCount: money1.OneCentCount + money2.OneCentCount,
            tenCentCount: money1.TenCentCount + money2.TenCentCount,
            quarterCentCount: money1.QuarterCentCount + money2.QuarterCentCount,
            oneEuroCount: money1.OneEuroCount + money2.OneEuroCount,
            fiveEuroCount: money1.FiveEuroCount + money2.FiveEuroCount,
            twentyEuroCount: money1.TwentyEuroCount + money2.TwentyEuroCount);
    }
}