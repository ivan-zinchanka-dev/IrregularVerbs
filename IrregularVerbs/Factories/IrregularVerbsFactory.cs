using System.Data;
using IrregularVerbs.Models;
using IrregularVerbs.Models.Verbs;
using IrregularVerbs.Models.Verbs.Components;

namespace IrregularVerbs.Factories;

public class IrregularVerbsFactory
{
    private static bool IsVolatileForm(string source)
    {
        return source.Contains('|') || source.Contains('&');
    }

    public BaseIrregularVerb FromDataRow(DataRow dataRow)
    {
        string term = dataRow[0].ToString();
        string infinitive = dataRow[1].ToString();
        string pastSimple = dataRow[2].ToString();
        string pastParticiple = dataRow[3].ToString();

        if (IsVolatileForm(pastSimple) || IsVolatileForm(pastParticiple))
        {
            return new VolatileIrregularVerb(term, 
                new VolatileForm(infinitive), 
                new VolatileForm(pastSimple),
                new VolatileForm(pastParticiple));

        }
        else
        {
            return new FixedIrregularVerb(term, infinitive, pastSimple, pastParticiple);
        }
    }

}