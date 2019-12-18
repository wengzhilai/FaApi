#!/bin/bash

function startMenu(){
	echo "(1)查看服务状态"
	echo "(2)开启服务"
	echo "(3)停止服务"	
}

function lookStatus(){
	echo "(0)查看所有状态"
	echo "(1)查看网关(ApiGateway)状态"
	echo "(2)查看认证(Idsvr4)状态"	
}

function startStatus(){
	echo "(0)启动所有服务"
	echo "(1)启动网关(ApiGateway)服务"
	echo "(2)启动认证(Idsvr4)服务"	
}

function stopStatus(){
	echo "(0)停止所有服务"
	echo "(1)停止网关(ApiGateway)服务"
	echo "(2)停止认证(Idsvr4)服务"	
}
function exec(){
	echo "你选择的操作命令是$0"
	echo "你选择的操作命令是$1"
	echo "你选择的操作命令是$2"

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
		exec $startMenuCheck $lookStatusCheck
		;;
	"2")
		echo 
		echo "开启服务"
		startStatus
		read -p "请选择操作类型：" startStatusCheck
		exec $startMenuCheck $startStatusCheck
		;;
	"3")
		echo 
		echo "停止服务"
		stopStatus
		read -p "请选择操作类型：" stopStatusCheck
		exec $startMenuCheck $stopStatusCheck
		;;
	esac
done