using StackExchange.Redis;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            // 本機
            var configuration = "localhost:6379";

            // 連接到其他機器
            // var configuration = "other_machine_ip:6379";

            // 如果有密碼
            // var configuration = "other_machine_ip:6379,password=your_password";

            ConnectionMultiplexer redis = await ConnectionMultiplexer.ConnectAsync(configuration);
            Console.WriteLine("成功連接到 Redis 服務器");

            // 獲取數據庫實例
            IDatabase db = redis.GetDatabase();

            // 測試 SET 操作
            string key = "test:key";
            string value = "Hello Redis!";
            await db.StringSetAsync(key, value);
            Console.WriteLine($"已設置 key: {key}, value: {value}");

            // 測試帶有過期時間的 SET 操作
            string tempKey = "test:temp";
            await db.StringSetAsync(tempKey, "這個值將在10秒後過期", TimeSpan.FromSeconds(10));
            Console.WriteLine($"已設置臨時 key: {tempKey}，10秒後將過期");

            // 測試 GET 操作
            string retrievedValue = await db.StringGetAsync(key);
            Console.WriteLine($"獲取 key: {key}, 值為: {retrievedValue}");

            // 測試 EXISTS 操作
            bool exists = await db.KeyExistsAsync(key);
            Console.WriteLine($"檢查 key 是否存在: {key}, 結果: {exists}");

            // 測試 INCR 操作
            string counterKey = "test:counter";
            await db.StringSetAsync(counterKey, "0");
            long newValue = await db.StringIncrementAsync(counterKey);
            Console.WriteLine($"計數器增加後的值: {newValue}");

            // 測試 List 操作
            string listKey = "test:list";
            await db.ListRightPushAsync(listKey, new RedisValue[] { "項目1", "項目2", "項目3" });
            RedisValue[] listItems = await db.ListRangeAsync(listKey);
            Console.WriteLine($"列表內容: {string.Join(", ", listItems)}");

            // 清理測試數據
            var keys = new RedisKey[] { key, tempKey, counterKey, listKey };
            await db.KeyDeleteAsync(keys);
            Console.WriteLine("已清理所有測試數據");
        }
        catch (RedisConnectionException redisEx)
        {
            Console.WriteLine($"Redis 連接錯誤: {redisEx.Message}");
            Console.WriteLine("請確認：");
            Console.WriteLine("1. Redis 服務是否正在運行");
            Console.WriteLine("2. 連接字符串是否正確");
            Console.WriteLine("3. 防火牆設置是否正確");
        }
        catch (RedisException redisEx)
        {
            Console.WriteLine($"Redis 操作錯誤: {redisEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"其他錯誤: {ex.Message}");
        }

        Console.WriteLine("按任意鍵結束...");
        Console.ReadKey();
    }
} 