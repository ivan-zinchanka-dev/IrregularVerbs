namespace IrregularVerbs.CodeBase;

public interface IOriginal<in T>
{
    public bool Inspect(T other);
}