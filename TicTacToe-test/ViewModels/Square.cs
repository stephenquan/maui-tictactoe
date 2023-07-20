using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TicTacToe_test.ViewModels
{
    public partial class Square : ObservableObject
    {
        [ObservableProperty]
        private int _id;

        [ObservableProperty]
        private int _x;

        [ObservableProperty]
        private int _y;

        [ObservableProperty]
        private char _ch;

        [RelayCommand]
        void RandomizeSquare()
        {
            var rand = new Random();
            var OldCh = Ch;
            Ch = "XO "[rand.Next(3)];
            while (Ch == OldCh)
            {
                Ch = "XO "[rand.Next(3)];
            }
        }
    }
}
