namespace Tests
{
    using Domain;
    using FluentAssertions;
    using Xunit;

    public class MoneyTests
    {
        [Theory]
        [InlineData(-1, 0, 0, 0, 0, 0)]
        [InlineData(0, -2, 0, 0, 0, 0)]
        [InlineData(0, 0, -3, 0, 0, 0)]
        [InlineData(0, 0, 0, -4, 0, 0)]
        [InlineData(0, 0, 0, 0, -5, 0)]
        [InlineData(0, 0, 0, 0, 0, -6)]
        public void Cannot_create_money_with_negative_values(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneEuroCount,
            int fiveEuroCount,
            int twentyEuroCount)
        {
            var action = () => new Money(
                oneCentCount: oneCentCount,
                tenCentCount: tenCentCount,
                quarterCount: quarterCount,
                oneEuroCount: oneEuroCount,
                fiveEuroCount: fiveEuroCount,
                twentyEuroCount: twentyEuroCount);

            action
                .Should()
                .Throw<InvalidOperationException>();
        }

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
        public void Two_money_instances_do_not_equal_if_contain_different_money_amount()
        {
            // Arrange
            var euro = new Money(
                oneCentCount: 0,
                tenCentCount: 0,
                quarterCount: 0,
                oneEuroCount: 1,
                fiveEuroCount: 0,
                twentyEuroCount: 0);

            var hundredCents = new Money(
                oneCentCount: 100,
                tenCentCount: 0,
                quarterCount: 0,
                oneEuroCount: 0,
                fiveEuroCount: 0,
                twentyEuroCount: 0);

            // Act

            // Assert
            euro
                .Should()
                .NotBe(hundredCents);

            euro
                .GetHashCode()
                .Should()
                .NotBe(hundredCents.GetHashCode());
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
                .GetHashCode()
                .Should()
                .Be(money2.GetHashCode());
        }
    }
}