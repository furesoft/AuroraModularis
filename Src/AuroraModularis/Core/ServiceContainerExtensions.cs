using System.Runtime.InteropServices;

namespace AuroraModularis.Core;

/// <summary>
/// Extension methods for Container
/// </summary>
public static class ServiceContainerExtensions
{
    public static void RegisterForPlatform<T>(this ServiceContainer scope, OSPlatform platform, T impl) {
        if (RuntimeInformation.IsOSPlatform(platform))
        {
            scope.Register<T>(impl);
        }
    }
    
    /// <summary>
    /// Registers an implementation type for the specified interface
    /// </summary>
    /// <typeparam name="T">Interface to register</typeparam>
    /// <param name="serviceContainer">This container instance</param>
    /// <param name="type">Implementing type</param>
    /// <returns>IRegisteredType object</returns>
    public static ServiceContainer.IRegisteredType Register<T>(this ServiceContainer serviceContainer, Type type)
        => serviceContainer.Register(typeof(T), type);

    /// <summary>
    /// Registers an implementation type for the specified interface
    /// </summary>
    /// <typeparam name="TInterface">Interface to register</typeparam>
    /// <typeparam name="TImplementation">Implementing type</typeparam>
    /// <param name="serviceContainer">This container instance</param>
    /// <returns>IRegisteredType object</returns>
    public static ServiceContainer.IRegisteredType Register<TInterface, TImplementation>(this ServiceContainer serviceContainer)
        where TImplementation : TInterface
        => serviceContainer.Register(typeof(TInterface), typeof(TImplementation));

    /// <summary>
    /// Registers a factory function which will be called to resolve the specified interface
    /// </summary>
    /// <typeparam name="T">Interface to register</typeparam>
    /// <param name="serviceContainer">This container instance</param>
    /// <param name="factory">Factory method</param>
    /// <returns>IRegisteredType object</returns>
    public static ServiceContainer.IRegisteredType Register<T>(this ServiceContainer serviceContainer, Func<T> factory)
        => serviceContainer.Register(typeof(T), () => factory());

    public static ServiceContainer.IRegisteredType Register<T>(this ServiceContainer serviceContainer, T impl) =>
        serviceContainer.Register<T>(() => impl);

    /// <summary>
    /// Registers a type
    /// </summary>
    /// <param name="serviceContainer">This container instance</param>
    /// <typeparam name="T">Type to register</typeparam>
    /// <returns>IRegisteredType object</returns>
    public static ServiceContainer.IRegisteredType Register<T>(this ServiceContainer serviceContainer)
        => serviceContainer.Register(typeof(T), typeof(T));

    /// <summary>
    /// Returns an implementation of the specified interface
    /// </summary>
    /// <typeparam name="T">Interface type</typeparam>
    /// <param name="scope">This scope instance</param>
    /// <returns>Object implementing the interface</returns>
    public static T Resolve<T>(this ServiceContainer.IScope scope) => (T)scope.GetService(typeof(T));

    public static T Resolve<T>(this ServiceContainer.IScope scope, Type type) => (T)scope.GetService(type);
}