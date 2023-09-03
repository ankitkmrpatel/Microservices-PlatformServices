namespace CommandService.Data.Repo;

public interface IRepo<T>
{
    IReadOnlyCollection<T> GetAll();
    IReadOnlyCollection<T> GetAll(Func<T, bool> func);
    bool IsExists(int id);
    bool IsExists(Func<T, bool> func);
    T? Get(int id);
    T? Get(Func<T, bool> func);
    T Create(T entity);
    void Update(T entity);
    void Delete(int id);
    bool SaveChanges();
}

