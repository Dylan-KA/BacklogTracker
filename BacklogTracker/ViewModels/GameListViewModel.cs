using BacklogTracker.Models;
using BacklogTracker.Views;
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

        public async Task LoadGamesFromDatabase()
        {
            await _localDBService.InitializeAsync();
            var games = await _localDBService.GetGameListAsync();

            var newList = new ObservableCollection<Game>(games);

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                GameList = newList;
            });
        }

        [ObservableProperty]
        private int columnSpan = 5;

        [ObservableProperty]
        private ObservableCollection<Game> gameList;

        [RelayCommand]
        private async Task RemoveGameFromList(Game game)
        {
            await _localDBService.DeleteGameAsync(game);
            GameList.Remove(game);
        }

        [RelayCommand]
        private async Task GoToEditView(Game game)
        {
            if (game == null) return;

            await Shell.Current.GoToAsync(
                $"{nameof(GameDetailsPage)}",
                true,
                new Dictionary<string, object>
                {
                    { "Game", game }
                });
        }

        [RelayCommand]
        private async Task GoToAddView()
        {
            Game game = new Game();
            
            await Shell.Current.GoToAsync(
                $"{nameof(GameDetailsPage)}",
                true,
                new Dictionary<string, object>
                {
                    { "Game", game }
                });
        }

    }
}
