using BacklogTracker.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace BacklogTracker.ViewModels
{
    [QueryProperty("Game", "Game")]
    public partial class GameDetailsViewModel : ObservableObject
    {
        private readonly LocalDBService _localDBService;
        public GameDetailsViewModel(LocalDBService localDBService)
        {
            _localDBService = localDBService;
        }

        [ObservableProperty]
        private Game game;

        public List<GamePlatform> GamePlatformList { get; } = Enum.GetValues(typeof(GamePlatform)).Cast<GamePlatform>().ToList();
        public List<GameStatus> GameStatusList { get; } = Enum.GetValues(typeof(GameStatus)).Cast<GameStatus>().ToList();

        [RelayCommand]
        async Task SaveGame()
        {
            if (Game.Id > 0)
            {
                Debug.WriteLine("Updating game in database");
                await _localDBService.UpdateGameAsync(Game);
            } else
            {
                Debug.WriteLine("Adding new game to database");
                await _localDBService.AddGameAsync(Game);
            }
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task RemoveGame()
        {
            await _localDBService.DeleteGameAsync(Game);
            await Shell.Current.GoToAsync("..");
        }

    }
}
