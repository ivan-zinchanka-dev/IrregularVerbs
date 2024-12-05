namespace IrregularVerbs.CodeBase.AbstractFactory;

public interface IAbstractFactory<out T>
{
    public T Create();
}