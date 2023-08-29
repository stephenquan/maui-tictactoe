using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace TicTacToe;

public partial class MainViewModel : ObservableObject
{
    private Random random = new Random();

    [ObservableProperty]
    private ObservableCollection<Cell> _b;

    [ObservableProperty]
    private List<int> _freeSquares;

    [ObservableProperty]
    private List<(int, int, int)> _winX;

    [ObservableProperty]
    private List<(int, int, int)> _winO;

    [ObservableProperty]
    private int _countWinX = 0;

    [ObservableProperty]
    private int _countWinO = 0;

    [ObservableProperty]
    private int _countTie = 0;
        
    public EventHandler GameOverEvent;

    private bool _gameOver = false;
    public bool GameOver
    {
        get => _gameOver;
        set
        {
            if (SetProperty(ref _gameOver, value))
            {
                if (_gameOver)
                {
                    GameOverEvent?.Invoke(this, EventArgs.Empty);
                    if (WinX.Count > 0)
                        CountWinX++;
                    else if (WinO.Count > 0)
                        CountWinO++;
                    else if (FreeSquares.Count == 0)
                        CountTie++;
                }
            }
        }
    }

    [RelayCommand]
    private void NewGame()
    {
        foreach (var c in B) c.V = String.Empty;
        CheckGame();
    }

    private void CheckGame()
    {
        WinX = Lines.Where(l => (B[l.Item1].V + B[l.Item2].V + B[l.Item3].V) == "XXX").ToList();
        WinO = Lines.Where(l => (B[l.Item1].V + B[l.Item2].V + B[l.Item3].V) == "OOO").ToList();
        FreeSquares = B.Where(c => c.V == String.Empty).Select(c => c.I).ToList();
        GameOver = WinX.Count > 0 || WinO.Count > 0 || FreeSquares.Count == 0;
    }

    [RelayCommand]
    private async void Click(int Index)
    {
        if (GameOver) NewGame();
        B[Index].V = "X";
        CheckGame();
        if (GameOver) return;

        await Task.Delay(250);
        B[FreeSquares[random.Next(FreeSquares.Count)]].V = "O";
        CheckGame();
    }

    private List<(int, int, int)> Lines = new List<(int, int, int)>()
    { (0, 1, 2), (3, 4, 5), (6, 7, 8), (0, 3, 6), (1, 4, 7), (2, 5, 8), (0, 4, 8), (2, 4, 6) };

    public bool IsDraw => B.Where(c => c.V == String.Empty).Count() == 0;

    public MainViewModel()
    {
        B = new ObservableCollection<Cell>();
        for (int I = 0; I < 9; I++) B.Add(new Cell() { I = I });
    }
}
