using System.Reflection;
using MerlAIn.Shared.Modules;

namespace MerlAIn.Api.Modules;

/// <summary>
/// Finds the <see cref="IFeatureModule"/> implementations compiled into the host.
/// </summary>
/// <remarks>
/// Discovery is what makes "adding a module" a one-line change: drop the folder (or reference the
/// project) and the host picks it up. Modules are instantiated before the DI container exists, so
/// they must not take constructor dependencies — their services are registered in
/// <see cref="IFeatureModule.RegisterServices"/>.
/// </remarks>
public static class FeatureModuleDiscovery
{
    /// <summary>
    /// Scans the given assemblies for feature modules and returns them ordered by identifier, so
    /// route registration and logging stay deterministic across restarts.
    /// </summary>
    /// <param name="assemblies">The assemblies to scan.</param>
    /// <returns>The discovered modules.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown when a module cannot be instantiated, or when two modules claim the same
    /// <see cref="IFeatureModule.Id"/> or <see cref="IFeatureModule.ApiBasePath"/>.
    /// </exception>
    public static IReadOnlyList<IFeatureModule> Discover(params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(assemblies);

        List<IFeatureModule> modules = [.. assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(IsFeatureModule)
            .Select(Instantiate)
            .OrderBy(module => module.Id, StringComparer.Ordinal)];

        EnsureUnique(modules, module => module.Id, nameof(IFeatureModule.Id));
        EnsureUnique(modules, module => module.ApiBasePath, nameof(IFeatureModule.ApiBasePath));

        return modules;
    }

    /// <summary>Whether a type is a concrete, instantiable feature module.</summary>
    private static bool IsFeatureModule(Type type) =>
        type is { IsClass: true, IsAbstract: false, IsGenericTypeDefinition: false }
        && typeof(IFeatureModule).IsAssignableFrom(type);

    /// <summary>Creates a module instance, turning reflection failures into an actionable message.</summary>
    private static IFeatureModule Instantiate(Type type)
    {
        try
        {
            return (IFeatureModule)Activator.CreateInstance(type)!;
        }
        catch (Exception exception) when (exception is MissingMethodException or TargetInvocationException)
        {
            throw new InvalidOperationException(
                $"'{type.FullName}' implements {nameof(IFeatureModule)} but could not be created. " +
                "Feature modules need a public parameterless constructor.",
                exception);
        }
    }

    /// <summary>Fails fast when two modules collide on a value that must be unique.</summary>
    private static void EnsureUnique(
        IReadOnlyList<IFeatureModule> modules,
        Func<IFeatureModule, string> selector,
        string memberName)
    {
        string[] duplicates = [.. modules
            .GroupBy(selector, StringComparer.OrdinalIgnoreCase)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)];

        if (duplicates.Length > 0)
        {
            throw new InvalidOperationException(
                $"Duplicate {memberName} across feature modules: {string.Join(", ", duplicates)}.");
        }
    }
}
