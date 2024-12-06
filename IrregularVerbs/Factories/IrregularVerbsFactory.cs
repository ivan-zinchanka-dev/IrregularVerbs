using System.Data;
using IrregularVerbs.CodeBase.AbstractFactory;
using IrregularVerbs.Models.Answers;
using IrregularVerbs.Models.Configs;
using IrregularVerbs.Models.Verbs;

namespace IrregularVerbs.Factories;

public class IrregularVerbsFactory
{
    private readonly IParametrizedFactory<LocalizedText, string> _localizedTextFactory;
    
    public IrregularVerbsFactory(IParametrizedFactory<LocalizedText, string> localizedTextFactory)
    {
        _localizedTextFactory = localizedTextFactory;
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
            answer.NativeWord.Term, 
            answer.Infinitive,
            answer.PastSimple, 
            answer.PastParticiple);
    }

    private static bool IsVolatileForm(string source)
    {
        return !string.IsNullOrEmpty(source) && VolatileFormFactory.ContainsSeparator(source);
    }
    
    private BaseIrregularVerb CreateIrregularVerb(string term, 
        string infinitive, string pastSimple, string pastParticiple)
    {
        if (IsVolatileForm(pastSimple) || IsVolatileForm(pastParticiple))
        {
            return new VolatileIrregularVerb(_localizedTextFactory.Create(term), 
                VolatileFormFactory.FromCombinedNotation(infinitive), 
                VolatileFormFactory.FromCombinedNotation(pastSimple),
                VolatileFormFactory.FromCombinedNotation(pastParticiple));
        }
        else
        {
            return new FixedIrregularVerb(_localizedTextFactory.Create(term), 
                FixedFormFactory.FromNotation(infinitive), 
                FixedFormFactory.FromNotation(pastSimple), 
                FixedFormFactory.FromNotation(pastParticiple));
        }
    }
    
}