using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRC_Helpdesk_Message_Composer
{
    public static class DI
    {
        #region Public Properties

        /// <summary>
        /// Service provider instance
        /// </summary>
        public static IServiceProvider Provider { get; set; }

        #endregion

        #region Private members

        private static ServiceCollection serviceCollection = new ServiceCollection();

        #endregion

        #region Public Methods

        /// <summary>
        /// Construct the DI container. note to call this method after adding all services first.
        /// </summary>
        public static void Construct()
        {
            var configBuilder = new ConfigurationBuilder();
            Provider = serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Add a singlton service to the DI container.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public static void AddSinglton<T>(T instance) where T : class
        {
            serviceCollection.AddSingleton<T>(instance);
        }

        public static T GetService<T>()
        {
            return (T)Provider.GetService(typeof(T));
        }

        public static void AddService<TService,TImplementation>() where TService : class where TImplementation :class,TService
        {
            serviceCollection.AddScoped<TService, TImplementation>();
        }

        /// <summary>
        /// Register a type as transient in service collection.
        /// </summary>
        /// <param name="type"></param>
        public static void AddTransient(Type type)
        {
            serviceCollection.AddTransient(type);
        }

        #endregion
    }
}
