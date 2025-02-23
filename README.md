# Redis 測試程序

這是一個用於測試 Redis 連接和基本操作的 C# 控制台應用程序。

## 環境要求

### 1. Redis 服務器安裝
- 下載並安裝 Redis for Windows：
  - 方法1：通過 [Github Redis-Windows](https://github.com/microsoftarchive/redis/releases)或(https://github.com/tporadowski/redis/releases) 下載
  - 方法2：使用 Windows 套件管理器 (以管理員身份運行 PowerShell)：
    ```powershell
    winget install Redis
    ```

### 2. Redis Windows 服務設置
1. 以管理員身份運行命令提示符
2. 安裝 Redis 服務：
   ```bash
   redis-server --service-install
   ```
3. 啟動 Redis 服務：
   ```bash
   redis-server --service-start
   ```
4. 停止服務（如需要）：
   ```bash
   redis-server --service-stop
   ```
5. 卸載服務（如需要）：
   ```bash
   redis-server --service-uninstall
   ```

### 3. 環境變數設置
- 確保 Redis 安裝目錄已添加到系統的 PATH 環境變數中
- 典型的安裝路徑為：`C:\Program Files\Redis`

### 4. 專案相依套件
- StackExchange.Redis

## 使用說明

1. 確保 Redis 服務正在運行
2. 運行程序
3. 程序將自動執行以下測試：
   - 基本的 SET/GET 操作
   - 設置帶有過期時間的鍵
   - 計數器操作
   - 列表操作

## 故障排除

如果遇到連接問題，請檢查：
1. Redis 服務是否正在運行（可在服務管理器中查看）
2. 防火牆設置是否允許 Redis 端口（預設 6379）
3. 連接字符串配置是否正確 

# 測試連接
PING
# 應該返回 PONG

# 設置一個值
SET mykey "Hello Redis"

# 讀取這個值
GET mykey 