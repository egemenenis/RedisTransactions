using StackExchange.Redis;

namespace RedisTransactions.Services
{
    public class TestService : ITestService
    {
        private IConnectionMultiplexer _redis;
        private IDatabaseAsync _database;

        public TestService(IConnectionMultiplexer connectionMultiplexer)
        {
            _redis = connectionMultiplexer;
        }

        public void Connection(int dbId = 0)
        {
            _database = _redis.GetDatabase(dbId);
        }

        public IDatabaseAsync Database(int dbId = 0)
        {
            return _redis.GetDatabase(dbId);
        }

        public async Task KeyDelete(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task<string> StringGet(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task StringSet(string key, string value)
        {
            await _database.StringSetAsync(key, value);
        }
    }
}
