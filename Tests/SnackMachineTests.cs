namespace Tests;

using System.ComponentModel.DataAnnotations;
using Domain;
using Domain.SharedKernel;
using Domain.SnackMachines;
using FluentAssertions;
using Xunit;

public class SnackMachineTests
{
    [Fact]
    public void After_Purchase_Change_Is_Returned()
    {
        // Arrange
        var snackMachine = new SnackMachine();

        snackMachine
            .LoadSnacks(1, new SnackPile(Snack.Chocolate, 1, 0.5m));

        snackMachine
            .LoadMoney(Money.TenCent * 10);

        snackMachine
            .InsertMoney(Money.Euro);

        // Act
        snackMachine
            .BuySnack(1);

        // Assert
        snackMachine
            .MoneyInside
            .Amount
            .Should()
            .Be(1.5m);

        snackMachine
            .MoneyInTransaction
            .Should()
            .Be(0m);
    }

    [Fact]
    public void Buysnack_trades_inserted_money_for_a_snack()
    {
        // Arrange
        var snackMachine = new SnackMachine();

        snackMachine
            .LoadSnacks(position: 1, snackPile: new SnackPile(Snack.Chocolate, 10, 1m));

        snackMachine.InsertMoney(Money.Euro);

        // Act
        snackMachine
            .BuySnack(1);

        // Assert
        snackMachine
            .MoneyInTransaction
            .Should()
            .Be(0);

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
    public void Cannot_Buy_Snack_If_Not_Enough_Change()
    {
        // Arrange
        var snackMachine = new SnackMachine();

        snackMachine.LoadSnacks(1, new SnackPile(Snack.Chocolate, 1, 0.5m));

        snackMachine.InsertMoney(Money.Euro);

        // Act
        var action = () => snackMachine.BuySnack(1);

        // Assert
        action
            .Should()
            .Throw<InvalidOperationException>();
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
    public void Cannot_Make_Purchase_When_There_Is_No_Snack()
    {
        // Arrange
        var snackMachine = new SnackMachine();

        // Act
        var action = () => snackMachine
            .BuySnack(1);

        // Assert
        action
            .Should()
            .Throw<InvalidOperationException>();
    }

    [Fact]
    public void Cannot_Make_Purshase_If_Not_Enough_Money_Inserted()
    {
        // Arrange
        var snackMachine = new SnackMachine();

        snackMachine
            .LoadSnacks(position: 1, snackPile: new SnackPile(Snack.Chocolate, 1, 2m));

        snackMachine.InsertMoney(Money.Euro);

        // Action
        var action = () => snackMachine
            .BuySnack(1);

        // Assert
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
            .Should()
            .Be(0m);
    }

    [Fact]
    public void Snack_Machine_Returns_Money_With_Highest_Denomination_First()
    {
        // Arrange
        var snackMachine = new SnackMachine();

        snackMachine.LoadMoney(Money.Euro);
        snackMachine.InsertMoney(Money.Quarter);
        snackMachine.InsertMoney(Money.Quarter);
        snackMachine.InsertMoney(Money.Quarter);
        snackMachine.InsertMoney(Money.Quarter);

        // Act
        snackMachine.ReturnMoney();

        // Assert
        snackMachine
            .MoneyInside
            .QuarterCount
            .Should()
            .Be(4);

        snackMachine
            .MoneyInside
            .OneEuroCount
            .Should()
            .Be(0);
    }
}