namespace Equant.SAV2000.ComponentLibrary.MVC.Infrastructure
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Service locator for dependency injection
    /// </summary>
    public static class Locator
    {
        private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        /// <summary>
        /// Registers a service instance
        /// </summary>
        public static void Register<T>(T instance) where T : class
        {
            services[typeof(T)] = instance;
        }

        /// <summary>
        /// Resolves a service instance
        /// </summary>
        public static T Resolve<T>() where T : class
        {
            if (services.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }
            throw new InvalidOperationException($"Service of type {typeof(T).Name} not registered");
        }

        /// <summary>
        /// Checks if a service is registered
        /// </summary>
        public static bool IsRegistered<T>() where T : class
        {
            return services.ContainsKey(typeof(T));
        }

        /// <summary>
        /// Clears all registered services
        /// </summary>
        public static void Clear()
        {
            services.Clear();
        }
    }
}
