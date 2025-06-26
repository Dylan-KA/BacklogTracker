using BacklogTracker.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BacklogTracker.ViewModels
{
    public partial class GameListViewModel : ObservableObject
    {
        private readonly LocalDBService _localDBService;

        public GameListViewModel(LocalDBService dBService)
        {
            _localDBService = dBService;
            GameList = new ObservableCollection<Game>();
            _ = LoadGamesFromDatabase();
        }

        private async Task LoadGamesFromDatabase()
        {
            await _localDBService.InitializeAsync();
            var games = await _localDBService.GetGameAsync();
            GameList.Clear();
            foreach (var game in games)
                GameList.Add(game);
        }

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
        private ObservableCollection<Game> gameList;

        [RelayCommand]
        private Task ShowAddGameView()
        {
            IsAddViewVisible = true;
            return Task.CompletedTask;
        }

        [RelayCommand]
        private async Task AddGameToList()
        {
            var newGame = new Game
            {
                Title = NewGameTitle,
                Platform = NewGamePlatform,
                Status = NewGameStatus,
                HoursPlayed = NewGameHoursPlayed
            };

            await _localDBService.AddGameAsync(newGame);
            GameList.Add(newGame);
            HideAddGameView();

            // Clear form fields
            NewGameTitle = string.Empty;
            NewGamePlatform = GamePlatform.Other;
            NewGameStatus = GameStatus.Backlog;
            NewGameHoursPlayed = 0;
        }

        [RelayCommand]
        private async Task RemoveGameFromList(Game game)
        {
            await _localDBService.DeleteGameAsync(game);
            GameList.Remove(game);
        }

        private void HideAddGameView()
        {
            IsAddViewVisible = false;
        }

    }
}
