﻿namespace CaeriusNet.Caches;

/// <summary>
///     Represents a utility class that manages caching operations in memory,
///     such as storing and retrieving cached data.
/// </summary>
internal static class InMemoryCacheManager
{
    private static readonly MemoryCache MemoryCache = new(new MemoryCacheOptions());

    /// <summary>
    ///     Stores the specified value in the in-memory cache with the given cache key and expiration time.
    /// </summary>
    /// <typeparam name="T">The type of the value to be stored in the cache.</typeparam>
    /// <param name="cacheKey">The unique key used to store and retrieve the value from the cache.</param>
    /// <param name="value">The value to be stored in the cache.</param>
    /// <param name="expiration">The duration for which the cached value is valid before it expires.</param>
    internal static void Store<T>(string cacheKey, T value, TimeSpan expiration)
    {
        MemoryCache.Set(cacheKey, value!, expiration);
    }

    /// <summary>
    ///     Attempts to retrieve a cached value from the in-memory cache based on the specified cache key.
    /// </summary>
    /// <typeparam name="T">The type of value expected to be retrieved from the cache.</typeparam>
    /// <param name="cacheKey">The unique identifier for the cached item.</param>
    /// <param name="value">
    ///     An output parameter that, upon completion, will contain the retrieved value if found and of the correct type;
    ///     otherwise, contains the default value of the type.
    /// </param>
    /// <returns>
    ///     True if the cache contains an item with the specified key and the value is of the expected type; otherwise, false.
    /// </returns>
    internal static bool TryGet<T>(string cacheKey, out T? value)
    {
        if (MemoryCache.TryGetValue(cacheKey, out var cached) && cached is T typedValue)
        {
            value = typedValue;
            return true;
        }

        value = default;
        return false;
    }
}