using System;

namespace IrregularVerbs.Models.Verbs;

public abstract class BaseIrregularVerb : IOriginal<BaseIrregularVerb>
{
    public abstract string Term { get; protected set; }
    public abstract string Infinitive { get; }
    public abstract string PastSimple { get; }
    public abstract string PastParticiple { get; }
    
    protected void AssertTermEquality(BaseIrregularVerb input)
    {
        if (Term != input.Term)
        {
            throw new ArgumentException("Original and answer terms should be equal", nameof(input.Term));
        }
    }

    public virtual bool Inspect(BaseIrregularVerb input)
    {
        AssertTermEquality(input);

        return Infinitive == input.Infinitive && 
               PastSimple == input.PastSimple && 
               PastParticiple == input.PastParticiple;
    }
}