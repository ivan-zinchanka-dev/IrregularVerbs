using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using IrregularVerbs.Models;
using IrregularVerbs.Models.Verbs;
using IrregularVerbs.Models.Verbs.Components;

namespace IrregularVerbs.Factories;

public class IrregularVerbsFactory
{
    private static bool IsVolatileForm(string source)
    {
        return !string.IsNullOrEmpty(source) && (source.Contains('|') || source.Contains('&'));
    }

    public BaseIrregularVerb FromDataRow(DataRow dataRow)
    {
        return CreateIrregularVerb(
            dataRow[0].ToString(), 
            dataRow[1].ToString(), 
            dataRow[2].ToString(), 
            dataRow[3].ToString());
    }

    public BaseIrregularVerb FromAnswer(IrregularVerbAnswer answer)
    {
        return CreateIrregularVerb(
            answer.Term, 
            answer.Infinitive,
            answer.PastSimple, 
            answer.PastParticiple);
    }

    private BaseIrregularVerb CreateIrregularVerb([NotNull] string term, string infinitive, string pastSimple, string pastParticiple)
    {
        if (IsVolatileForm(pastSimple) || IsVolatileForm(pastParticiple))
        {
            return new VolatileIrregularVerb(term, 
                CreateVolatileForm(infinitive), 
                CreateVolatileForm(pastSimple),
                CreateVolatileForm(pastParticiple));

        }
        else
        {
            return new FixedIrregularVerb(term, infinitive, pastSimple, pastParticiple);
        }
    }


    private static VolatileForm CreateVolatileForm(string sourceNotation)
    {
        Tuple<string, string> variants;
        CombineOperation combineOperation;
        
        if (sourceNotation.Contains('&'))
        {
            combineOperation = CombineOperation.And;
            variants = GetWords(sourceNotation, " & ");
        }
        else if (sourceNotation.Contains('|'))
        {
            combineOperation = CombineOperation.Or;
            variants = GetWords(sourceNotation, " | ");
        }
        else
        {
            combineOperation = CombineOperation.None;
            variants = new Tuple<string, string>(sourceNotation, string.Empty);
        }
        
        return new VolatileForm(variants, combineOperation);

    }

    private static Tuple<string, string> GetWords(string sourceNotation, string separator)
    {
        string[] words = sourceNotation.Split(separator);
        return new Tuple<string, string>(words[0], words[1]);
    }

}