using Checkers.Services;
using Checkers.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Checkers.ForDesign
{
    public class PlayerToTurnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PlayerType playerType)
            {
                switch (playerType)
                {
                    case PlayerType.Red:
                        return "Red's turn";
                    case PlayerType.White:
                        return "White's turn";
                    default:
                        return "Unknown";
                }
            }
            return "Invalid player type";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


