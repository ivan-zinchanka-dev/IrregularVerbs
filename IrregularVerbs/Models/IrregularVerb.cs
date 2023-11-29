namespace IrregularVerbs.Models;

public class IrregularVerb
{
    public string Term { get; set; }
    public string Infinitive { get;  set; }
    public string PastSimple { get;  set; }
    public string PastParticiple { get;  set; }

    public IrregularVerb GetEmptyForm()
    {
        return new IrregularVerb()
        {
            Term = this.Term,
        };
    }

}