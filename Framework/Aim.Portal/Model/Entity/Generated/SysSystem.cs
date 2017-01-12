// Business class SysSystem generated from SysSystem
// Creator: Ray
// Created Date: [2010-03-07]

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Castle.ActiveRecord;
using Aim.Data;

namespace Aim.Portal.Model
{
    [ActiveRecord("SysSystem")]
    public partial class SysSystem : EntityBase<SysSystem>, INotifyPropertyChanged
    {

        #region Property_Names

        public static string Prop_SystemID = "SystemID";
        public static string Prop_Name = "Name";
        public static string Prop_Version = "Version";
        public static string Prop_SystemConfigData = "SystemConfigData";

        #endregion

        #region Private_Variables

        private string _systemid;
        private string _name;
        private string _version;
        private string _systemConfigData;
        private bool _isCurrent;


        #endregion

        #region Constructors

        public SysSystem()
        {
        }

        public SysSystem(
            string p_systemid,
            string p_name,
            string p_version,
            string p_systemConfigData,
            bool p_isCurrent)
        {
            _systemid = p_systemid;
            _name = p_name;
            _version = p_version;
            _systemConfigData = p_systemConfigData;
            _isCurrent = p_isCurrent;
        }

        #endregion

        #region Properties

        [PrimaryKey("SystemID", Generator = PrimaryKeyType.Custom, CustomGenerator = typeof(AimIdentifierGenerator), Access = PropertyAccess.NosetterLowercaseUnderscore)]
        public string SystemID
        {
            get { return _systemid; }
        }

        [Property("Name", Access = PropertyAccess.NosetterCamelcaseUnderscore, NotNull = true, Length = 50)]
        public string Name
        {
            get { return _name; }
            set
            {
                if ((_name == null) || (value == null) || (!value.Equals(_name)))
                {
                    _name = value;
                    NotifyPropertyChanged(SysSystem.Prop_Name);
                }
            }
        }

        [Property("Version", Access = PropertyAccess.NosetterCamelcaseUnderscore, Length = 50)]
        public string Version
        {
            get { return _version; }
            set
            {
                if ((_version == null) || (value == null) || (!value.Equals(_version)))
                {
                    _version = value;
                    NotifyPropertyChanged(SysSystem.Prop_Version);
                }
            }
        }

        [Property("SystemConfigData", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public string SystemConfigData
        {
            get { return _systemConfigData; }
            set
            {
                if (value != _systemConfigData)
                {
                    _systemConfigData = value;
                    NotifyPropertyChanged(SysSystem.Prop_SystemConfigData);
                }
            }
        }

        [Property("IsCurrent", Access = PropertyAccess.NosetterCamelcaseUnderscore)]
        public bool IsCurrent
        {
            get { return _isCurrent; }
            set
            {
                if ((!value.Equals(_isCurrent)))
                {
                    _isCurrent = value;
                    NotifyPropertyChanged(SysSystem.Prop_Version);
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            PropertyChangedEventHandler localPropertyChanged = PropertyChanged;
            if (localPropertyChanged != null)
            {
                localPropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion

    } // SysSystem
}

