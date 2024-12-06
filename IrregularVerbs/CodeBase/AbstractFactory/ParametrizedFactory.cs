using System;

namespace IrregularVerbs.CodeBase.AbstractFactory;

public class ParametrizedFactory<TProduct, TInputData> : IParametrizedFactory<TProduct, TInputData>
{
    private readonly Func<TInputData, TProduct> _creationMethod;
    
    public ParametrizedFactory(Func<TInputData, TProduct> creationMethod)
    {
        _creationMethod = creationMethod;
    }
    
    public TProduct Create(TInputData inputData)
    {
        return _creationMethod(inputData);
    }
}