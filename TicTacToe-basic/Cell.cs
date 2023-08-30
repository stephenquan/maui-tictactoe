using CommunityToolkit.Mvvm.ComponentModel;

namespace TicTacToe;

public partial class Cell : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(R))]
    [NotifyPropertyChangedFor(nameof(C))]
    private int _i = 0;

    public int R => I / 3;
    public int C => I % 3;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Img))]
    private string _v = string.Empty;

    public string Img => V switch
    {
        "X" => "cell_x.png",
        "O" => "cell_o.png",
        _ => ""
    };
}