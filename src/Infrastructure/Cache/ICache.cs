namespace Infrastructure.Cache
{
    public interface ICache
    {
        object Get(string key);

        void Set(string key, object value);
    }
}
