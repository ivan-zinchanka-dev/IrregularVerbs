using System.Diagnostics.Contracts;

namespace IrregularVerbs.Domain.Extensions;

public static class CollectionExtensions
{
    [Pure]
    public static IEnumerable<T> Disorder<T>(this IEnumerable<T> source)
    {
        Random random = new Random();

        List<T> result = new List<T>(source);
        
        for (int i = 0; i < result.Count; i++)
        {
            int randomIndex = random.Next(i, result.Count);
            T item = result[randomIndex];
            result[randomIndex] = result[i];
            result[i] = item;
        }

        return result;
    }
}