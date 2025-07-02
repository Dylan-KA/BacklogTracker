using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BacklogTracker.Models
{
    [Table("games")]
    public class Game() : INotifyPropertyChanged
    {
        [Unique]
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("platform")]

        public GamePlatform Platform { get; set; }
        [Column("status")]

        public GameStatus Status { get; set; }

        private float _hoursPlayed;
        [Column("hours_played")]
        public float HoursPlayed
        {
            get => _hoursPlayed;
            set
            {
                if (_hoursPlayed != value)
                {
                    _hoursPlayed = value;
                    OnPropertyChanged();
                }
            }
        }
        [Column("notes")]
        public string Notes { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
