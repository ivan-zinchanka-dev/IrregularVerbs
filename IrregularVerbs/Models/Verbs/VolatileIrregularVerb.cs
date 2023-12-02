using IrregularVerbs.Models.Verbs.Components;

namespace IrregularVerbs.Models.Verbs;

public class VolatileIrregularVerb : BaseIrregularVerb
{
    private VolatileForm _infinitive;
    private VolatileForm _pastSimple;
    private VolatileForm _pastParticiple;
    
    public sealed override string Term { get; protected set; }
    
    public sealed override string Infinitive
    {
        get => _infinitive.ToString();
        protected set => _infinitive = new VolatileForm(value);
    }

    public sealed override string PastSimple
    {
        get => _pastSimple.ToString();
        protected set => _pastSimple = new VolatileForm(value);
    }

    public sealed override string PastParticiple
    {
        get => _pastParticiple.ToString();
        protected set => _pastParticiple = new VolatileForm(value);
    }

    public VolatileIrregularVerb(string term, VolatileForm infinitive, VolatileForm pastSimple, VolatileForm pastParticiple)
    {
        Term = term;
        _infinitive = infinitive;
        _pastSimple = pastSimple;
        _pastParticiple = pastParticiple;
    }

    public override BaseIrregularVerb GetEmptyForm()
    {
        return new VolatileIrregularVerb(Term, VolatileForm.Empty, VolatileForm.Empty, VolatileForm.Empty);
    }
}