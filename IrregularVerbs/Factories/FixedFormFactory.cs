using IrregularVerbs.CodeBase.AbstractFactory;

namespace IrregularVerbs.Factories;

public class FixedFormFactory : IParametrizedFactory<string, string>
{
    public string Create(string sourceNotation) => FromNotation(sourceNotation);
    
    public string FromNotation(string sourceNotation)
    {
        sourceNotation = sourceNotation.Trim();
        
        if (sourceNotation.Length > 0 && char.IsUpper(sourceNotation[0]))
        {
            sourceNotation = $"{char.ToLower(sourceNotation[0])}{sourceNotation.Substring(1)}";
        }

        return sourceNotation;
    }
}