#!/bin/bash

function startMenu(){
	echo "(1)查看服务状态"
	echo "(2)开启服务"
	echo "(3)停止服务"	
	echo "(8)更新所有服务"	
	echo "(9)退出"	
}

function lookStatus(){
	echo "(0)查看所有状态"
	echo "(1)查看网关(ApiGateway)状态"
	echo "(2)查看认证(Idsvr4)状态"	
	echo "(3)查看用户(ApiUser)状态"	
	echo "(4)查看谱书(ApiFamily)状态"	
	echo "(5)查看文件(ApiUpFile)状态"	
	echo "(6)查看短信(ApiSms)状态"	
	echo "(7)查看任务(ApiQuartz)状态"	
	echo "(9)返回主菜单"	
}

function startStatus(){
	echo "(0)启动所有服务"
	echo "(1)启动网关(ApiGateway)服务"
	echo "(2)启动认证(Idsvr4)服务"	
	echo "(3)启动用户(ApiUser)服务"	
	echo "(4)启动谱书(ApiFamily)服务"	
	echo "(5)启动文件(ApiUpFile)服务"	
	echo "(6)启动短信(ApiSms)服务"	
	echo "(7)启动任务(ApiQuartz)服务"	
	echo "(9)返回主菜单"	
}

function stopStatus(){
	echo "(0)停止所有服务"
	echo "(1)停止网关(ApiGateway)服务"
	echo "(2)停止认证(Idsvr4)服务"	
	echo "(3)停止用户(ApiUser)服务"	
	echo "(4)停止谱书(ApiFamily)服务"	
	echo "(5)停止文件(ApiUpFile)服务"	
	echo "(6)停止短信(ApiSms)服务"	
	echo "(7)停止任务(ApiQuartz)服务"	
	echo "(9)返回主菜单"	
}
function runcmd(){
	echo "你选择的操作命令是$1"
	case "$1" in
	"10")
		echo 
		systemctl status ApiGateway.service
		systemctl status Idsvr4.service
		systemctl status ApiUser.service
		systemctl status ApiFamily.service
		systemctl status ApiUpFile.service
		systemctl status ApiSms.service
		systemctl status ApiQuartz.service
		;;
	"11")
		systemctl status ApiGateway.service;;
	"12")  
		systemctl status Idsvr4.service;;
	"13")
		systemctl status ApiUser.service;;
	"14")
		systemctl status ApiFamily.service;;
	"15")
		systemctl status ApiUpFile.service;;
	"16")
		systemctl status ApiSms.service;;
	"17")
		systemctl status ApiQuartz.service;;
	"20")
		echo 
		systemctl restart ApiGateway.service
		systemctl restart Idsvr4.service
		systemctl restart ApiUser.service
		systemctl restart ApiFamily.service
		systemctl restart ApiUpFile.service
		systemctl restart ApiSms.service
		systemctl restart ApiQuartz.service
		;;
	"21")
		systemctl restart ApiGateway.service;;
	"22")
		systemctl restart Idsvr4.service;;
	"23")
		systemctl restart ApiUser.service;;
	"24")
		systemctl restart ApiFamily.service;;
	"25")
		systemctl restart ApiUpFile.service;;
	"26")
		systemctl restart ApiSms.service;;
	"27")
		systemctl restart ApiQuartz.service;;
	"30")
		echo 
		systemctl stop ApiGateway.service
		systemctl stop Idsvr4.service
		systemctl stop ApiUser.service
		systemctl stop ApiFamily.service
		systemctl stop ApiUpFile.service
		systemctl stop ApiSms.service
		systemctl stop ApiQuartz.service
		;;
	"31")
		systemctl stop ApiGateway.service;;
	"32")
		systemctl stop Idsvr4.service;;
	"33")
		systemctl stop ApiUser.service;;
	"34")
		systemctl stop ApiFamily.service;;
	"35")
		systemctl stop ApiUpFile.service;;
	"36")
		systemctl stop ApiSms.service;;
	"37")
		systemctl stop ApiQuartz.service;;
	esac
}

for (( i = 0; i < 10; i++ )); do

	startMenu
	read -p "请选择服务类型：" startMenuCheck

	case "$startMenuCheck" in

	"1")
		echo 
		echo "查看服务状态"
		lookStatus
		read -p "请选择操作类型：" lookStatusCheck
		runcmd "$startMenuCheck$lookStatusCheck"
		;;
	"2")
		echo 
		echo "开启服务"
		startStatus
		read -p "请选择操作类型：" startStatusCheck
		runcmd $startMenuCheck$startStatusCheck
		;;
	"3")
		echo 
		echo "停止服务"
		stopStatus
		read -p "请选择操作类型：" stopStatusCheck
		runcmd $startMenuCheck$stopStatusCheck
		;;
	"8")
		cp -f *.service /etc/systemd/system/

		chmod +x /home/www/FaApi/ApiGateway/ApiGateway
		chmod +x /home/www/FaApi/Idsvr4/Idsvr4
		chmod +x /home/www/FaApi/ApiUser/ApiUser
		chmod +x /home/www/FaApi/ApiFamily/ApiFamily
		chmod +x /home/www/FaApi/ApiUpFile/ApiUpFile
		chmod +x /home/www/FaApi/ApiSms/ApiSms
		chmod +x /home/www/FaApi/ApiQuartz/ApiQuartz
		systemctl  daemon-reload
		firewall-cmd --permanent --add-port=9000/tcp
		firewall-cmd --reload
		firewall-cmd --list-all
		;;
	"9")
		i=10
		;;
	esac
done