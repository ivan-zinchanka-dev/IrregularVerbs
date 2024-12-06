namespace IrregularVerbs.CodeBase.AbstractFactory;

public interface IParametrizedFactory<out TProduct, in TInputData>
{
    public TProduct Create(TInputData inputData);
}