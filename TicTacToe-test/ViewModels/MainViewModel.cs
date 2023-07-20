using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace TicTacToe_test.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Square> _squares = new ObservableCollection<Square>(
            Enumerable.Range(0, 9).Select(i => new Square()
            {
                Id = i,
                X = i % 3,
                Y = i / 3,
                Ch = ' '
            })
         );

        public MainViewModel()
        {
            RandomizeGame();
        }

        [RelayCommand]
        void ResetGame()
        {
            foreach (var square in Squares) square.Ch = ' ';
        }

        [RelayCommand]
        void RandomizeGame()
        {
            var rand = new Random();
            foreach (var square in Squares) square.Ch = "XO "[rand.Next(3)];
        }
    }
}
