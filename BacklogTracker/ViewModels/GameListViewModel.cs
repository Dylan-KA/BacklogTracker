using BacklogTracker.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BacklogTracker.ViewModels
{
    public partial class GameListViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isAddViewVisible;

        [ObservableProperty]
        private string newGameTitle;

        [ObservableProperty]
        private string newGamePlatform;

        [ObservableProperty]
        private string newGameStatus;

        [ObservableProperty]
        private float newGameHoursPlayed;

        [ObservableProperty]
        private ObservableCollection<Game> gameList = new();

        public GameListViewModel()
        {
            GameList = new ObservableCollection<Game>
            {
                new Game("Test Title", "Test Platform", "Test Status", 0)
            };
        }

        [RelayCommand]
        private Task ShowAddGameView()
        {
            IsAddViewVisible = true;
            return Task.CompletedTask;
        }

        [RelayCommand]
        private Task AddGameToList()
        {
            Game newGame = new Game(NewGameTitle, NewGamePlatform, NewGameStatus, NewGameHoursPlayed);
            GameList.Add(newGame);
            HideAddGameView();
            return Task.CompletedTask;
        }

        private void HideAddGameView()
        {
            IsAddViewVisible = false;
        }

    }
}
