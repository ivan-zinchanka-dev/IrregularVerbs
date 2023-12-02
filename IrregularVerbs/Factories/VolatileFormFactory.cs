using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using IrregularVerbs.Models.Verbs.Components;

namespace IrregularVerbs.Factories;

public static class VolatileFormFactory
{
    private static readonly Dictionary<char, CombineOperation> Separators = new Dictionary<char, CombineOperation>()
    {
        {'&', CombineOperation.And},
        {'|', CombineOperation.Or},
        {'/', CombineOperation.Unknown},
        {',', CombineOperation.Unknown},
        {'\"', CombineOperation.Unknown},
    };


    /*private static VolatileForm FromSpreadsheet(string sourceNotation)
    {
        Tuple<string, string> variants;
        CombineOperation combineOperation;
        
        if (sourceNotation.Contains('&'))
        {
            combineOperation = CombineOperation.And;
            variants = ToVariantsTuple(sourceNotation, " & ");
        }
        else if (sourceNotation.Contains('|'))
        {
            combineOperation = CombineOperation.Or;
            variants = ToVariantsTuple(sourceNotation, " | ");
        }
        else
        {
            combineOperation = CombineOperation.None;
            variants = new Tuple<string, string>(sourceNotation, string.Empty);
        }
        
        return new VolatileForm(variants, combineOperation);

    }*/

    
    
    public static VolatileForm FromCombinedNotation(string sourceNotation)
    {
        //"  "+word+"  "+"/,\"+"  "+word+"  "
        // use Trim()
        
        Tuple<string, string> variants;
        CombineOperation combineOperation;

        if (ContainsSeparator(sourceNotation, out char separator))
        {
            variants = ToVariantsTuple(sourceNotation, separator);
            combineOperation = Separators[separator];
        }
        else
        {
            variants = ToVariantsTuple(sourceNotation);
            combineOperation = CombineOperation.None;
        }

        return new VolatileForm(variants, combineOperation);
    }


    private static bool ContainsSeparator(string sourceNotation, out char foundSeparator)
    {
        foreach (var separator in Separators)
        {
            if (sourceNotation.Contains(separator.Key))
            {
                foundSeparator = separator.Key;
                return true;
            }
        }

        foundSeparator = ' ';
        return false;
    }
    
    private static Tuple<string, string> ToVariantsTuple(string sourceNotation)
    {
        return new Tuple<string, string>(sourceNotation.Trim(), string.Empty);
    }
    
    private static Tuple<string, string> ToVariantsTuple(string sourceNotation, char separator)
    {
        string[] variantsArray = sourceNotation.Split(separator);
        
        switch (variantsArray.Length)
        {
            case 2: return new Tuple<string, string>(variantsArray[0].Trim(), variantsArray[1].Trim());
            case 1: return new Tuple<string, string>(variantsArray[0].Trim(), string.Empty);
            default: return new Tuple<string, string>(string.Empty, string.Empty);
        }
    }
}