namespace IrregularVerbs.CodeBase.AbstractFactory;

public interface IAbstractFactory<out T>
{
    public T Create();
}

public interface IAbstractFactory
{
    public object Create();
}