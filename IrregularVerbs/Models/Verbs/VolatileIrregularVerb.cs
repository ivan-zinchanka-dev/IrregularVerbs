using System;
using IrregularVerbs.Models.Verbs.Components;

namespace IrregularVerbs.Models.Verbs;

public class VolatileIrregularVerb : BaseIrregularVerb
{
    private readonly VolatileForm _infinitive;
    private readonly VolatileForm _pastSimple;
    private readonly VolatileForm _pastParticiple;
    
    public sealed override string Term { get; protected set; }
    public sealed override string Infinitive => _infinitive.ToString();
    public sealed override string PastSimple => _pastSimple.ToString();
    public sealed override string PastParticiple => _pastParticiple.ToString();
    
    public VolatileIrregularVerb(string term, VolatileForm infinitive, VolatileForm pastSimple, VolatileForm pastParticiple)
    {
        Term = term;
        _infinitive = infinitive;
        _pastSimple = pastSimple;
        _pastParticiple = pastParticiple;
    }

    public VolatileIrregularVerb(FixedIrregularVerb fixedIrregularVerb)
    {
        Term = fixedIrregularVerb.Term;
        _infinitive = new VolatileForm(fixedIrregularVerb.Infinitive);
        _pastSimple = new VolatileForm(fixedIrregularVerb.PastSimple);
        _pastParticiple = new VolatileForm(fixedIrregularVerb.PastParticiple);
    }
    
    private static VolatileIrregularVerb AssertTypesEquality(BaseIrregularVerb input)
    {
        if (input is VolatileIrregularVerb castedInput)
        {
            return castedInput;
        }
        else if (input is FixedIrregularVerb fixedInput)
        {
            return new VolatileIrregularVerb(fixedInput);
        }
        else
        {
            throw new ArgumentException("Types mismatch", nameof(input));
        }
    }
    
    public override bool Inspect(BaseIrregularVerb input)
    {
        if (input is VolatileIrregularVerb castedInput)
        {
            return Inspect(castedInput);
        }
        else
        {
            return false;
        }
    }

    private bool Inspect(VolatileIrregularVerb input)
    {
        AssertTermEquality(input);
        
        return _infinitive.Inspect(input._infinitive) &&
               _pastSimple.Inspect(input._pastSimple) &&
               _pastParticiple.Inspect(input._pastParticiple);
    }
}