using System;
using System.Collections.Generic;

namespace KeepCorrect.Common.Extensions;

public static class DictionaryExtensions
{
    /// <summary>
    /// Try get value from dictionary by key. If key is not exists – returns default of TValue
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
    {
        if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));
        if (key == null) throw new ArgumentNullException(nameof(key));
        return dictionary.TryGetValue(key, out var value) ? value : default;
    }
}