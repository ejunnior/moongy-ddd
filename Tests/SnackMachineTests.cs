namespace Tests;

using System.ComponentModel.DataAnnotations;
using Domain;
using FluentAssertions;
using Xunit;

public class SnackMachineTests
{
    [Fact]
    public void Buysnack_trades_inserted_money_for_a_snack()
    {
        // Arrange
        var snackMachine = new SnackMachine();

        snackMachine.LoadSnacks(
            position: 1,
            new SnackPile(new Snack("Some Snack"), 10, 1m));

        snackMachine.InsertMoney(Money.Euro);

        // Act
        snackMachine
            .BuySnack(1);

        // Assert
        snackMachine
            .MoneyInTransaction
            .Should()
            .Be(Money.None);

        snackMachine
            .MoneyInside
            .Amount
            .Should()
            .Be(1m);

        snackMachine
            .GetSnackPile(1)
            .Quantity
            .Should()
            .Be(9);
    }

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