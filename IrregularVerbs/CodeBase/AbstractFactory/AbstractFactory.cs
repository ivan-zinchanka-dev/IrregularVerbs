using System;

namespace IrregularVerbs.CodeBase.AbstractFactory;

public class AbstractFactory<T> : IAbstractFactory<T>
{
    private readonly Func<T> _creationMethod;
    
    public AbstractFactory(Func<T> creationMethod)
    {
        _creationMethod = creationMethod;
    }
    
    public T Create()
    {
        return _creationMethod();
    }
}