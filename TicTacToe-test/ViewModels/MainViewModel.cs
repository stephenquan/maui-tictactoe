using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace TicTacToe_test.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Square> _squares;

        public MainViewModel()
        {
            Random rand = new Random();
            Squares = new ObservableCollection<Square>(Enumerable.Range(0, 9).Select(i => {
                var x = i % 3;
                var y = i / 3;
                return new Square()
                {
                    Id = i,
                    X = x,
                    Y = y,
                    Top = y != 0,
                    Bottom = y != 2,
                    Left = x != 0,
                    Right = x != 2,
                    Ch = "XO "[rand.Next(3)]
                };
                }
            ));
        }

        [RelayCommand]
        void RandomizeGame()
        {
            var rand = new Random();
            foreach (var square in Squares)
            {
                square.Ch = "XO "[rand.Next(3)];
            }
        }
    }
}
