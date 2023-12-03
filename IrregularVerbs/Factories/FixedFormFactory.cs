namespace IrregularVerbs.Factories;

public static class FixedFormFactory
{
    public static string FromNotation(string sourceNotation)
    {
        sourceNotation = sourceNotation.Trim();
        
        if (sourceNotation.Length > 0 && char.IsUpper(sourceNotation[0]))
        {
            sourceNotation = $"{char.ToLower(sourceNotation[0])}{sourceNotation.Substring(1)}";
        }

        return sourceNotation;
    }
    
}