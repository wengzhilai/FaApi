
dotnet run
安装 Mono 4.4.2 或更高版本。
sudo curl -o /usr/local/bin/nuget.exe https://dist.nuget.org/win-x86-commandline/latest/nuget.exe

scp -r /Users/wengzhilai/Desktop/ExportCsv/ root@45.32.134.176:~/ExportCsv/

scp -r /Users/wengzhilai/Desktop/Angular/FaAdmin/dist.zip root@155.138.209.42:/home/www/

scp  root@155.138.209.42:/root/fa1.sql /Users/wengzhilai/Desktop/dotnet/FaApi/WebApi/fa1.sql


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


create table fa_table_type (
    ID INT NOT NULL AUTO_INCREMENT,
	NAME VARCHAR(50) not null COMMENT '表名',
    TABLE_NAME VARCHAR(50) not null COMMENT '数据库中表名',
    INTRODUCE VARCHAR(50) not null COMMENT '介绍',
    ADD_TIME datetime not null COMMENT '添加时间',
	STAUTS VARCHAR(15) not null COMMENT '状态',
    PRIMARY KEY ( ID )
)

alert table fa_table_type add STAUTS VARCHAR(15) not null COMMENT '状态',

create table fa_table_column (
    ID INT NOT NULL AUTO_INCREMENT,
    TABLE_TYPE_ID INT NOT NULL ,
	NAME VARCHAR(50) not null COMMENT '表名',
    COLUMN_NAME VARCHAR(50) set utf8 not null COMMENT '数据库中列名',
    INTRODUCE VARCHAR(50) not null COMMENT '介绍',
    STAUTS VARCHAR(15) not null COMMENT '状态',
    ORDER_INDEX INT NOT NULL ,
    COLUMN_TYPE VARCHAR(15) not null COMMENT '列类型',
    COLUMN_LONG INT NULL ,
    IS_REQUIRED INT NULL,
    DEFAULT_VALUE VARCHAR(15) null,
    COLUMN_TYPE_CFG VARCHAR(15) null,
    AUTHORITY  INT NOT NULL,
    PRIMARY KEY ( ID )
)



[{
		"title": "修改",
		"class": "nb-edit",
		"openModal": "RoleEditComponent",
		"readUrl": "Table/Single",
		"apiUrl": "Table/Save"
	},
	{
		"title": "删除",
		"class": "nb-trash",
		"apiUrl": "Table/Delete",
		"confirmTip": "确定要删除吗？"
	}
]

[{
		"title": "添加",
		"class": "nb-plus",
		"click": "nowThis.Add('Table/Save')"
	},
	{
		"title": "批量删除",
		"class": "ion-delete",
		"click": "nowThis.Exec('Table/Delete','ID','删除要删除吗？')"
	},
	{
		"title": "导出",
		"class": "ion-archive",
		"click": "nowThis.onExportXls()"
	}
]

update fa_user_info set `STATUS`='存档',AUTHORITY=4 where ELDER_ID<23



DROP TEMPORARY TABLE tmp_table;
CREATE TEMPORARY TABLE tmp_table select * from fa_user_info where ELDER_ID is NOT NULL;
UPDATE fa_user_info a 
SET    a.ELDER_ID = (select ELDER_ID+1 from tmp_table b where b.ID=a.FATHER_ID)
where a.ELDER_ID is NULL;


update fa_user_info set ELDER_ID=(select ELDER_ID+1 from fa_user_info a where a.ID=106)

ALTER TABLE fa_family_books DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci



rz -b

unzip

dotnet new mstest -n mstest 

mysql.server start


# mysqldump --opt -h127.0.0.1 -uroot -pWcnfngo123 --skip-lock-tables fa>fa0730.sql 
scp  root@155.138.209.42:/root/fa0730.sql /Users/wengzhilai/Desktop/dotnet/FaApi/WebApi/fa0730.sql