using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Group<TKey, TElement> : IGrouping<TKey, TElement>, IReadOnlyCollection<TElement>
{
    private IGrouping<TKey, TElement> group;

    private TElement[] array;

    public TElement[] Array => array ??= group.ToArray();

    public TKey Key => group.Key;

    public int Length => array.Length;

    public int Count => group.Count();

    public TElement this[int i]
    {
        get => Array[i];
        set => Array[i] = value;
    }

    public Group(IGrouping<TKey, TElement> g) => group = g;

    public IEnumerator<TElement> GetEnumerator() => group.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (group as IEnumerable).GetEnumerator();
}

public static class Group
{
    public static Group<TKey, TSource>[] ToGroup<TKey, TSource>(this IEnumerable<IGrouping<TKey, TSource>> groups)
    {
        var result = new Group<TKey, TSource>[groups.Count()];

        int i = default;

        foreach (var group in groups)
        {
            result[i++] = new(group);
        }
        return result;
    }

    public static Group<TKey, TSource>[] GroupByToGroup<TKey, TSource>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
    {
        return source.GroupBy(keySelector).ToGroup();
    }
}