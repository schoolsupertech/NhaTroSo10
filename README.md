# NhaTroSo10

# DOCKER
## Require Pull Images
- `docker pull mcr.microsoft.com/mssql/server:2022-latest`
- `docker pull bitnami/dotnet-sdk:8.0.402` (For build from outside `visual studio` only)
- `docker pull mcr.microsoft.com/dotnet/aspnet:8.0`

## Backup & Restore Data
```shell
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourPassword>" `
-p 21433:1433 --name sql_data_nhatroso10 `
-v sql_data_nhatroso10:/var/opt/mssql `
-d mcr.microsoft.com/mssql/server:2022-latest
```

Check the container ID and make sure it's running.

`docker ps -a`

Output:
```Shell
CONTAINER ID   IMAGE                                        COMMAND                  CREATED                  STATUS          PORTS                     NAMES
a03fb52ac500   mcr.microsoft.com/mssql/server:2022-latest   "/opt/mssql/bin/perm…"   Less than a second ago   Up 16 minutes   0.0.0.0:21143->1433/tcp   sql_data_nhatroso10
```

Copy backup database from local drive to container ID: a03fb52ac500

```Shell
docker cp "E:\IT Software\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\Backup\MotelManagement2024DB.bak" a03fb52ac500:/var/opt/mssql/data
```

Open SSMS (Sql Server Management Studio) and connect to the server: `localhost,21433`

Run the restore query:
```sql
RESTORE DATABASE [MotelManagement2024DB]
FROM DISK = N'/var/opt/mssql/data/MotelManagement2024DB.bak'
WITH FILE = 1,
MOVE N'MotelManagement2024DB' TO N'/var/opt/mssql/data/MotelManagement2024DB.mdf',
MOVE N'MotelManagement2024DB_log' TO N'/var/opt/mssql/data/MotelManagement2024DB_log.ldf',
NOUNLOAD, STATS = 5
GO
```

Check database inside docker

`docker exec -it sql_data_nhatroso10 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P myPassw0rd -C`
```sql
SELECT * FROM sys.databases
GO
```

"MotelManagement2024DB" will be shown

## Docker-compose
Create `docker-compose.yml` file and paste this code:
```yml
networks:
  webapi_mssql_network:

services:
  web_api:
    image: zenithhelios/demowebapi_4
    container_name: web_api
    environment:
      - DB_HOST=mssql_server
      - DB_PORT=1433
      - DB_NAME=MotelManagement2024DB
      - DB_SA_PASSWORD=myPassw0rd
    ports:
      - "8080:8080"
      - "8081:8081"
    depends_on:
      - mssql_server
    networks:
      - webapi_mssql_network

  mssql_server:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: mssql_server
    hostname: mssql_server
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=myPassw0rd
    ports:
      - "1433:1433"
    networks:
      - webapi_mssql_network
    volumes:
      - sql_data_nhatroso10:/var/opt/mssql

volumes:
  sql_data_nhatroso10:
    external: true
```