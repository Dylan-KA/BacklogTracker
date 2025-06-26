using BacklogTracker.ViewModels;

namespace BacklogTracker.Views;

public partial class GameListPage : ContentPage
{

    public GameListPage(GameListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is GameListViewModel viewModel)
        {
            await viewModel.LoadGamesFromDatabase();
        }
    }


}