using BacklogTracker.Views;

namespace BacklogTracker
{
    public partial class App : Application
    {
        public App(GameListPage gameListPage)
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
