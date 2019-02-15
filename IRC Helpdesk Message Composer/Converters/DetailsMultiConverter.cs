using System;
using System.Collections.Generic;
using System.Globalization;

namespace IRC_Helpdesk_Message_Composer
{
    public class DetailsMultiConverter : BaseMultiValueConverter<DetailsMultiConverter>
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string mainCat = null;
            string subCat = null;
            Dictionary<string, Dictionary<string, List<string>>> subCategories = null;
            foreach (var item in values)
            {
                if (item is string s)
                {
                    if (mainCat == null)
                        mainCat = s;
                    else
                        subCat = s;
                }
                if (item is Dictionary<string, Dictionary<string, List<string>>> c)
                    subCategories = c;
            }
            if (string.IsNullOrWhiteSpace(mainCat) || string.IsNullOrWhiteSpace(subCat))
                return null;
            if (subCategories == null)
                return null;
            return subCategories[mainCat][subCat];

        }

        public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
