using System;
using System.Collections.Generic;
using System.Linq;

namespace IrregularVerbs.Models.Verbs;

public abstract class BaseIrregularVerb
{
    public abstract string Term { get; protected set; }
    public abstract string Infinitive { get; protected set; }
    public abstract string PastSimple { get; protected set;  }
    public abstract string PastParticiple { get; protected set; }

    public abstract BaseIrregularVerb GetEmptyForm();

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