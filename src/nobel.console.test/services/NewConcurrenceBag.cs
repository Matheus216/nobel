using System.Collections.Concurrent;

namespace nobel.console.test.services;

public static class ControlConcurrenceExtension
{
    public static void AddUnicRange<T>(this ConcurrentBag<T> @this, IEnumerable<T> toAdd)
    {
        foreach (var element in toAdd)
        {
            if (!@this.Any(x => x?.Equals(element)?? false))
            @this.Add(element);
        }
    }
}
