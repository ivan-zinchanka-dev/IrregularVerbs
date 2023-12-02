using System;

namespace IrregularVerbs.Models.Verbs.Components;

public class VolatileForm
{
    private readonly Tuple<string, string> _variants;
    private CombineOperation _combineOperation;
    
    public VolatileForm(Tuple<string, string> variants, CombineOperation combineOperation)
    {
        if (variants.Item1 == variants.Item2)
        {
            throw new ArgumentException("Variants cant be equal", nameof(variants));
        }

        _variants = variants;
        _combineOperation = combineOperation;
    }
    
    public override string ToString()
    {
        return string.IsNullOrEmpty(_variants.Item2) ? 
            _variants.Item1 : 
            $"{_variants.Item1}/{_variants.Item2}";
    }

    public override bool Equals(object other)
    {
        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (other is VolatileForm otherVerb)
        {
            CombineOperation commonCombineOperation =
                GetCommonCombineOperation(this._combineOperation, otherVerb._combineOperation);

            if (commonCombineOperation == CombineOperation.Unknown)
            {
                commonCombineOperation = CombineOperation.Or;
            }
            
            if (commonCombineOperation == CombineOperation.None)
            {
                return this._variants.Item1 == otherVerb._variants.Item1;
            }
            else if (commonCombineOperation == CombineOperation.Or)
            {
                return (this._variants.Item1 == otherVerb._variants.Item1 ||
                        this._variants.Item1 == otherVerb._variants.Item2) && 
                       (string.IsNullOrEmpty(this._variants.Item2) || string.IsNullOrEmpty(otherVerb._variants.Item2) ||
                        this._variants.Item2 == otherVerb._variants.Item1 ||
                        this._variants.Item2 == otherVerb._variants.Item2);
            }
            else if (commonCombineOperation == CombineOperation.And)
            {
                return !string.IsNullOrEmpty(this._variants.Item2) &&
                       (this._variants.Item1 == otherVerb._variants.Item1 ||
                        this._variants.Item1 == otherVerb._variants.Item2) &&
                       this._variants.Item2 == otherVerb._variants.Item1 ||
                       this._variants.Item2 == otherVerb._variants.Item2;
            }

        }

        return false;
    }

    
    private static int GetOperationPriority(CombineOperation operation)
    {
        switch (operation)
        {
            case CombineOperation.Unknown: return 0;
            case CombineOperation.None: return 1;
            case CombineOperation.Or: return 2;
            case CombineOperation.And: return 3;
            default: return 0;
        }
    }
    
    private static CombineOperation GetCommonCombineOperation(CombineOperation first, CombineOperation second)
    {
        return GetOperationPriority(first) > GetOperationPriority(second) ? first : second;
    }
}