using System.Collections.Concurrent;

namespace Text.API.Data;

public static class DataContext
{
    public static ConcurrentBag<Entities.Text> Texts { get; set; } = [];
}