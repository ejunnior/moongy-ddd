﻿namespace Domain;

public class SnackPile : ValueObject<SnackPile>
{
    public SnackPile(
        Snack snack,
        int quantity,
        decimal price)
    {
        Snack = snack;
        Quantity = quantity;
        Price = price;
    }

    private SnackPile()
    {
    }

    public decimal Price { get; }

    public int Quantity { get; }

    public Snack Snack { get; }

    public SnackPile SubtractOne()
    {
        return new SnackPile(Snack, Quantity - 1, Price);
    }

    protected override bool EqualsCore(SnackPile other)
    {
        return Snack == other.Snack &&
               Quantity == other.Quantity &&
               Price == other.Price;
    }

    protected override int GetHashCodeCore()
    {
        unchecked
        {
            int hashCode = Snack.GetHashCode();
            hashCode = (hashCode * 397) ^ Quantity;
            hashCode = (hashCode * 397) ^ Price.GetHashCode();
            return hashCode;
        }
    }
}