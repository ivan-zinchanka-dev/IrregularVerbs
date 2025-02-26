namespace IrregularVerbs.Domain.Models.Contracts;

public interface IOriginal<in T>
{
    public bool Inspect(T other);
}