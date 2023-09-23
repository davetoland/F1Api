namespace F1Api.Mappers;

public interface IModelMapper<TFrom, TTo>
{
    IList<TTo> Map(IList<TFrom> from);
}