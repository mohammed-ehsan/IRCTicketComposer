using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace IRC_Helpdesk_Message_Composer
{
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter where T : new()
    {
        public static T Instance { get; } = new T();
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
        public override object ProvideValue(IServiceProvider serviceProvider) => Instance;
    }
}
