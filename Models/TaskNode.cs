
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Entity;

namespace Models
{
    /// <summary>
    /// 用于可操作的任务
    /// </summary>
    public class TaskNode:FaFlowNodeEntity
    {
        /// <summary>
        /// 下步可选节点
        /// </summary>
        public List<FaFlowNodeEntity> NextNode{get;set;}

        /// <summary>
        /// 允许操作的角色
        /// </summary>
        /// <value></value>
        public List<FaRoleEntity> AllowRole{get;set;}
    }
}