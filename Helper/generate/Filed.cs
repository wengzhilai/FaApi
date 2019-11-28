using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.generate
{
    public class Filed
    {
        /**
         * 字段名称
         */
        public String name;
        /**
         * 说明
         */
        public String remark;
        /**
         * 类型
         */
        public String type;

        /**
         * 长度
         */
        public int size;
        /**
         * 是否是必须
         */
        public Boolean required;
        /**
         * 是主键
         */
        public Boolean isKey;
    }
}
