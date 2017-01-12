using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aim.Data;
using Aim.Common;
using Aim.Portal;
using Aim.Portal.Data;

namespace Aim.Portal.Model
{
    [Serializable]
    public class ModelBase<T> : EntityBase<T> where T : ModelBase<T>
    {
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserInfo UserInfo
        {
            get
            {
                return PortalService.CurrentUserInfo;
            }
        }
    }
}
