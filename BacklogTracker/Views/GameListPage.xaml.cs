using BacklogTracker.ViewModels;
using System.Diagnostics;

namespace BacklogTracker.Views;

public partial class GameListPage : ContentPage
{

    public GameListPage()
    {
        InitializeComponent();
        BindingContext = new GameListViewModel();
    }

}