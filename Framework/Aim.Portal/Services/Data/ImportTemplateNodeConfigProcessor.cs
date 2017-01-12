using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aim.Portal.Data
{
    public class ImportTemplateColumnNodeConfigProcessor : TemplateConfigProcessor<ImportTemplateColumnNode>
    {
        /// <summary>
        /// 前缀
        /// </summary>
        public override string Prefix
        {
            get { return "@"; }
        }

        /// <summary>
        /// 预处理
        /// </summary>
        /// <param name="configstr"></param>
        /// <returns></returns>
        protected override string Preprocess(string configstr)
        {
            string[] colTagFields = configstr.Split(',');

            for (int i = 0; i < colTagFields.Length; i++)
            {
                if (i == 0)
                {
                    if (!colTagFields[i].Contains(":"))
                    {
                        colTagFields[i] = String.Format("ColumnName:\"{0}\"", colTagFields[i]);
                    }
                }

                if (colTagFields[i].Trim() == "IsCommon")
                {
                    colTagFields[i] = "IsCommon:true";
                }

                /*else if (colTagFields[i].Trim() == "IsCheck")
                {
                    colTagFields[i] = "IsCheck:true";
                }
                 * */
            }

            return StringHelper.Join(colTagFields);
        }

        /// <summary>
        /// 后续处理
        /// </summary>
        /// <param name="configobj"></param>
        /// <returns></returns>
        protected override ImportTemplateColumnNode Postprocess(ImportTemplateColumnNode configobj)
        {
            if (String.IsNullOrEmpty(configobj.Name))
            {
                configobj.Name = configobj.ColumnName;
            }

            return configobj;
        }
    }

    public class ImportTemplateCommandNodeConfigProcessor : TemplateConfigProcessor<ImportTemplateCommandNode>
    {
        /// <summary>
        /// 前缀
        /// </summary>
        public override string Prefix
        {
            get { return "$"; }
        }

        /// <summary>
        /// 预处理
        /// </summary>
        /// <param name="configstr"></param>
        /// <returns></returns>
        protected override string Preprocess(string configstr)
        {
            string[] colTagFields = configstr.Split(',');

            for (int i = 0; i < colTagFields.Length; i++)
            {
                if (i == 0)
                {
                    if (!colTagFields[i].Contains(":"))
                    {
                        colTagFields[i] = String.Format("CommandCode:\"{0}\"", colTagFields[i]);
                    }
                }

                /*
                if (colTagFields[i].Trim() == "IsCheck")
                {
                    colTagFields[i] = "IsCheck:true";
                }
                 * */
                
                if (colTagFields[i].Trim() == "IsTransaction")
                {
                    colTagFields[i] = "IsTransaction:true";
                }
            }

            return StringHelper.Join(colTagFields);
        }

        /// <summary>
        /// 后续处理
        /// </summary>
        /// <param name="configobj"></param>
        /// <returns></returns>
        protected override ImportTemplateCommandNode Postprocess(ImportTemplateCommandNode configobj)
        {
            return configobj;
        }
    }

    public class ImportTemplatePropertyNodeConfigProcessor : TemplateConfigProcessor<ImportTemplatePropertyNode>
    {
        /// <summary>
        /// 前缀
        /// </summary>
        public override string Prefix
        {
            get { return "#"; }
        }
    }
}
