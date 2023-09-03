namespace PlatformService.Data.Repo;

public interface IRepo<T>
{
    IReadOnlyCollection<T> GetAll();
    bool IsExists(int id);
    T? Get(int id);
    T Create(T entity);
    void Update(T entity);
    void Delete(int id);
    bool SaveChanges();
}
