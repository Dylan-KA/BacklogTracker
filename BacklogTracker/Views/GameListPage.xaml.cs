using BacklogTracker.ViewModels;

namespace BacklogTracker.Views;

public partial class GameListPage : ContentPage
{
    private GameListViewModel ViewModel => (GameListViewModel)BindingContext;

    public GameListPage(GameListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        SizeChanged += OnSizeChanged;
    }
    
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is GameListViewModel viewModel)
        {
            await viewModel.LoadGamesFromDatabase();
        }
    }
    private void OnSizeChanged(object sender, EventArgs e)
    {
        var width = this.Width;

        if (width < 500)
            ViewModel.ColumnSpan = 1;
        else if (width < 800)
            ViewModel.ColumnSpan = 2;
        else if (width < 1100)
            ViewModel.ColumnSpan = 3;
        else if (width < 1400)
            ViewModel.ColumnSpan = 4;
        else if (width < 1700)
            ViewModel.ColumnSpan = 5;
        else if (width < 2000)
            ViewModel.ColumnSpan = 6;
        else
            ViewModel.ColumnSpan = 7;
    }

}