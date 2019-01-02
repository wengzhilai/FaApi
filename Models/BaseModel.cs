using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BaseModel
    {
        /// <summary>
        /// 验证,输入项是否满足条件
        /// </summary>
        /// <returns></returns>
        public List<ValidationResult> Validate()
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this, null, null), results, true);
            return results;
        }

        /// <summary>
        /// 本类字段说明
        /// </summary>
        public string _DictStr { get; set; }
    }
}
