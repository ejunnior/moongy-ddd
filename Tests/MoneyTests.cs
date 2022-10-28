namespace Tests
{
    using Domain;
    using FluentAssertions;
    using Xunit;

    public class MoneyTests
    {
        [Fact]
        public void Sum_of_two_moneys_produces_correct_result()
        {
            // Arrange
            var money1 = new Money(
                oneCentCount: 1,
                tenCentCount: 2,
                quarterCount: 3,
                oneEuroCount: 4,
                fiveEuroCount: 5,
                twentyEuroCount: 6);

            var money2 = new Money(
                oneCentCount: 1,
                tenCentCount: 2,
                quarterCount: 3,
                oneEuroCount: 4,
                fiveEuroCount: 5,
                twentyEuroCount: 6);

            // Act
            var sum = money1 + money2;

            // Assert
            sum
                .OneCentCount
                .Should()
                .Be(2);

            sum.
                TenCentCount
                .Should()
                .Be(4);

            sum.
                QuarterCount
                .Should()
                .Be(6);

            sum.
                OneEuroCount
                .Should()
                .Be(8);

            sum.
                FiveEuroCount
                .Should()
                .Be(10);

            sum.
                TwentyEuroCount
                .Should()
                .Be(12);
        }

        [Fact]
        public void Two_money_instances_equal_if_contain_the_same_money_amount()
        {
            // Arrange
            var money1 = new Money(
                oneCentCount: 1,
                tenCentCount: 2,
                quarterCount: 3,
                oneEuroCount: 4,
                fiveEuroCount: 5,
                twentyEuroCount: 6);

            var money2 = new Money(
                oneCentCount: 1,
                tenCentCount: 2,
                quarterCount: 3,
                oneEuroCount: 4,
                fiveEuroCount: 5,
                twentyEuroCount: 6);

            // Act

            // Assert
            money1
                .Should()
                .Be(money2);

            money1
                .Should()
                .Be(money2.GetHashCode());
        }
    }
}