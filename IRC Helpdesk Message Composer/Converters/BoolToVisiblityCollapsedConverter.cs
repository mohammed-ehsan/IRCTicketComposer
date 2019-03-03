using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IRC_Helpdesk_Message_Composer
{
    /// <summary>
    /// Converts boolean if true to <see cref="Visibility.Visible"/>, otherwise return <see cref="Visibility.Collapsed"/>.
    /// </summary>
    public class BoolToVisiblityCollapsedConverter : BaseValueConverter<BoolToVisiblityCollapsedConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                throw new NotSupportedException("value is not bool");
            var input = (bool)value;
            if (input)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
