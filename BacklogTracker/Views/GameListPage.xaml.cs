using BacklogTracker.ViewModels;

namespace BacklogTracker.Views;

public partial class GameListPage : ContentPage
{

    public GameListPage(GameListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

}