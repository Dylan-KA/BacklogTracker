using System.Globalization;
using BacklogTracker.Models;

namespace BacklogTracker.Helpers
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is GameStatus status)
            {
                return status switch
                {
                    GameStatus.Backlog => Application.Current.Resources["BacklogColor"],
                    GameStatus.Playing => Application.Current.Resources["PlayingColor"],
                    GameStatus.Completed => Application.Current.Resources["CompletedColor"],
                    _ => Colors.Transparent
                };
            }

            return Colors.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotImplementedException();
    }
}
