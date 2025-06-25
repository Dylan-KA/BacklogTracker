using CommunityToolkit.Mvvm.ComponentModel;

namespace BacklogTracker.Models
{
    public partial class Game : ObservableObject
    {
        public string Title { get; set; } = string.Empty;
        public string Platform { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public float HoursPlayed { get; set; } = 0;
        public string Notes { get; set; } = string.Empty;

        public Game(string title, string platform, string status, float hoursPlayed)
        {
            Title = title;
            Platform = platform;
            Status = status;
            HoursPlayed = hoursPlayed;
        }
    }

}
