﻿using IrregularVerbs.Domain.Models.Contracts;
using IrregularVerbs.Domain.Services.Localization;

namespace IrregularVerbs.Domain.Models.Verbs;

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

    public virtual bool Inspect(BaseIrregularVerb other)
    {
        AssertTermEquality(other);

        return Infinitive == other.Infinitive && 
               PastSimple == other.PastSimple && 
               PastParticiple == other.PastParticiple;
    }
}