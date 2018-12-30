
dotnet run
安装 Mono 4.4.2 或更高版本。
sudo curl -o /usr/local/bin/nuget.exe https://dist.nuget.org/win-x86-commandline/latest/nuget.exe


Scaffold-DbContext "server=45.32.134.176;userid=FA;pwd=abcdef1234;port=3306;database=fa;sslmode=none;" Pomelo.EntityFrameworkCore.MySql -OutputDir Models -Force


Install-Package MySql.Data.EntityFrameworkCore -Pre
Install-Package Pomelo.EntityFrameworkCore.MySql
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design

https://www.cnblogs.com/yangjinwang/p/9516988.html