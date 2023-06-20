namespace TicTacToe;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnNavigate(object sender, EventArgs e)
	{
		Button btn = (Button)sender;
		string arg = btn.CommandParameter.ToString();
		await Shell.Current.GoToAsync(arg);
	}
}

