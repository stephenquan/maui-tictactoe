using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace TicTacToe;

public partial class Board : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Cell> _cells = new ObservableCollection<Cell>(
        Enumerable.Range(0, 9).Select(i => new Cell() { I = i })
    );

    public List<int> AvailableMoves
        => Cells.Where(c => c.V == String.Empty).Select(c => c.I).ToList();

    public List<(int A, int B, int C)> WinX
        => Lines.Where(l => (Cells[l.A].V + Cells[l.B].V + Cells[l.C].V) == "XXX").ToList();

    public List<(int A, int B, int C)> WinO
        => Lines.Where(l => (Cells[l.A].V + Cells[l.B].V + Cells[l.C].V) == "OOO").ToList();

    public bool IsGameOver
        => WinX.Count > 0 || WinO.Count > 0 || AvailableMoves.Count == 0;

    public void EmitPropertyChanged()
    {
        OnPropertyChanged(nameof(AvailableMoves));
        OnPropertyChanged(nameof(WinX));
        OnPropertyChanged(nameof(WinO));
        OnPropertyChanged(nameof(IsGameOver));
        OnPropertyChanged(nameof(Score));
    }

    public Dictionary<string, int> Heur = new Dictionary<string, int>()
    {
        {"", 0 },
        {"O", 1 },
        {"X", -2 },
        {"OO", 10 },
        {"OX", 0 },
        {"XX", -20 },
        {"OOO", 1000 },
        {"OOX", 0 },
        {"OXX", 0 },
        {"XXX", -2000 }
    };

    public int Score
        => Lines.Sum(line => Heur[new string((Cells[line.A].V + Cells[line.B].V + Cells[line.C].V).OrderBy(c => c).ToArray())]);

    public void Clear()
    {
        foreach (var c in Cells) c.V = String.Empty;
        EmitPropertyChanged();
    }

    public Board PlayMove(int I, string V)
    {
        Cells[I].V = V;
        EmitPropertyChanged();
        return this;
    }

    public Board Clone()
    {
        Board newBoard = new Board();
        foreach (var c in newBoard.Cells) c.V = Cells[c.I].V;
        newBoard.EmitPropertyChanged();
        return newBoard;
    }

    public Board TryMove(int I, string V) => Clone().PlayMove(I, V);

    private List<(int A, int B, int C)> Lines = new List<(int A, int B, int C)>()
    { (0, 1, 2), (3, 4, 5), (6, 7, 8), (0, 3, 6), (1, 4, 7), (2, 5, 8), (0, 4, 8), (2, 4, 6) };
}