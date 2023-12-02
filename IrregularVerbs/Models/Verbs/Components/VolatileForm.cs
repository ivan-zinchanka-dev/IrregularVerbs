namespace IrregularVerbs.Models.Verbs.Components;

public class VolatileForm
{
    public VariantsMergeOperation VariantsMergeOperation { get; private set; }
    private readonly string[] _variants;

    public static VolatileForm Empty => new VolatileForm(string.Empty);
    
    public VolatileForm(string notation)
    {
        if (notation.Contains('&'))
        {
            VariantsMergeOperation = VariantsMergeOperation.And;
            _variants = notation.Split(" & ");
        }
        else if (notation.Contains('|'))
        {
            VariantsMergeOperation = VariantsMergeOperation.Or;
            _variants = notation.Split(" | ");
        }
        else
        {
            VariantsMergeOperation = VariantsMergeOperation.None;
            _variants = new string[] { notation };
        }
    }

    public override string ToString()
    {
        string output = string.Empty;
            
        for (int i = 0; i < _variants.Length; i++)
        {
            output += _variants[i];

            if (_variants.Length - i > 1)
            {
                output += '/';
            }
        }

        return output;
    }
}