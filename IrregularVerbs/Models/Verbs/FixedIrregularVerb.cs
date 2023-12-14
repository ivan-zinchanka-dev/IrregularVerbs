using IrregularVerbs.Services;

namespace IrregularVerbs.Models.Verbs;

public class FixedIrregularVerb : BaseIrregularVerb
{
    public sealed override string Infinitive { get; }
    public sealed override string PastSimple { get;}
    public sealed override string PastParticiple { get;}
    
    public FixedIrregularVerb(LocalizedText nativeWord, string infinitive, string pastSimple, string pastParticiple)
    {
        NativeWord = nativeWord;
        Infinitive = infinitive;
        PastSimple = pastSimple;
        PastParticiple = pastParticiple;
    }
}