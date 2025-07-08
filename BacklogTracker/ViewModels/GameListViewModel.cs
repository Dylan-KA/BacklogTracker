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

        [ObservableProperty]
        private GameStatus? selectedStatusFilter = null;


        partial void OnSearchTextChanged(string value)
        {
            ApplyFilters();
        }

        [RelayCommand]
        public void ApplyFilters()
        {
            var filteredList = GameList.AsEnumerable();

            // Apply search text
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filteredList = filteredList
                    .Where(game => !string.IsNullOrEmpty(game.Title) &&
                                   game.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
            }

            // Apply status filter
            if (SelectedStatusFilter.HasValue)
            {
                filteredList = filteredList
                    .Where(game => game.Status == SelectedStatusFilter.Value);
            }

            SearchFilteredGameList = new ObservableCollection<Game>(filteredList);
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
        public void FilterButtonAction(string buttonNumberString)
        {
            if (int.TryParse(buttonNumberString, out int buttonNumber))
            {
                switch (buttonNumber)
                {
                    case 0:
                        SelectedStatusFilter = null;
                        IsResetButtonVisible = false;
                        IsBacklogButtonVisible = true;
                        IsPlayingButtonVisible = true;
                        IsCompletedButtonVisible = true;
                        break;
                    case 1:
                        SelectedStatusFilter = GameStatus.Backlog;
                        IsResetButtonVisible = true;
                        IsBacklogButtonVisible = true;
                        IsPlayingButtonVisible = false;
                        IsCompletedButtonVisible = false;
                        break;
                    case 2:
                        SelectedStatusFilter = GameStatus.Playing;
                        IsResetButtonVisible = true;
                        IsBacklogButtonVisible = false;
                        IsPlayingButtonVisible = true;
                        IsCompletedButtonVisible = false;
                        break;
                    case 3:
                        SelectedStatusFilter = GameStatus.Completed;
                        IsResetButtonVisible = true;
                        IsBacklogButtonVisible = false;
                        IsPlayingButtonVisible = false;
                        IsCompletedButtonVisible = true;
                        break;
                }

                ApplyFilters();
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
