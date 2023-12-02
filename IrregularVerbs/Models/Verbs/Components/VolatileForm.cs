using System;

namespace IrregularVerbs.Models.Verbs.Components;

public class VolatileForm
{
    private readonly Tuple<string, string> _variants;
    private CombineOperation _combineOperation;
    
    public VolatileForm(Tuple<string, string> variants, CombineOperation combineOperation)
    {
        _variants = variants;
        _combineOperation = combineOperation;
    }
    
    public override string ToString()
    {
        return $"{_variants.Item1}/{_variants.Item2}";
    }
}