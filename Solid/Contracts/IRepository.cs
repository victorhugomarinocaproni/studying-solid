namespace Solid.Contracts;

public interface IRepository<T>
{
    int Add(T entity);
    T? GetById(int id);
    void Update(T entity);
}