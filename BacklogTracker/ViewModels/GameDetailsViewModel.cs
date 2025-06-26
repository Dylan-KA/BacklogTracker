using BacklogTracker.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

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
            await _localDBService.UpdateGameAsync(Game);
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
