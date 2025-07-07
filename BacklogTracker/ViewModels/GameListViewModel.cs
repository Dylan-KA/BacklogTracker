using BacklogTracker.Models;
using BacklogTracker.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BacklogTracker.ViewModels
{
    public partial class GameListViewModel : ObservableObject
    {
        private readonly LocalDBService _localDBService;

        public GameListViewModel(LocalDBService dBService)
        {
            // Get Game List from database
            _localDBService = dBService;
            GameList = new ObservableCollection<Game>();
            SearchFilteredGameList = new ObservableCollection<Game>();
            _ = LoadGamesFromDatabase();

            // Set Filter buttons to visible
            IsResetButtonVisible = false;
            IsBacklogButtonVisible = true;
            IsPlayingButtonVisible = true;
            IsCompletedButtonVisible = true;
        }

        public async Task LoadGamesFromDatabase()
        {
            await _localDBService.InitializeAsync();
            var games = await _localDBService.GetGameListAsync();

            var newList = new ObservableCollection<Game>(games);

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                GameList = newList;
                SearchFilteredGameList = new ObservableCollection<Game>(GameList);
            });
        }

        [ObservableProperty]
        private int columnSpan = 5;

        [ObservableProperty]
        private ObservableCollection<Game> gameList;

        [ObservableProperty]
        private ObservableCollection<Game> searchFilteredGameList;

        [ObservableProperty]
        private string searchText;

        partial void OnSearchTextChanged(string value)
        {
            PerformSearch(value);
        }

        [RelayCommand]
        public async Task PerformSearch(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                SearchFilteredGameList = new ObservableCollection<Game>(GameList);
                //Debug.WriteLine("Showing all games");
            }
            else
            {
                var filtered = GameList
                    .Where(game => !string.IsNullOrEmpty(game.Title) &&
                                   game.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
             
                SearchFilteredGameList.Clear();
                foreach (var game in filtered)
                {
                    SearchFilteredGameList.Add(game);
                }
                
                //Debug.WriteLine($"Filtering games by: {searchText}");
            }
        }

        [ObservableProperty]
        private bool isResetButtonVisible;
        
        [ObservableProperty]
        private bool isBacklogButtonVisible;

        [ObservableProperty]
        private bool isPlayingButtonVisible;

        [ObservableProperty]
        private bool isCompletedButtonVisible;

        [ObservableProperty]
        private Style backlogButtonStyle;

        [ObservableProperty]
        private Style playingButtonStyle;

        [ObservableProperty]
        private Style completedButtonStyle;


        [RelayCommand]
        public void ToggleButtonVisibility(string buttonNumberString)
        {
            if (int.TryParse(buttonNumberString, out int buttonNumber))
            {
                if (buttonNumber == 0)
                {
                    IsResetButtonVisible = false;
                    IsBacklogButtonVisible = true;
                    IsPlayingButtonVisible = true;
                    IsCompletedButtonVisible = true;
                    return;
                }

                IsResetButtonVisible = true;
                IsBacklogButtonVisible = buttonNumber == 1;
                IsPlayingButtonVisible = buttonNumber == 2;
                IsCompletedButtonVisible = buttonNumber == 3;
            }
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
