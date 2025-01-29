namespace WebApiParking.Interface
{
    public interface ICrud<T>
    {
        Task Insert(T data);
        Task Update(T data, int Id);
        Task Delete(int Id);
    }
}
