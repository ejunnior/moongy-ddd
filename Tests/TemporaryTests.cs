namespace Tests;

using Domain;
using Xunit;

public class TemporaryTests
{
    [Fact]
    public void MappingTest()
    {
        SessionFactory.Init(@"Server=(local);Database=Ddd;Trusted_Connection=true");

        var repository = new SnackMachineRepository();
        var snackMachine = repository.GetById(1);
    }
}