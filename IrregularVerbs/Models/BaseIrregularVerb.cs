namespace IrregularVerbs.Models;

public abstract class BaseIrregularVerb 
{
    public abstract string Term { get; protected set; }
    public abstract string Infinitive { get; protected set; }
    public abstract string PastSimple { get; protected set;  }
    public abstract string PastParticiple { get; protected set; }

    public abstract BaseIrregularVerb GetEmptyForm();

}