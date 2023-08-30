using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TicTacToe;

public partial class MainViewModel : ObservableObject
{
    private Random random = new Random();

    [ObservableProperty]
    private Board _board = new Board();

    [ObservableProperty]
    private int _countWinX = 0;

    [ObservableProperty]
    private int _countWinO = 0;

    [ObservableProperty]
    private int _countTie = 0;

    private void IncrementScores()
    {
        if (Board.WinX.Count > 0)
            CountWinX++;
        else if (Board.WinO.Count > 0)
            CountWinO++;
        else if (Board.AvailableMoves.Count == 0)
            CountTie++;
    }

    [RelayCommand]
    private async void Click(int Index)
    {
        if (Board.IsGameOver) Board.Clear();
        if (Board.Cells[Index].V != String.Empty) return;
        Board.PlayMove(Index, "X");
        if (Board.IsGameOver)
        {
            IncrementScores();
            return;
        }

        await Task.Delay(250);
        var Moves = Board.AvailableMoves.OrderBy(M => -Board.TryMove(M, "O").Score).ToList();
        int MaxLevel = 5;
        int Level = 4;
        int Move = random.Next(MaxLevel) <= Level ? Moves[0] : Moves[random.Next(Moves.Count)];
        Board.PlayMove(Move, "O");
        if (Board.IsGameOver)
        {
            IncrementScores();
        }
    }
}
