﻿using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Query.Dto
{
    public class HeadBtnDto
    {
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 连接地址
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// 显示条件
        /// </summary>
        public List<KTV> showCondition { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public List<KV> parameter { get; set; }

        /// <summary>
        /// 宽
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// 高
        /// </summary>
        public int heigth { get; set; }
        /// <summary>
        /// 对话框类型
        /// </summary>
        public DialogMode dialogMode { get; set; }

        /// <summary>
        /// 样式
        /// </summary>
        public string cssName { get; set; }
        /// <summary>
        /// 弹出模式
        /// </summary>
        public string openModal { get; set; }
        /// <summary>
        /// 读取Api
        /// </summary>
        public string readUrl { get; set; }
        /// <summary>
        /// 保存Api
        /// </summary>
        public string saveUrl { get; set; }

    }

    public enum DialogMode
    {
        PromptAjax,
        Ajax,
        Div,
        WinOpen,
        DivDialog,
        TopDiv,
        JsFun
    }
}
