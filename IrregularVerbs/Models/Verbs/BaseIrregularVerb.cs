using System;
using System.Collections.Generic;
using System.Linq;

namespace IrregularVerbs.Models.Verbs;

public abstract class BaseIrregularVerb
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




    public override bool Equals(object other)
    {
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        
        if (other is BaseIrregularVerb otherVerb)
        {
            if (GetHashCode() == otherVerb.GetHashCode())
            {
                return Term == otherVerb.Term && 
                       Infinitive == otherVerb.Infinitive &&
                       PastSimple == otherVerb.PastSimple &&
                       PastParticiple == otherVerb.PastParticiple;
            }
        }

        return false;
    }

    public override int GetHashCode()
    {
        string concatenationResult = string.Concat(
            Term, 
            Infinitive, 
            PastSimple, 
            PastParticiple
            );
        
        return concatenationResult.Select<char, int>(character => (int)character).Sum();
    }
}