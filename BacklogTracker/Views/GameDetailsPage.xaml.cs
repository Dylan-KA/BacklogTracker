using BacklogTracker.ViewModels;

namespace BacklogTracker.Views;

public partial class GameDetailsPage : ContentPage
{
	public GameDetailsPage(GameDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}