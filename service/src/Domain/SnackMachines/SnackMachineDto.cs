namespace Domain.SnackMachines;

public class SnackMachineDto
{
    public SnackMachineDto(long id, decimal moneyInside)
    {
        Id = id;
        MoneyInside = moneyInside;
    }

    public long Id { get; private set; }
    public decimal MoneyInside { get; private set; }
}