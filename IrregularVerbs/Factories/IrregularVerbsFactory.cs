using System.Data;
using System.Diagnostics.CodeAnalysis;
using IrregularVerbs.Models;
using IrregularVerbs.Models.Verbs;
using IrregularVerbs.Models.Verbs.Components;

namespace IrregularVerbs.Factories;

public static class IrregularVerbsFactory
{
    private static bool IsVolatileForm(string source)
    {
        if (!string.IsNullOrEmpty(source) && VolatileFormFactory.ContainsSeparator(source, out char foundSeparator))
        {
            CombineOperation combineOperation = VolatileFormFactory.GetCombineOperationBySeparator(foundSeparator);
            return combineOperation == CombineOperation.Or || combineOperation == CombineOperation.And;
        }

        return false;
    }

    public static BaseIrregularVerb FromDataRow(DataRow dataRow)
    {
        return CreateIrregularVerb(
            dataRow[0].ToString(), 
            dataRow[1].ToString(), 
            dataRow[2].ToString(), 
            dataRow[3].ToString());
    }

    public static BaseIrregularVerb FromAnswer(IrregularVerbAnswer answer)
    {
        return CreateIrregularVerb(
            answer.Term, 
            answer.Infinitive,
            answer.PastSimple, 
            answer.PastParticiple);
    }

    private static BaseIrregularVerb CreateIrregularVerb(string term, string infinitive, string pastSimple, string pastParticiple)
    {
        if (IsVolatileForm(pastSimple) || IsVolatileForm(pastParticiple))
        {
            return new VolatileIrregularVerb(term, 
                VolatileFormFactory.FromCombinedNotation(infinitive), 
                VolatileFormFactory.FromCombinedNotation(pastSimple),
                VolatileFormFactory.FromCombinedNotation(pastParticiple));

        }
        else
        {
            return new FixedIrregularVerb(term, infinitive, pastSimple, pastParticiple);
        }
    }
}