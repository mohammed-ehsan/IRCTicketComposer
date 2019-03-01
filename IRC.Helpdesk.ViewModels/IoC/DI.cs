using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC.Helpdesk.ViewModels
{
    /// <summary>
    /// Dependency injection provider.
    /// </summary>
    public static class DI
    {
        /// <summary>
        /// Internal service provider object.
        /// </summary>
        public static IServiceProvider Provider { get; set; }

        /// <summary>
        /// Sets the service provider.
        /// </summary>
        /// <param name="provider"></param>
        public static void SetProvider(IServiceProvider provider)
        {
            Provider = provider;
        }

        /// <summary>
        /// Retreive a specified service type
        /// </summary>
        /// <typeparam name="T">Type of service to be retreived.</typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return (T)Provider.GetService(typeof(T));
        }
    }
}
