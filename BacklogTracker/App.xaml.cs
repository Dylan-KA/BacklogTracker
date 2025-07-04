using BacklogTracker.Views;

namespace BacklogTracker
{
    public partial class App : Application
    {
        public App(GameListPage gameListPage)
        {
            InitializeComponent();

            //App.Current.UserAppTheme = AppTheme.Light;

            MainPage = new AppShell();
        }
    }
}
