namespace Domain.Atms;

public class AtmDto
{
    public AtmDto(long id, decimal cash)
    {
        Id = id;
        Cash = cash;
    }

    public decimal Cash { get; private set; }
    public long Id { get; private set; }
}