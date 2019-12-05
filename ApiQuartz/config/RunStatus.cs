using System;
using System.Collections.Generic;

namespace ApiQuartz.config
{
    public static class RunStatus
    {
        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, bool> runObj = new Dictionary<string, bool>();

        public static bool isRun(string name)
        {
            return runObj.GetValueOrDefault(name);
        }

        public static void setRun(string name)
        {
            if (runObj.ContainsKey(name))
            {
                runObj[name] = true;
            }
            else
            {
                runObj.Add(name, true);
            }

        }

        public static void remove(string name)
        {
            runObj.Remove(name);
        }
    }
}
