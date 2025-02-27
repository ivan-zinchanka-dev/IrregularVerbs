namespace IrregularVerbs.Domain.Factories;

public class FixedFormFactory
{
    public string FromNotation(string sourceNotation)
    {
        sourceNotation = sourceNotation.Trim().ToLower();
        
        if (sourceNotation.Length > 0 && char.IsUpper(sourceNotation[0]))
        {
            sourceNotation = $"{char.ToLower(sourceNotation[0])}{sourceNotation.Substring(1)}";
        }

        return sourceNotation;
    }
}