using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Localization;
using System.Globalization;
using TicTacToe.Resources.Strings;

namespace TicTacToe;

public partial class MainViewModel : ObservableObject
{
    private Random random = new Random();

    static private MainViewModel _current;
    static public MainViewModel Current => _current ??= new MainViewModel();

    private IStringLocalizer _localizer;
    public IStringLocalizer Localizer => _localizer ??= ServiceHelper.GetService<IStringLocalizer<AppStrings>>();
    public string this[string name] => Localizer[name];
    public string this[string name, params object[] arguments] => Localizer[name, arguments];

    public FlowDirection FlowDirection => Culture.TextInfo.IsRightToLeft ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

    [ObservableProperty]
    private Board _board = new Board();

    [ObservableProperty]
    private int _countWinX = 0;

    [ObservableProperty]
    private int _countWinO = 0;

    [ObservableProperty]
    private int _countTie = 0;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(LevelText))]
    private int _level = 4;

    public string LevelText => this["LABEL_LEVEL", Level];

    private void IncrementScores()
    {
        if (Board.WinX.Count > 0)
            CountWinX++;
        else if (Board.WinO.Count > 0)
            CountWinO++;
        else if (Board.AvailableMoves.Count == 0)
            CountTie++;
    }

    private void ResetOpacity()
    {
        foreach (var c in Board.Cells) c.O = 1.0;
    }

    private void SetOpacity()
    {
        foreach (var c in Board.Cells) c.O = 0.2;
        foreach (var w in Board.WinX)
        {
            Board.Cells[w.A].O = 1.0;
            Board.Cells[w.B].O = 1.0;
            Board.Cells[w.C].O = 1.0;
        }
        foreach (var w in Board.WinO)
        {
            Board.Cells[w.A].O = 1.0;
            Board.Cells[w.B].O = 1.0;
            Board.Cells[w.C].O = 1.0;
        }
    }

    public CultureInfo Culture
    {
        get => CultureInfo.CurrentUICulture;
        set
        {
            if (value == null) return;
            if (value.Name == CultureInfo.CurrentUICulture.Name) return;
            CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = value;
            OnPropertyChanged(nameof(Culture));
            OnPropertyChanged("Item");
            OnPropertyChanged(nameof(FlowDirection));
            OnPropertyChanged(nameof(LevelText));
        }
    }

    [ObservableProperty]
    private List<CultureInfo> _languages = new List<CultureInfo>()
    {
        new CultureInfo("en-US"),
        new CultureInfo("fr-FR"),
        new CultureInfo("de-DE"),
        new CultureInfo("zh-CN"),
    };

    [RelayCommand]
    private async Task Click(int Index)
    {
        if (Board.IsGameOver) Board.Clear();
        if (Board.Cells[Index].V != String.Empty) return;
        Board.PlayMove(Index, "X");
        if (Board.IsGameOver)
        {
            IncrementScores();
            SetOpacity();
            return;
        }

        await Task.Delay(250);
        var BestMoves = Board.AvailableMoves
            .OrderBy(M => random.Next())
            .OrderBy(M => -Board.TryMove(M, "O").Score).ToList();
        int Move = random.Next(5) < Level ? BestMoves[0] : BestMoves[random.Next(BestMoves.Count)];
        Board.PlayMove(Move, "O");
        if (Board.IsGameOver)
        {
            IncrementScores();
            SetOpacity();
            return;
        }
    }

}
