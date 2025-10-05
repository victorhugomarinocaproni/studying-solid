namespace Solid.Contracts;

public interface IRepository<T>
{
    void Add(T entity);
    T? GetById(int id);
    void Update(T entity);
}