using System;

namespace IrregularVerbs.Models.Verbs.Components;

public class VolatileForm : IOriginal<VolatileForm>
{
    private readonly Tuple<string, string> _variants;
    private readonly CombineOperation _combineOperation;
    
    public VolatileForm(Tuple<string, string> variants, CombineOperation combineOperation)
    {
        if (variants.Item1 == variants.Item2)
        {
            throw new ArgumentException("Variants cant be equal", nameof(variants));
        }

        if (string.IsNullOrEmpty(variants.Item1))
        {
            throw new ArgumentException("First variant cant be null or empty", nameof(variants.Item1));
        }

        _variants = variants;
        _combineOperation = combineOperation;
    }

    public VolatileForm(string variant) 
        : this(new Tuple<string, string>(variant, string.Empty), CombineOperation.None) { }

    public bool Inspect(VolatileForm input)
    {
        switch (_combineOperation)
        {
            case CombineOperation.None: 
                return input._variants.Item1 == _variants.Item1;
            
            case CombineOperation.And:
                return (input._variants.Item1 == _variants.Item1 ||
                        input._variants.Item1 == _variants.Item2) &&
                       (input._variants.Item2 == _variants.Item1 ||
                        input._variants.Item2 == _variants.Item2);
            
            case CombineOperation.Or:
                return (input._variants.Item1 == _variants.Item1 ||
                        input._variants.Item1 == _variants.Item2) && 
                       (string.IsNullOrEmpty(input._variants.Item2) ||
                        input._variants.Item2 == _variants.Item1 ||
                        input._variants.Item2 == _variants.Item2);
            
            default: 
                return false;
        }
    }

    public override string ToString()
    {
        return string.IsNullOrEmpty(_variants.Item2) ? 
            _variants.Item1 : 
            $"{_variants.Item1}/{_variants.Item2}";
    }
    
}