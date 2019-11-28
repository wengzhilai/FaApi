using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Helper.generate
{
    public class GenerateFile
    {

        /**
         * 创建页面
         * @param cfg
         * @throws IOException
         */
        public static void Start(PathConfig cfg)
        {

            if (cfg.makeFileNum.Contains(8))
            {
                StreamWriter fw = new StreamWriter(cfg.consumerPath + "controller/impl/" + cfg.tableName + "ControllerImpl.java");
                fw.Write(cfg.getConsumerControllerImplText(cfg.consumerPackageName + ".controller.impl", cfg.tableName));
                fw.Flush();
                fw.Close();
            }
            if (cfg.makeFileNum.Contains(7))
            {
                StreamWriter fw = new StreamWriter(cfg.consumerPath + "controller/" + cfg.tableName + "Controller.java");
                fw.Write(cfg.getConsumerControllerInterfaceText(cfg.consumerPackageName + ".controller", cfg.tableName));
                fw.Flush();
                fw.Close();
            }

            if (cfg.makeFileNum.Contains(6))
            {
                StreamWriter fw = new StreamWriter(cfg.consumerPath + "feign/impl/" + cfg.tableName + "ServiceImpl.java");
                fw.Write(cfg.getConsumerFeignImplText(cfg.consumerPackageName + ".feign.impl", cfg.tableName));
                fw.Flush();
                fw.Close();
            }

            if (cfg.makeFileNum.Contains(5))
            {
                StreamWriter fw = new StreamWriter(cfg.consumerPath + "feign/" + cfg.tableName + "Service.java");
                fw.Write(cfg.getConsumerFeignInterfaceText(cfg.consumerPackageName + ".feign", cfg.tableName));
                fw.Flush();
                fw.Close();
            }



            if (cfg.makeFileNum.Contains(4))
            {
                StreamWriter fw = new StreamWriter(cfg.providerPath + "controller/impl/" + cfg.tableName + "ControllerImpl.java");
                fw.Write(cfg.getProviderControllerImplText(cfg.providerPackageName + ".controller.impl", cfg.tableName));
                fw.Flush();
                fw.Close();
            }

            if (cfg.makeFileNum.Contains(3))
            {

                StreamWriter fw = new StreamWriter(cfg.providerPath + "controller/" + cfg.tableName + "Controller.java");
                fw.Write(cfg.getProviderControllerInterfaceText(cfg.providerPackageName + ".controller", cfg.tableName));
                fw.Flush();
                fw.Close();
            }

            if (cfg.makeFileNum.Contains(2))
            {
                StreamWriter fw = new StreamWriter(cfg.providerPath + "server/impl/" + cfg.tableName + "ServiceImpl.java");
                fw.Write(cfg.getProviderServerImplText(cfg.providerPackageName + ".server.impl", cfg.tableName));
                fw.Flush();
                fw.Close();
            }

            if (cfg.makeFileNum.Contains(1))
            {
                StreamWriter fw = new StreamWriter(cfg.providerPath + "server/" + cfg.tableName + "Service.java");
                fw.Write(cfg.getProviderServerInterFaceText(cfg.providerPackageName + ".server", cfg.tableName));
                fw.Flush();
                fw.Close();
            }
        }

        /**
         * 添加方法
         * @param cfg 配置
         * @param funName   表名
         * @param reObjStr 返回对象字符串
         * @param inObj 传入对象字符号昨晚上
         * @param msg 备注
         * @param tableName 表名
         * @throws IOException
         */
        public static void MakeNewFunction(PathConfig cfg, String funName, String reObjStr, String inObj, String msg, String tableName)
        {

            {
                String path = cfg.consumerPath + "controller/" + cfg.tableName + "Controller.java";
                String content = cfg.getFunConsumerControllerInterfaceText(funName, reObjStr, inObj, msg);
                cfg.modifyFileContent(path, "    //——代码分隔线——", content);
            }

            {

                String path = cfg.consumerPath + "controller/impl/" + cfg.tableName + "ControllerImpl.java";
                String content = cfg.getFunConsumerControllerImplText(funName, reObjStr, inObj, msg);
                cfg.modifyFileContent(path, "    //——代码分隔线——", content);
            }

            {
                String path = cfg.consumerPath + "feign/" + cfg.tableName + "Service.java";
                String content = cfg.getFunConsumerFeignInterfaceText(funName, reObjStr, inObj, msg, tableName);
                cfg.modifyFileContent(path, "    //——代码分隔线——", content);
            }

            {
                String path = cfg.consumerPath + "feign/impl/" + cfg.tableName + "ServiceImpl.java";
                String content = cfg.getFunConsumerFeignImplText(funName, reObjStr, inObj);
                cfg.modifyFileContent(path, "    //——代码分隔线——", content);
            }



            {
                String path = cfg.providerPath + "controller/" + cfg.tableName + "Controller.java";
                String content = cfg.getFunProviderControllerInterfaceText(funName, reObjStr, inObj, msg);
                cfg.modifyFileContent(path, "    //——代码分隔线——", content);
            }

            {
                String path = cfg.providerPath + "controller/impl/" + cfg.tableName + "ControllerImpl.java";
                String content = cfg.getFunProviderControllerImplText(funName, reObjStr, inObj, msg);
                cfg.modifyFileContent(path, "    //——代码分隔线——", content);
            }

            {
                String path = cfg.providerPath + "server/" + cfg.tableName + "Service.java";
                String content = cfg.getFunProviderServerInterFaceText(funName, reObjStr, inObj, msg);
                cfg.modifyFileContent(path, "    //——代码分隔线——", content);
            }

            {
                String path = cfg.providerPath + "server/impl/" + cfg.tableName + "ServiceImpl.java";
                String content = cfg.getFunProviderServerImplText(funName, reObjStr, inObj, msg);
                cfg.modifyFileContent(path, "    //——代码分隔线——", content);
            }

        }

        public static void MakeEntity(PathConfig cfg)
        {

            StreamWriter fw = new StreamWriter(cfg.entityPath + cfg.makeCamelName(cfg.tableName, true) + "Entity.cs");
            fw.Write(cfg.makeEntity());
            fw.Flush();
            fw.Close();
        }
    }
}
