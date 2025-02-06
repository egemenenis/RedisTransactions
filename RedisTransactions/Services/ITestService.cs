using StackExchange.Redis;

namespace RedisTransactions.Services
{
    public interface ITestService
    {
        void Connection(int dbId = 0);
        Task<string> StringGet(string key);
        Task StringSet(string key, string value);
        Task KeyDelete(string key);
        IDatabaseAsync Database(int dbId=0);
    }
}
