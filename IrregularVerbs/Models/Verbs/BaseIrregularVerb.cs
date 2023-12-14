using System;
using IrregularVerbs.Services;

namespace IrregularVerbs.Models.Verbs;

public abstract class BaseIrregularVerb : IOriginal<BaseIrregularVerb>
{
    public LocalizedText NativeWord { get; protected set; }
    public abstract string Infinitive { get; }
    public abstract string PastSimple { get; }
    public abstract string PastParticiple { get; }
    
    protected void AssertTermEquality(BaseIrregularVerb input)
    {
        if (NativeWord != input.NativeWord)
        {
            throw new ArgumentException("Original and answer terms should be equal", nameof(input.NativeWord));
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