using System;
using System.Reflection;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Aim.Data;

namespace Aim.Portal.Web
{
    /// <summary>
    /// 获取的数据
    /// </summary>
    public class DataRequest : EasyDictionary
    {
        #region 构造函数

        public DataRequest(IDictionary<string, Object> innerDirectionary)
            : base(innerDirectionary)
        {
        }

        #endregion

        #region 属性索引

        #endregion

        #region 公共方法

        public IList<T> GetList<T>(string key)
        {
            IList<T> rtn = null;

            JArray vals = null;
            if (this[key] != null)
            {
                vals = this[key] as JArray;
            }

            if (vals != null)
            {
                IEnumerable<T> ids = vals.Values<T>();
                rtn = ids.ToList();
            }

            return rtn;
        }

        #endregion
    }
}
