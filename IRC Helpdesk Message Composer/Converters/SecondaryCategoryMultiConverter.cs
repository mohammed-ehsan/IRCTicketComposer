using System;
using System.Collections.Generic;
using System.Globalization;

namespace IRC_Helpdesk_Message_Composer
{
    public class SecondaryCategoryMultiConverter : BaseMultiValueConverter<SecondaryCategoryMultiConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string mainCategory = null;
            Dictionary<string, Dictionary<string, List<string>>> secondaryDict = null;
            foreach (var item in values)
            {
                if (item is string)
                    mainCategory = (string)item;
                if (item is Dictionary<string, Dictionary<string, List<string>>>)
                    secondaryDict = (Dictionary<string, Dictionary<string, List<string>>>)item;
            }
            if (string.IsNullOrWhiteSpace(mainCategory) || secondaryDict == null)
                return null;
            return secondaryDict[mainCategory].Keys;

        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
