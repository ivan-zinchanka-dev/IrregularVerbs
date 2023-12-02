using System;
using System.Collections.Generic;

namespace IrregularVerbs.Models.Verbs;

public class FixedIrregularVerb : BaseIrregularVerb
{
    public sealed override string Term { get; protected set; }
    public sealed override string Infinitive { get; protected set; }
    public sealed override string PastSimple { get; protected set; }
    public sealed override string PastParticiple { get; protected set; }
    
    public FixedIrregularVerb(string term, string infinitive, string pastSimple, string pastParticiple)
    {
        Term = term;
        Infinitive = infinitive;
        PastSimple = pastSimple;
        PastParticiple = pastParticiple;
    }
    
    public override BaseIrregularVerb GetEmptyForm()
    {
        return new FixedIrregularVerb(Term, string.Empty, string.Empty, string.Empty);
    }
}