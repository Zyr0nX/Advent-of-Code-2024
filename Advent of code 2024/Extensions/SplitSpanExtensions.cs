namespace Advent_of_code_2024.Extensions;

public static class SpanExtensions
{
    public static int Count<T>(this MemoryExtensions.SpanSplitEnumerator<T> source) where T : IEquatable<T>
    {
        var count = 0;
        foreach (var _ in source)
        {
            count++;
        }
        return count;
    }

}