namespace UI.SnackMachines;

using System;
using System.Windows;
using System.Windows.Media;
using Domain.SnackMachines;

public class SnackPileViewModel
{
    private readonly SnackPile _snackPile;

    public SnackPileViewModel(SnackPile snackPile)
    {
        _snackPile = snackPile;
    }

    public int Amount => _snackPile.Quantity;

    public ImageSource Image =>
        (ImageSource)Application.Current.FindResource("img" + _snackPile.Snack.Name);

    public int ImageWidth => GetImageWidth(_snackPile.Snack);
    public string Price => _snackPile.Price.ToString("C2");

    private int GetImageWidth(Snack snack)
    {
        if (snack == Snack.Chocolate)
            return 120;

        if (snack == Snack.Soda)
            return 70;

        if (snack == Snack.Gum)
            return 70;

        throw new ArgumentException();
    }
}