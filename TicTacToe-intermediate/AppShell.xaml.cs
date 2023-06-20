using TicTacToe.Pages;

namespace TicTacToe;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
		Routing.RegisterRoute(nameof(GamePage), typeof(GamePage));
	}
}
