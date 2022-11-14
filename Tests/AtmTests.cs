namespace Tests;

using Domain.Atms;
using Domain.SharedKernel;
using FluentAssertions;
using Xunit;

public class AtmTests
{
    [Fact]
    public void Comission_is_at_least_one_cent()
    {
        // Arrange
        var atm = new Atm();

        atm.LoadMoney(Money.Cent);

        // Act
        atm.TakeMoney(0.01m);

        // Assert
        atm
            .MoneyCharged
            .Should()
            .Be(0.02m);
    }

    [Fact]
    public void Comission_is_rounded_up_to_the_next_cent()
    {
        // Arrange
        var atm = new Atm();

        atm.LoadMoney(Money.Euro + Money.TenCent);

        // Act
        atm.TakeMoney(1.1m);

        // Assert
        atm
            .MoneyCharged
            .Should()
            .Be(1.12m);
    }

    [Fact]
    public void Take_money_exchanges_money_with_comission()
    {
        // Arrange
        var atm = new Atm();

        atm.LoadMoney(Money.Euro);

        // Act
        atm.TakeMoney(1m);

        // Assert
        atm
            .MoneyInside
            .Amount
            .Should()
            .Be(0m);

        atm
            .MoneyCharged
            .Should()
            .Be(1.01m);
    }
}