namespace TicTacToe_test
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            GameFrame.SizeChanged += (s, e) =>
            {
                var size = Math.Min(GameFrame.Width, GameFrame.Height);
                GameLayout.WidthRequest = size;
                GameLayout.HeightRequest = size;
            };
        }
    }
}