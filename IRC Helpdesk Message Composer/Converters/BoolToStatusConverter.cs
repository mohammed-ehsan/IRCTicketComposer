using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC_Helpdesk_Message_Composer
{
    public class BoolToStatusConverter : BaseValueConverter<BoolToStatusConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return null;
            var input = (bool)value;
            if (input)
                return "Sent";
            else
                return "Not Sent";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
