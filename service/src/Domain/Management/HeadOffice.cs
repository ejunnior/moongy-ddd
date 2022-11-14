namespace Domain.Management;

using Common;
using SharedKernel;

public class HeadOffice : AggregateRoot
{
    public decimal Balance { get; private set; }

    public Money Cash { get; } = Money.None;

    public void ChangeBalance(decimal delta)
    {
        Balance += delta;
    }
}