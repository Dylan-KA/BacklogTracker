using BacklogTracker.ViewModels;
using System.Diagnostics;

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

        //Debug.WriteLine($"Width {width}");

        if (width < 745)
            ViewModel.ColumnSpan = 1;
        else if (width < 1075)
            ViewModel.ColumnSpan = 2;
        else if (width < 1405)
            ViewModel.ColumnSpan = 3;
        else if (width < 1735)
            ViewModel.ColumnSpan = 4;
        else if (width < 2065)
            ViewModel.ColumnSpan = 5;
        else if (width < 2395)
            ViewModel.ColumnSpan = 6;
        else
            ViewModel.ColumnSpan = 7;
    }

}