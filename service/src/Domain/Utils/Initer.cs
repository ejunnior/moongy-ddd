namespace Domain.Utils;

using Common;
using Management;

public static class Initer
{
    public static void Init(string connectionString)
    {
        SessionFactory.Init(connectionString);
        HeadOfficeInstance.Init();
        DomainEvents.Init();
    }
}