using System.Data;
using IrregularVerbs.Models.Answers;
using IrregularVerbs.Models.Verbs;

namespace IrregularVerbs.Factories;

public class IrregularVerbsFactory
{
    private readonly LocalizedTextFactory _localizedTextFactory;
    private readonly FixedFormFactory _fixedFormFactory;
    private readonly VolatileFormFactory _volatileFormFactory;
    
    public IrregularVerbsFactory(
        LocalizedTextFactory localizedTextFactory, 
        FixedFormFactory fixedFormFactory, 
        VolatileFormFactory volatileFormFactory)
    {
        _localizedTextFactory = localizedTextFactory;
        _fixedFormFactory = fixedFormFactory;
        _volatileFormFactory = volatileFormFactory;
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

    private bool IsVolatileForm(string source)
    {
        return !string.IsNullOrEmpty(source) && _volatileFormFactory.ContainsSeparator(source);
    }
    
    private BaseIrregularVerb CreateIrregularVerb(string term, 
        string infinitive, string pastSimple, string pastParticiple)
    {
        if (IsVolatileForm(pastSimple) || IsVolatileForm(pastParticiple))
        {
            return new VolatileIrregularVerb(_localizedTextFactory.Create(term), 
                _volatileFormFactory.FromCombinedNotation(infinitive), 
                _volatileFormFactory.FromCombinedNotation(pastSimple),
                _volatileFormFactory.FromCombinedNotation(pastParticiple));
        }
        else
        {
            return new FixedIrregularVerb(_localizedTextFactory.Create(term), 
                _fixedFormFactory.FromNotation(infinitive), 
                _fixedFormFactory.FromNotation(pastSimple), 
                _fixedFormFactory.FromNotation(pastParticiple));
        }
    }
    
}