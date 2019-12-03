using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.generate
{
    public class PathConfig
    {
        /**
         * 消费者控制层路径
         */
        public String consumerPath;

        /**
         * 生产者路径
         */
        public String providerPath;

        /**
         * 实体路径
         */
        public String entityPath;

        /**
         * 表名
         */
        public String tableName;
        /**
         * 消费费包名
         */
        public String consumerPackageName;
        /**
         * 提供者包名
         */
        public String providerPackageName;

        /**
         * 字段字符串
         */
        public String clumStr;

        /**
         * 表名备注
         */
        public String tableNameRmark;

        /**
         * 生成文件数，用于生成文件
         */
        public List<int> makeFileNum = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };


        public String getConsumerControllerInterfaceText(String packageName, String tableName)
        {
            String contentStr = "package {0};\n" +
                    "\n" +
                    "\n" +
                    "import com.wzl.commons.model.*;\n" +
                    "import com.wzl.commons.model.dto.DtoSave;\n" +
                    "import com.wzl.commons.model.entity.Fa{1}Entity;\n" +
                    "import org.springframework.web.bind.annotation.RequestBody;\n" +
                    "\n" +
                    "public interface {1}Controller {\n" +
                    "    //region 基本方法\n" +
                    "    /**\n" +
                    "     * 查询单条\n" +
                    "     * @param inObj\n" +
                    "     * @return\n" +
                    "     */\n" +
                    "    ResultObj<Fa{1}Entity> singleByKey(@RequestBody DtoDo inObj);\n" +
                    "\n" +
                    "    /**\n" +
                    "     * 删除\n" +
                    "     * @param inObj\n" +
                    "     * @return\n" +
                    "     */\n" +
                    "    Result delete(@RequestBody DtoDo inObj);\n" +
                    "\n" +
                    "    /**\n" +
                    "     * 保存基本信息\n" +
                    "     * @param inEnt\n" +
                    "     * @return\n" +
                    "     */\n" +
                    "    ResultObj<Integer> save(@RequestBody DtoSave<Fa{1}Entity> inEnt);\n" +
                    "    //endregion\n" +
                    "\n" +
                    "    //——代码分隔线——\n" +
                    "\n" +
                    "}\n";

            contentStr = String.Format(contentStr, packageName, tableName);
            return contentStr;
        }

        public String getConsumerControllerImplText(String packageName, String tableName)
        {
            String contentStr = "package {0};\n" +
                    "\n" +
                    "import cn.hutool.core.convert.Convert;\n" +
                    "import {3}.controller.{1}Controller;\n" +
                    "import {3}.feign.{1}Service;\n" +
                    "import com.wzl.commons.model.*;\n" +
                    "import com.wzl.commons.model.dto.DtoSave;\n" +
                    "import com.wzl.commons.model.entity.Fa{1}Entity;\n" +
                    "import io.swagger.annotations.ApiOperation;\n" +
                    "import org.springframework.beans.factory.annotation.Autowired;\n" +
                    "import org.springframework.web.bind.annotation.RequestBody;\n" +
                    "import org.springframework.web.bind.annotation.RequestMapping;\n" +
                    "import org.springframework.web.bind.annotation.RequestMethod;\n" +
                    "import org.springframework.web.bind.annotation.RestController;\n" +
                    "\n" +
                    "@RestController\n" +
                    "@RequestMapping(\"{2}\")\n" +
                    "public class {1}ControllerImpl implements {1}Controller {\n" +
                    "\n" +
                    "    @Autowired\n" +
                    "    {1}Service service;\n" +
                    "\n" +
                    "    @RequestMapping(value = \"singleByKey\", method = RequestMethod.POST)\n" +
                    "    @ApiOperation(value = \"查询单个角色\")\n" +
                    "    public ResultObj<Fa{1}Entity> singleByKey(@RequestBody DtoDo inObj) {\n" +
                    "        return service.singleByKey(inObj);\n" +
                    "    }\n" +
                    "\n" +
                    "    @RequestMapping(value = \"delete\", method = RequestMethod.POST)\n" +
                    "    @ApiOperation(value = \"删除角色\")\n" +
                    "    public Result delete(@RequestBody DtoDo inObj) {\n" +
                    "        return service.delete(inObj);\n" +
                    "    }\n" +
                    "\n" +
                    "    @RequestMapping(value = \"save\", method = RequestMethod.POST)\n" +
                    "    @ApiOperation(value = \"保存角色\")\n" +
                    "    public ResultObj<Integer> save(@RequestBody DtoSave<Fa{1}Entity> inEnt) {\n" +
                    "        return service.save(inEnt);\n" +
                    "    }\n" +
                    "\n" +
                    "    //——代码分隔线——\n" +
                    "\n" +
                    "}\n";

            contentStr = String.Format(contentStr, packageName, tableName, tableName.ToLower(), this.consumerPackageName);
            return contentStr;
        }

        public String getConsumerFeignInterfaceText(String packageName, String tableName)
        {
            String contentStr = "package {0};\n" +
                    "\n" +
                    "import {0}.impl.{1}ServiceImpl;\n" +
                    "import com.wzl.commons.model.*;\n" +
                    "import com.wzl.commons.model.dto.DtoSave;\n" +
                    "import com.wzl.commons.model.entity.Fa{1}Entity;\n" +
                    "import org.springframework.cloud.openfeign.FeignClient;\n" +
                    "import org.springframework.web.bind.annotation.GetMapping;\n" +
                    "import org.springframework.web.bind.annotation.RequestBody;\n" +
                    "\n" +
                    "@FeignClient(value = \"user-provider-{2}\",url = \"http://localhost:9001\",fallback = {1}ServiceImpl.class)\n" +
                    "public interface {1}Service {\n" +
                    "    //region 基本方法\n" +
                    "    /**\n" +
                    "     * 查询单条\n" +
                    "     * @param inEnt\n" +
                    "     * @return\n" +
                    "     */\n" +
                    "    @GetMapping(value = \"/{2}/singleByKey\")\n" +
                    "    ResultObj<Fa{1}Entity> singleByKey(@RequestBody DtoDo inEnt);\n" +
                    "\n" +
                    "    /**\n" +
                    "     * 删除\n" +
                    "     * @param inEnt\n" +
                    "     * @return\n" +
                    "     */\n" +
                    "    @GetMapping(value = \"/{2}/delete\")\n" +
                    "    Result delete(@RequestBody DtoDo inEnt);\n" +
                    "\n" +
                    "    /**\n" +
                    "     * 保存基本信息\n" +
                    "     * @param inEnt\n" +
                    "     * @return\n" +
                    "     */\n" +
                    "    @GetMapping(value = \"/{2}/save\")\n" +
                    "    ResultObj<Integer> save(@RequestBody DtoSave<Fa{1}Entity> inEnt);\n" +
                    "    //endregion\n" +
                    "\n" +
                    "    //——代码分隔线——\n" +
                    "\n" +
                    "}\n";

            contentStr = String.Format(contentStr, packageName, tableName, tableName.ToLower());
            return contentStr;
        }

        public String getConsumerFeignImplText(String packageName, String tableName)
        {
            String contentStr = "package {0};\n" +
                    "\n" +
                    "import {2}.feign.{1}Service;\n" +
                    "import com.wzl.commons.model.*;\n" +
                    "import com.wzl.commons.model.dto.DtoSave;\n" +
                    "import com.wzl.commons.model.entity.Fa{1}Entity;\n" +
                    "import org.springframework.stereotype.Service;\n" +
                    "\n" +
                    "@Service\n" +
                    "public class {1}ServiceImpl implements {1}Service {\n" +
                    "    @Override\n" +
                    "    public ResultObj<Fa{1}Entity> singleByKey(DtoDo inEnt) {\n" +
                    "        ResultObj<Fa{1}Entity>  reObj=new ResultObj<> ();\n" +
                    "        reObj.success=false;\n" +
                    "        reObj.msg=\"网络有问题\";\n" +
                    "        return reObj;\n" +
                    "    }\n" +
                    "\n" +
                    "    @Override\n" +
                    "    public Result delete(DtoDo inEnt) {\n" +
                    "        Result reObj=new Result();\n" +
                    "        reObj.success=false;\n" +
                    "        reObj.msg=\"网络有问题\";\n" +
                    "        return reObj;\n" +
                    "    }\n" +
                    "\n" +
                    "    @Override\n" +
                    "    public ResultObj<Integer> save(DtoSave<Fa{1}Entity> inEnt) {\n" +
                    "        ResultObj<Integer>  reObj=new ResultObj<> ();\n" +
                    "        reObj.success=false;\n" +
                    "        reObj.msg=\"网络有问题\";\n" +
                    "        return reObj;\n" +
                    "    }\n" +
                    "\n" +
                    "    //——代码分隔线——\n" +
                    "\n" +
                    "}\n";

            contentStr = String.Format(contentStr, packageName, tableName, this.consumerPackageName);
            return contentStr;
        }

        public String getProviderControllerInterfaceText(String packageName, String tableName)
        {
            String contentStr = "package {0};\n" +
                    "\n" +
                    "\n" +
                    "import com.wzl.commons.model.*;\n" +
                    "import com.wzl.commons.model.dto.DtoSave;\n" +
                    "import com.wzl.commons.model.entity.Fa{1}Entity;\n" +
                    "import org.springframework.web.bind.annotation.RequestBody;\n" +
                    "\n" +
                    "public interface {1}Controller {\n" +
                    "    //region 基本方法\n" +
                    "    /**\n" +
                    "     * 查询单条\n" +
                    "     * @param inEnt\n" +
                    "     * @return\n" +
                    "     */\n" +
                    "    ResultObj<Fa{1}Entity> singleByKey(@RequestBody DtoDo inEnt);\n" +
                    "\n" +
                    "    /**\n" +
                    "     * 删除\n" +
                    "     * @param inEnt\n" +
                    "     * @return\n" +
                    "     */\n" +
                    "    Result delete(@RequestBody DtoDo inEnt);\n" +
                    "\n" +
                    "    /**\n" +
                    "     * 保存基本信息\n" +
                    "     * @param inEnt\n" +
                    "     * @return\n" +
                    "     */\n" +
                    "    ResultObj<Integer> save(@RequestBody DtoSave<Fa{1}Entity> inEnt);\n" +
                    "    //endregion\n" +
                    "\n" +
                    "    //——代码分隔线——\n" +
                    "\n" +
                    "}\n";

            contentStr = String.Format(contentStr, packageName, tableName);
            return contentStr;
        }

        public String getProviderControllerImplText(String packageName, String tableName)
        {
            String contentStr = "package {0};\n" +
                    "\n" +
                    "import com.user.provider.controller.{1}Controller;\n" +
                    "import com.user.provider.server.{1}Service;\n" +
                    "import com.wzl.commons.model.*;\n" +
                    "import com.wzl.commons.model.dto.DtoSave;\n" +
                    "import com.wzl.commons.model.entity.Fa{1}Entity;\n" +
                    "import io.swagger.annotations.ApiOperation;\n" +
                    "import org.springframework.beans.factory.annotation.Autowired;\n" +
                    "import org.springframework.web.bind.annotation.RequestBody;\n" +
                    "import org.springframework.web.bind.annotation.RequestMapping;\n" +
                    "import org.springframework.web.bind.annotation.RequestMethod;\n" +
                    "import org.springframework.web.bind.annotation.RestController;\n" +
                    "\n" +
                    "@RestController\n" +
                    "@RequestMapping(\"{2}\")\n" +
                    "public class {1}ControllerImpl implements {1}Controller {\n" +
                    "    @Autowired\n" +
                    "    {1}Service service;\n" +
                    "\n" +
                    "    @ApiOperation(value=\"获取{1}对象\")\n" +
                    "    @RequestMapping(value = \"singleByKey\", method = RequestMethod.POST)\n" +
                    "    public ResultObj<Fa{1}Entity> singleByKey(@RequestBody DtoDo inEnt) {\n" +
                    "        return service.singleByKey(inEnt);\n" +
                    "    }\n" +
                    "\n" +
                    "    @ApiOperation(value=\"删除{1}对象\")\n" +
                    "    @RequestMapping(value = \"delete\", method = RequestMethod.POST)\n" +
                    "    public Result delete(@RequestBody DtoDo inEnt) {\n" +
                    "        return service.delete(inEnt);\n" +
                    "    }\n" +
                    "\n" +
                    "    @ApiOperation(value=\"删除{1}对象\")\n" +
                    "    @RequestMapping(value = \"save\", method = RequestMethod.POST)\n" +
                    "    public ResultObj<Integer> save(@RequestBody DtoSave<Fa{1}Entity> inEnt) {\n" +
                    "        return service.save(inEnt);\n" +
                    "    }\n" +
                    "\n" +
                    "    //——代码分隔线——\n" +
                    "\n" +
                    "}\n";

            contentStr = String.Format(contentStr, packageName, tableName, tableName.ToLower());
            return contentStr;
        }

        public String getProviderServerInterFaceText(String packageName, String tableName)
        {
            String contentStr = "package {0};\n" +
                    "\n" +
                    "\n" +
                    "import com.wzl.commons.model.*;\n" +
                    "import com.wzl.commons.model.dto.DtoSave;\n" +
                    "import com.wzl.commons.model.entity.Fa{1}Entity;\n" +
                    "\n" +
                    "public interface {1}Service {\n" +
                    "    //region 基本方法\n" +
                    "    /**\n" +
                    "     * 查询单条\n" +
                    "     * @param inEnt\n" +
                    "     * @return\n" +
                    "     */\n" +
                    "    ResultObj<Fa{1}Entity> singleByKey(DtoDo inEnt);\n" +
                    "\n" +
                    "    /**\n" +
                    "     * 删除\n" +
                    "     * @param inEnt\n" +
                    "     * @return\n" +
                    "     */\n" +
                    "    Result delete(DtoDo inEnt);\n" +
                    "\n" +
                    "    /**\n" +
                    "     * 保存基本信息\n" +
                    "     * @param inEnt\n" +
                    "     * @return\n" +
                    "     */\n" +
                    "    ResultObj<Integer> save(DtoSave<Fa{1}Entity> inEnt);\n" +
                    "    //endregion\n" +
                    "\n" +
                    "    //——代码分隔线——\n" +
                    "\n" +
                    "}\n";

            contentStr = String.Format(contentStr, packageName, tableName);
            return contentStr;
        }

        public String getProviderServerImplText(String packageName, String tableName)
        {
            String contentStr = "package {0};\n" +
                    "\n" +
                    "import com.dependencies.mybatis.service.MyBatisService;\n" +
                    "import {0}.{1}Service;\n" +
                    "import com.wzl.commons.model.*;\n" +
                    "import com.wzl.commons.model.dto.DtoSave;\n" +
                    "import com.wzl.commons.model.entity.Fa{1}Entity;\n" +
                    "import com.wzl.commons.model.mynum.DatabaseGeneratedOption;\n" +
                    "import com.wzl.commons.retention.EntityHelper;\n" +
                    "import org.apache.commons.lang3.StringUtils;\n" +
                    "import org.springframework.beans.factory.annotation.Autowired;\n" +
                    "import org.springframework.stereotype.Service;\n" +
                    "import cn.hutool.core.convert.Convert;\n" +
                    "import java.util.ArrayList;\n" +
                    "import java.util.Arrays;\n" +
                    "import java.util.List;\n" +
                    "import java.util.stream.Collectors;\n" +
                    "\n" +
                    "@Service\n" +
                    "public class {1}ServiceImpl implements {1}Service {\n" +
                    "    @Autowired\n" +
                    "    MyBatisService<Fa{1}Entity> dapper;\n" +
                    "\n" +
                    "    EntityHelper<Fa{1}Entity> eh = new EntityHelper<>(new Fa{1}Entity());\n" +
                    "\n" +
                    "    @Override\n" +
                    "    public ResultObj<Fa{1}Entity> singleByKey(DtoDo inEnt) {\n" +
                    "        ResultObj<Fa{1}Entity> reObj=new ResultObj<>(true);\n" +
                    "        reObj.data=dapper.getSingleByPrimaryKey(eh, Convert.toInt(inEnt.key));\n" +
                    "        return reObj;\n" +
                    "    }\n" +
                    "\n" +
                    "    @Override\n" +
                    "    public Result delete(DtoDo inEnt) {\n" +
                    "        Result reObj = new Result();\n" +
                    "        Integer key = Convert.toInt(inEnt.key);\n" +
                    "        reObj.success = dapper.delete(eh, x -> x.id == key) > 0;\n" +
                    "        return reObj;\n" +
                    "    }\n" +
                    "\n" +
                    "    @Override\n" +
                    "    public ResultObj<Integer> save(DtoSave<Fa{1}Entity> inEnt) {\n" +
                    "        ResultObj<Integer> resultObj = new ResultObj<>();\n" +
                    "        eh.data = inEnt.data;\n" +
                    "        if(inEnt.whereList==null || inEnt.whereList.size()==0){\n" +
                    "            inEnt.whereList=new ArrayList<>();\n" +
                    "            inEnt.whereList.add(\"id\");\n" +
                    "        }\n" +
                    "\n" +
                    "        if (inEnt.data.id == 0) {\n" +
                    "            if (eh.dbKeyType == DatabaseGeneratedOption.Computed) {\n" +
                    "                eh.data.id = dapper.getIncreasingId(eh);\n" +
                    "                inEnt.saveFieldList.add(\"id\");\n" +
                    "            }\n" +
                    "            resultObj.data = dapper.insert(eh, inEnt.saveFieldList, null);\n" +
                    "        } else {\n" +
                    "            if(inEnt.whereList==null || inEnt.whereList.size()==0){\n" +
                    "                inEnt.whereList = Arrays.asList(\"id\");\n" +
                    "            }\n" +
                    "            resultObj.data = dapper.update(eh, inEnt.saveFieldList, inEnt.whereList);\n" +
                    "        }\n" +
                    "        resultObj.success = resultObj.data > 0;\n" +
                    "\n" +
                    "        return resultObj;\n" +
                    "    }\n" +
                    "\n" +
                    "    //——代码分隔线——\n" +
                    "\n" +
                    "}\n";

            contentStr = String.Format(contentStr, packageName, tableName);
            return contentStr;
        }


        public String getFunConsumerControllerInterfaceText(String funName, String reObjStr, String inObj, String msg)
        {
            String contentStr = "" +
                    "    /**\n" +
                    "     * {3}\n" +
                    "     * @param inEnt\n" +
                    "     * @return\n" +
                    "     */\n" +
                    "    {1} {0}(@RequestBody {2} inEnt);\n" +
                    "\n" +
                    "    //——代码分隔线——\n}";

            contentStr = String.Format(contentStr, funName, reObjStr, inObj, msg);
            return contentStr;
        }

        public String getFunConsumerControllerImplText(String funName, String reObjStr, String inObj, String msg)
        {
            String contentStr = "" +
                    "    @RequestMapping(value = \"{0}\", method = RequestMethod.POST)\n" +
                    "    @ApiOperation(value = \"{3}\")\n" +
                    "    public {1} {0}(@RequestBody {2} inEnt) {\n" +
                    "        return service.{0}(inEnt);\n" +
                    "    }\n" +
                    "\n" +
                    "    //——代码分隔线——\n}";

            contentStr = String.Format(contentStr, funName, reObjStr, inObj, msg);
            return contentStr;
        }

        public String getFunConsumerFeignInterfaceText(String funName, String reObjStr, String inObj, String msg, String tableName)
        {
            String contentStr = "" +
                    "    /**\n" +
                    "     * {3}\n" +
                    "     * @param inEnt\n" +
                    "     * @return\n" +
                    "     */\n" +
                    "    @GetMapping(value = \"/{4}/{0}\")\n" +
                    "    {1} {0}(@RequestBody {2} inEnt);\n" +
                    "\n" +
                    "    //——代码分隔线——\n}";

            contentStr = String.Format(contentStr, funName, reObjStr, inObj, msg, tableName.ToLower());
            return contentStr;
        }

        public String getFunConsumerFeignImplText(String funName, String reObjStr, String inObj)
        {
            String contentStr = "" +
                    "    public {1} {0}({2} inEnt) {\n" +
                    "        {1} reObj=new {1} ();\n" +
                    "        reObj.success=false;\n" +
                    "        reObj.msg=\"网络有问题\";\n" +
                    "        return reObj;\n" +
                    "    }\n" +
                    "\n" +
                    "    //——代码分隔线——\n}";

            contentStr = String.Format(contentStr, funName, reObjStr, inObj);
            return contentStr;
        }

        public String getFunProviderControllerInterfaceText(String funName, String reObjStr, String inObj, String msg)
        {
            String contentStr = "" +
                    "    /**\n" +
                    "     * {3}\n" +
                    "     * @param inEnt\n" +
                    "     * @return\n" +
                    "     */\n" +
                    "    {1} {0}(@RequestBody {2} inEnt);\n" +
                    "\n" +
                    "    //——代码分隔线——\n}";

            contentStr = String.Format(contentStr, funName, reObjStr, inObj, msg);
            return contentStr;
        }

        public String getFunProviderControllerImplText(String funName, String reObjStr, String inObj, String msg)
        {
            String contentStr = "" +
                    "    @ApiOperation(value=\"{3}\")\n" +
                    "    @RequestMapping(value = \"{0}\", method = RequestMethod.POST)\n" +
                    "    public {1} {0}(@RequestBody {2} inEnt) {\n" +
                    "        return service.{0}(inEnt);\n" +
                    "    }\n" +
                    "\n" +
                    "    //——代码分隔线——\n}";
            contentStr = String.Format(contentStr, funName, reObjStr, inObj, msg);
            return contentStr;
        }

        public String getFunProviderServerInterFaceText(String funName, String reObjStr, String inObj, String msg)
        {
            String contentStr = "" +
                    "    /**\n" +
                    "     * {3}\n" +
                    "     * @param inEnt\n" +
                    "     * @return\n" +
                    "     */\n" +
                    "    {1} {0}({2} inEnt);\n" +
                    "\n" +
                    "    //——代码分隔线——\n}";

            contentStr = String.Format(contentStr, funName, reObjStr, inObj, msg);
            return contentStr;
        }

        public String getFunProviderServerImplText(String funName, String reObjStr, String inObj, String msg)
        {
            String contentStr = "" +
                    "    public {1} {0}({2} inEnt) {\n" +
                    "        {1} reObj=new ResultObj<> ();\n" +
                    "        reObj.success=false;\n" +
                    "        reObj.msg=\"开发中...\";\n" +
                    "        return reObj;\n" +
                    "    }\n" +
                    "\n" +
                    "    //——代码分隔线——\n}";

            contentStr = String.Format(contentStr, funName, reObjStr, inObj);
            return contentStr;
        }


        /**
         * 修改文件内容
         *
         * @param allfileName
         * @param oldstr
         * @param newStr
         * @return
         */
        public Boolean modifyFileContent(String allfileName, String oldstr, String newStr)
        {

            string sss = File.ReadAllText(allfileName);
            string stroutput = sss.Replace(oldstr, newStr);
            File.WriteAllText(allfileName, stroutput);
            return true;
        }

        /**
         * 生成实体类字符串
         *
         * @return
         */
        public String makeEntity()
        {
            IEnumerable<Filed> allFiled;
            if (!string.IsNullOrEmpty(this.clumStr))
            {
                allFiled = getFiledList(this.clumStr);
            }
            else
            {
                allFiled = getFiledListByMysql(this.tableName);
            }
            StringBuilder attributeStr = new StringBuilder();

            foreach (var filed in allFiled)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(String.Format("\n" +
                        "        /// <summary>\n" +
                        "        /// {0}\n" +
                        "        /// </summary>\n", filed.remark,filed.type));
                if (filed.isKey)
                {
                    sb.Append("        [Key]\n");
                    sb.Append("        [DatabaseGenerated(DatabaseGeneratedOption.None)]\n");
                    sb.Append("        [Required]\n");
                }
                else if (filed.required)
                {
                    sb.Append("        [Required]\n");
                }
                sb.Append(String.Format("        [Display(Name = \"{0}\")]\n", filed.remark));
                sb.Append(String.Format("        [Column(\"{0}\")]\n", filed.name));
                sb.Append(String.Format("        public {0} {1} {{ get; set; }}\n", getFiledType(filed.type), makeCamelName(filed.name.ToLower(), false)));
                attributeStr.Append(sb);
            }

            String classStr = @"
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entity
{{

    /// <summary>
    /// {0}
    /// </summary>
    [Table(""{1}"")]
    public class {3}Entity {{

    {2}

    }}

}}
/*
{4}


{5}
*/

";
            classStr = String.Format(classStr, this.tableNameRmark, this.tableName, attributeStr, this.makeCamelName(this.tableName, true), makeAllSql(allFiled), makeAllQueryCfg(allFiled));
            return classStr;
        }

        /**
         * 生成SQL
         * @param allFiled
         * @return
         */
        private String makeAllSql(IEnumerable<Filed> allFiled)
        {
            String sql = "" +
                    "select \n" +
                    "{0} \n" +
                    "from {1}";
            String allListStr = String.Join(",\n", allFiled.Select(x=>"  " + x.name + " " + makeCamelName(x.name, false)).ToList());
            sql = String.Format(sql, allListStr, this.tableName);
            return sql;
        }

        private String makeAllQueryCfg(IEnumerable<Filed> allFiled)
        {
            String itemCfg = "" +
                    "  \"{0}\": {{\n" +
                    "    \"title\": \"{1}\",\n" +
                    "    \"type\": \"{2}\",\n" +
                    "    \"editable\": true\n" +
                    "  }}";
            String allCfgListStr = String.Join(",\n", allFiled.Select(x=>String.Format(itemCfg, makeCamelName(x.name, false), x.remark, x.type)).ToList());

            String reStr = "" +
                    "{{\n" +
                    "{0}\n" +
                    "}}";

            reStr = String.Format(reStr, allCfgListStr);
            return reStr;
        }

        /**
         * 将输入项转换成列表
         * @param clumContent PowerDesigner的行列信息
         * @return
         */
        private List<Filed> getFiledList(String clumContent)
        {
            List<Filed> allFiled = new List<Filed>();
            if (string.IsNullOrEmpty(clumContent))
            {
                return allFiled;
            }
            String[] rowsArr = clumContent.Split("\n");
            foreach (var row in rowsArr)
            {
                if (!string.IsNullOrEmpty(row))
                {
                    String[] filedArr = row.Split("\t");
                    if (filedArr.Length > 3)
                    {
                        Filed filed = new Filed();
                        filed.name = filedArr[1];
                        filed.remark = filedArr[0];
                        filed.type = getFiledType(filedArr[2]);
                        filed.size = getFiledLength(filedArr[2]);
                        filed.isKey = Convert.ToBoolean(filedArr[5]);
                        filed.required = Convert.ToBoolean(filedArr[7]);
                        allFiled.Add(filed);
                    }
                }
            }
            return allFiled;
        }

        /**
         * 获取mysql的表结构
         * @param tableName
         * @return
         */
        private IEnumerable<Filed> getFiledListByMysql(String tableName)
        {
            IEnumerable<Filed> allFiled = new List<Filed>();

            String sql = "select column_name name, column_comment remark,COLUMN_TYPE type,case IS_NULLABLE WHEN 'No' then 0 ELSE 1 END required,COLUMN_KEY='PRI' isKey  from information_schema.columns where table_name = '{0}' ;";
            sql = String.Format(sql, tableName);
            allFiled =new DapperHelper().Query<Filed>(sql);

            return allFiled;
        }

        private String getFiledType(String typeStr)
        {
            String reObj = "";
            typeStr = typeStr.Split('(')[0];
            switch (typeStr)
            {
                case "int":
                    reObj = "int";
                    break;
                case "decimal":
                    reObj = "Decimal";
                    break;
                case "datetime":
                    reObj = "DateTime";
                    break;
                default:
                    reObj = "String";
                    break;
            }
            return reObj;
        }

        private int getFiledLength(String typeStr)
        {
            int reObj = 0;
            if (!string.IsNullOrEmpty(typeStr))
            {
                String tmpStr = typeStr.Replace("(", ",").Replace(")", ",");
                String[] typeSplit = typeStr.Split(",");
                if (typeSplit.Length > 2)
                {
                    reObj = Convert.ToInt32(typeSplit[1], 0);
                }
            }
            return reObj;
        }

        /**
         * 生成骆峰命名
         * @param name 需转换的名字
         * @param fristUper 首字母是否大写
         * @return
         */
        public String makeCamelName(String name, Boolean fristUper)
        {
            String reObj = "";
            if (string.IsNullOrEmpty(name))
            {
                return reObj;
            }

            List<String> nameArr = name.Split("_").ToList();
            nameArr = nameArr.Select(x=>x.ToLower()).ToList();
            nameArr = nameArr.Select(x=>x.Substring(0, 1).ToUpper() + x.Substring(1)).ToList();
            reObj = String.Join("", nameArr);
            if (fristUper)
            {
                reObj = reObj.Substring(0, 1).ToUpper() + reObj.Substring(1);
            }
            else
            {
                reObj = reObj.Substring(0, 1).ToLower() + reObj.Substring(1);
            }
            return reObj;
        }
    }
}
