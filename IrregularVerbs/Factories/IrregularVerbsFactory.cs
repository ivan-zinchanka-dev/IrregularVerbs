using System.Data;
using IrregularVerbs.Models.Answers;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Models.Verbs;

namespace IrregularVerbs.Factories;

public static class IrregularVerbsFactory
{
    private static bool IsVolatileForm(string source)
    {
        return !string.IsNullOrEmpty(source) && VolatileFormFactory.ContainsSeparator(source);
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
            answer.NativeWord.Term, 
            answer.Infinitive,
            answer.PastSimple, 
            answer.PastParticiple);
    }

    private static BaseIrregularVerb CreateIrregularVerb(string term, string infinitive, string pastSimple, string pastParticiple)
    {
        if (IsVolatileForm(pastSimple) || IsVolatileForm(pastParticiple))
        {
            return new VolatileIrregularVerb(new LocalizedText(term, App.Instance.LocalizationService), 
                VolatileFormFactory.FromCombinedNotation(infinitive), 
                VolatileFormFactory.FromCombinedNotation(pastSimple),
                VolatileFormFactory.FromCombinedNotation(pastParticiple));
        }
        else
        {
            return new FixedIrregularVerb(new LocalizedText(term, App.Instance.LocalizationService), 
                FixedFormFactory.FromNotation(infinitive), 
                FixedFormFactory.FromNotation(pastSimple), 
                FixedFormFactory.FromNotation(pastParticiple));
        }
    }
    
}