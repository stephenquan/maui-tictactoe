namespace TicTacToe;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(MainViewModel VM)
    {
        BindingContext = VM;
        InitializeComponent();
    }
}
