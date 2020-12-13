using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace PomodorTimer
{
    public static class Ioc
    {
        private static readonly ServiceCollection _serviceCollection = new ServiceCollection();

        private static readonly Lazy<ServiceProvider> _lazyServiceProvider = new Lazy<ServiceProvider>(() => _serviceCollection.BuildServiceProvider());
        private static ServiceProvider ServiceProvider => _lazyServiceProvider.Value;

        public static void RegisterType<TInterface, TClass>()
            where TInterface : class
            where TClass : class, TInterface
        {
            _serviceCollection.AddTransient<TInterface, TClass>();
        }

        public static void RegisterSingleton<TInterface, TClass>()
            where TInterface : class
            where TClass : class, TInterface
        {
            _serviceCollection.AddSingleton<TInterface, TClass>();
        }

        public static TInterface Resolve<TInterface>()
            where TInterface : class
        {
            return ServiceProvider.GetService<TInterface>();
        }
    }
}
