using IrregularVerbs.Domain.Models.Components;
using IrregularVerbs.Domain.Services.Localization;

namespace IrregularVerbs.Domain.Models.Verbs;

public class VolatileIrregularVerb : BaseIrregularVerb
{
    private readonly VolatileForm _infinitive;
    private readonly VolatileForm _pastSimple;
    private readonly VolatileForm _pastParticiple;
    
    public sealed override string Infinitive => _infinitive.ToString();
    public sealed override string PastSimple => _pastSimple.ToString();
    public sealed override string PastParticiple => _pastParticiple.ToString();
    
    public VolatileIrregularVerb(
        LocalizedText nativeWord, 
        VolatileForm infinitive, 
        VolatileForm pastSimple, 
        VolatileForm pastParticiple)
    {
        NativeWord = nativeWord;
        _infinitive = infinitive;
        _pastSimple = pastSimple;
        _pastParticiple = pastParticiple;
    }

    public override bool Inspect(BaseIrregularVerb other)
    {
        if (other is VolatileIrregularVerb castedInput)
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