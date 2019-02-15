using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace IRC_Helpdesk_Message_Composer
{
    public abstract class BaseMultiValueConverter<T> : MarkupExtension, IMultiValueConverter where T : new()
    {
        public static T Instance { get; set; } = new T();
        public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);
        public abstract object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture);
        public override object ProvideValue(IServiceProvider serviceProvider) => Instance;
    }
}
