namespace IrregularVerbs.Models;

public interface IOriginal<in T>
{
    public bool Inspect(T other);
}