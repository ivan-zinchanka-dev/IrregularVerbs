using IrregularVerbs.Models.Verbs;

namespace IrregularVerbs.Models;

public class IrregularVerbAnswer
{
    public string Term { get; set; }
    public string Infinitive { get; set; }
    public string PastSimple { get; set; }
    public string PastParticiple { get; set; }
    
    public IrregularVerbAnswer()
    {
        Term = string.Empty;
        Infinitive = string.Empty;
        PastSimple = string.Empty;
        PastParticiple = string.Empty;
    }

    /*public static bool Inspect(IrregularVerbAnswer input, BaseIrregularVerb original)
    {
        
    }*/

    public override string ToString()
    {
        return $"{Infinitive}|{PastSimple}|{PastParticiple}";
    }
}