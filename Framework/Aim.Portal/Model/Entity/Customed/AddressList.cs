using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aim.Portal.Model
{
    public partial class AddressList
    {
        private bool _isLeaf = false;
        private string _aim_Filter = "";

        public string Aim_Filter
        {
            get { return _aim_Filter; }
            set { _aim_Filter = value; }
        }

        public bool IsLeaf
        {
            get { return !AddressList.Exists("ParentId = ?", this.Id); }
            set { _isLeaf = value; }
        }
    }
}
