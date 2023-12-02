using IrregularVerbs.Models.Verbs.Components;

namespace IrregularVerbs.Models.Verbs;

public class VolatileIrregularVerb : BaseIrregularVerb
{
    private VolatileForm _infinitive;
    private VolatileForm _pastSimple;
    private VolatileForm _pastParticiple;
    
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
    
}