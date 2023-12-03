using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace IrregularVerbs.Extensions;

public static class CollectionExtensions
{
    [Pure]
    public static List<T> Disorder<T>(this List<T> source)
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