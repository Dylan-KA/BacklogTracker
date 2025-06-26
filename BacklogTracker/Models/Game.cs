using CommunityToolkit.Mvvm.ComponentModel;

namespace BacklogTracker.Models
{
    public partial class Game : ObservableObject
    {
        public string Title { get; set; } = string.Empty;
        public GamePlatform Platform { get; set; } = GamePlatform.Other;
        public GameStatus Status { get; set; } = GameStatus.Backlog;
        public float HoursPlayed { get; set; } = 0;
        public string Notes { get; set; } = string.Empty;

        public Game(string title, GamePlatform platform, GameStatus status, float hoursPlayed)
        {
            Title = title;
            Platform = platform;
            Status = status;
            HoursPlayed = hoursPlayed;
        }
    }

    public enum GameStatus
    {
        Backlog,
        Playing,
        Completed
    }

    public enum GamePlatform
    {
        Xbox,
        PlayStation,
        Nintendo,
        Steam,
        EpicGames,
        EA,
        Battlenet,
        UbisoftConnect,
        GOG,
        Other
    }

}
