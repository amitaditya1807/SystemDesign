namespace Cache.Core.Interfaces
{
    public interface ICache
    {
        int Get(int key);
        void Put(int key, int value);
    }
}
