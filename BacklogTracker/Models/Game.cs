using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace BacklogTracker.Models
{
    [Table("games")]
    public class Game()
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
        [Column("hours_played")]
        public float HoursPlayed { get; set; }
        [Column("notes")]
        public string Notes { get; set; }
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
