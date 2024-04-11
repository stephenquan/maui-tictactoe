namespace TicTacToe;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel VM)
    {
        BindingContext = VM;
        InitializeComponent();
    }
}