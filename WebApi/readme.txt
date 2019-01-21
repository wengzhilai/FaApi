
dotnet run
安装 Mono 4.4.2 或更高版本。
sudo curl -o /usr/local/bin/nuget.exe https://dist.nuget.org/win-x86-commandline/latest/nuget.exe

scp -r /Users/wengzhilai/Desktop/ExportCsv/ root@45.32.134.176:~/ExportCsv/

mongoimport -u fa -p fa --db fa --collection fa_role --type csv --headerline --ignoreBlanks --file ~/ExportCsv/fa_role.csv
mongoimport -u fa -p fa --db fa --collection fa_login --type csv --headerline --ignoreBlanks --file ~/ExportCsv/fa_login.csv
mongoimport -u fa -p fa --db fa --collection fa_user --type csv --headerline --ignoreBlanks --file ~/ExportCsv/fa_user.csv


dotnet ef dbcontext scaffold "server=45.32.134.176;userid=FA;pwd=abcdef1234;port=3306;database=fa;sslmode=none;" "Pomelo.EntityFrameworkCore.Mysql" -o Entity


Install-Package MySql.Data.EntityFrameworkCore -Pre
Install-Package Pomelo.EntityFrameworkCore.MySql
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design

https://www.cnblogs.com/yangjinwang/p/9516988.html

Microsoft.AspNetCore.Authentication.AuthenticationBuilder IServiceCollection.AddAuthentication(Action<Microsoft.AspNetCore.Authentication.AuthenticationOptions> configureOptions)

/Users/wengzhilai/Desktop/ExportCsv
/Users/wengzhilai/Desktop/dotnet/FaApi/WebApi/Controllers/PublicController.cs
/Users/wengzhilai/Desktop/dotnet/FaApi/WebApi/readme.txt