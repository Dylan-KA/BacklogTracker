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
        private GamePlatform newGamePlatform;
        public List<GamePlatform> GamePlatformList { get; } = Enum.GetValues(typeof(GamePlatform)).Cast<GamePlatform>().ToList();

        [ObservableProperty]
        private GameStatus newGameStatus;
        public List<GameStatus> GameStatusList { get; } = Enum.GetValues(typeof(GameStatus)).Cast<GameStatus>().ToList();

        [ObservableProperty]
        private float newGameHoursPlayed;

        [ObservableProperty]
        private ObservableCollection<GameEntity> gameList = new();

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
