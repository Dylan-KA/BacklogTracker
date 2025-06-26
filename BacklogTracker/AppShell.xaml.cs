using BacklogTracker.Views;

namespace BacklogTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(GameListPage), typeof(GameListPage));
            Routing.RegisterRoute(nameof(GameDetailsPage), typeof(GameDetailsPage));
        }
    }
}
