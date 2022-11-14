namespace Domain.Atms;

using Common;

public class BalanceChangedEvent : IDomainEvent
{
    public BalanceChangedEvent(decimal delta)
    {
        Delta = delta;
    }

    public decimal Delta { get; private set; }
}