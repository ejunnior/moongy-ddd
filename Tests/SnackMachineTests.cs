namespace Tests;

using Domain;
using FluentAssertions;
using Xunit;

public class SnackMachineTests
{
    [Fact]
    public void Cannot_insert_more_than_one_coin_or_note_at_a_time()
    {
        var machine = new SnackMachine();
        var twoCent = Money.Cent + Money.Cent;

        var action = () => machine
            .InsertMoney(twoCent);

        action
            .Should()
            .Throw<InvalidOperationException>();
    }

    [Fact]
    public void Inserted_money_goes_to_money_in_transaction()
    {
        // Arrange
        var snackMachine = new SnackMachine();

        // Act
        snackMachine.InsertMoney(Money.Cent);
        snackMachine.InsertMoney(Money.Euro);

        // Assert
        snackMachine
            .MoneyInTransaction
            .Amount
            .Should()
            .Be(1.01m);
    }

    [Fact]
    public void Money_in_transaction_goes_to_inside_after_purchase()
    {
        // Arrange
        var snackMachine = new SnackMachine();

        snackMachine.InsertMoney(Money.Euro);
        snackMachine.InsertMoney(Money.Euro);

        // Act
        snackMachine
            .BuySnack();

        // Assert
        snackMachine
            .MoneyInTransaction
            .Should()
            .Be(Money.None);

        snackMachine
            .MoneyInside
            .Amount
            .Should()
            .Be(2m);
    }

    [Fact]
    public void Return_Money_Empties_Money_In_Transaction()
    {
        // Arrange
        var snackMachine = new SnackMachine();

        snackMachine
            .InsertMoney(Money.Cent);

        // Act
        snackMachine
            .ReturnMoney();

        // Assert
        snackMachine
            .MoneyInTransaction
            .Amount
            .Should()
            .Be(0m);
    }
}