using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Aim.Common;
using Aim.Portal.Model;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Queries;
using System.Data.SqlClient;
using System.Data;
using NHibernate;
using Aim.Common.Service;
using Aim.Data;

namespace Aim.Portal.Model
{
    public partial class WebPartRule
    {
        //获得模版布局html
		public static string GetBlocks(string userId,string userName,ref string LayoutType,string blockType,string templateId,string isManage)
		{
			string html = String.Empty;

            WebPartTemplate dr = GetWebPartTemplateRecord(userId,userName, blockType, templateId, isManage);

            if (dr != null)
            {
                string withT = dr.TemplateColWidth;
                string templateString = dr.TemplateString;
                string templateXml = dr.TemplateXml;

                DataCollection list = new DataCollection(templateXml);
                string[] cols = templateString.Split(';');
                LayoutType = cols.Length.ToString();
                string[] widths = withT.Split(',');
                int i = 0;

                foreach (string col in cols)
                {
                    html += GetCols(col, widths[i], i, list, userId);
                    i++;
                }
            }

			return html;
		}

		//获得小图表列表
		public static string GetIcons()
		{
			if(Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("/Modules/WebPart/Icons")))
			{
				string imgs = "";
                string[] files = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("/Modules/WebPart/Icons"));
				foreach(string file in files)
				{
					if(!Directory.Exists(file))
					{
						FileInfo fil = new FileInfo(file);
						if(fil.Extension.ToLower()!=".gif")
							continue;
						string fileName = fil.Name;
                        imgs += "<img src='/Modules/WebPart/Icons/" + fileName + "' onclick=\"changeDragIcon('/Modules/WebPart/Icons/" + fileName + "','" + fileName + "')\" class='imglink'>";
					}
				}
				return imgs;
			}
			else
			{
				return "不存在存放图标的目录,请联系管理员!";
			}
		}

		//获得小图表列表
		public static string SetIcon(string userId,string blockId,string blockType,string imgUrl,string templateId,string isManage)
		{
			WebPartTemplate dr = GetWebPartTemplateRecord(userId,blockType,templateId,isManage);
			string tempXml = dr.TemplateXml;
			DataCollection list = new DataCollection(tempXml);
			DataElement item = list.GetElement("Id",blockId);
			item.SetAttr("BlockImage",imgUrl);
			dr.TemplateXml = list.ToString();
			dr.Update();
			return "";
		}
		public static string GetCols(string col,string width,int i,DataCollection list,string userId)
		{
			string blockHtmls = "";
			string colBeginHtml = "<DIV class=\"col_div\" id=\"col_{0}\" style=\"WIDTH: {1}\">";
			string colEndHtml = @"<DIV class='drag_div no_drag' id='col_{0}_hidden_div'>
					<DIV id='col_{0}_hidden_div_h'></DIV>
				</DIV></DIV>";
			string BlockBeginHtml = "<DIV class=\"drag_div\" id=\"drag_{0}\" style=\"BORDER-COLOR: {1}; BACKGROUND: #fff;Height:{2}\">";
			string BlockEndHtml = @"<DIV id='drag_switch_{0}'>
						<DIV class='drag_editor' id='drag_editor_{0}' style='DISPLAY: none'>
							<DIV id='loadeditorid_{0}' style='WIDTH: 100px'><IMG src='/Modules/WebPart/loading.gif'><SPAN id='loadeditortext_{0}' style='COLOR: #333'></SPAN></DIV>
						</DIV>
						<DIV class='drag_content' id='drag_content_{0}' style='background-color:{2}'>
							<DIV id='loadcontentid_{0}' style='WIDTH: 100px'><IMG src='/Modules/WebPart/loading.gif'><SPAN id='loadcontenttext_{0}' style='COLOR: #333'></SPAN></DIV>
						</DIV>
					</DIV>
				</DIV>
                <SCRIPT>{1}</SCRIPT>";
			blockHtmls +=string.Format(colBeginHtml,Convert.ToString(i+1),width);
			string[] blockIds = col.Split(',');
			//每列的各块
			foreach(string blockId in blockIds)
			{
				if(blockId!="" && WebPart.Exists<String>(blockId))
				{
                    WebPart bl = WebPart.Find(blockId);
					//读取模版中实例化参数
					DataElement item = list.GetElement("Id",blockId);
					if(item!=null)
					{
						bl.BlockKey = item.GetAttr("BlockKey");
						bl.BlockImage = item.GetAttr("BlockImage");
						bl.BlockTitle = item.GetAttr("BlockTitle");
						bl.BlockType = item.GetAttr("BlockType");
						bl.ColorValue = item.GetAttr("ColorValue");
						bl.Color = item.GetAttr("Color");
						bl.RepeatItemCount = int.Parse(item.GetAttr("RepeatItemCount"));
                        bl.RepeatItemLength = int.Parse(item.GetAttr("RepeatItemLength"));
                        bl.DelayLoadSecond = int.Parse(item.GetAttr("DelayLoadSecond"));
					}
					blockHtmls +=string.Format(BlockBeginHtml,blockId,bl.ColorValue,bl.DefaultHeight);
                    WebPartExt wb= new WebPartExt(bl, userId);
                    blockHtmls += wb.GetHeadHtml();
					//是否延时加载
					if(bl.DelayLoadSecond.ToString().Trim()!="0")
					{
						blockHtmls +=string.Format(BlockEndHtml,blockId,"window.setTimeout(\"loadDragContent('"+blockId+"','"+bl.RepeatItemCount+"')\","+Convert.ToString(bl.DelayLoadSecond*1000)+");"+"\n"+bl.RelateScript,bl.ContentColor);
					}
					else
					{
                        blockHtmls += string.Format(BlockEndHtml, blockId, "loadDragContent('" + blockId + "','" + bl.RepeatItemCount + "');" + "\n" + bl.RelateScript, bl.ContentColor);
					}
				}
			}
			blockHtmls +=string.Format(colEndHtml,Convert.ToString(i+1));
			return blockHtmls;
		}

		//保存模版布局
		public static void SaveGetBlocks(string userId,string TemplateString,string blockType,string templateId,string isManage)
		{
            WebPartTemplate dr = GetWebPartTemplateRecord(userId, blockType, templateId, isManage);
			//DbRecord dr = new DbRecord(daAccess,"DoorTemplate","UserId",userId,"IsDefault","T","BlockType",blockType);
			dr.TemplateString = TemplateString;
			dr.Update();
		}

		//保存模版布局的改变
		public static void ChangeColumns(string userId,string columns,string templateString,string blockType,string templateId,string isManage,params string[] lists)
		{
            WebPartTemplate dr = GetWebPartTemplateRecord(userId, blockType, templateId, isManage);
			//DbRecord dr = new DbRecord(daAccess,"DoorTemplate","UserId",userId,"IsDefault","T","BlockType",blockType);
			int cols = int.Parse(columns);
			int old = dr.TemplateColWidth.Split(',').Length;
			dr.TemplateString =templateString;
			string widths = "";
			for(int i=0;i<cols;i++)
			{
				widths +=lists[i]+"%,";
			}
			widths = widths.TrimEnd(',');
			dr.TemplateColWidth = widths;
			dr.Update();
		}

		//保存模版布局的各个宽度
		public static void ChangeColumnsWidth(string userId,string columns,string blockType,string templateId,string isManage,params string[] lists)
		{
            WebPartTemplate dr = GetWebPartTemplateRecord(userId, blockType, templateId, isManage);
			//DbRecord dr = new DbRecord(daAccess,"DoorTemplate","UserId",userId,"IsDefault","T","BlockType",blockType);
			int cols = int.Parse(columns);
			string widths = "";
			for(int i=0;i<cols;i++)
			{
				widths +=lists[i]+"%,";
			}
			widths = widths.TrimEnd(',');
			dr.TemplateColWidth = widths;
			dr.Update();
		}

		//获得block设置模版
		public static string GetUserBlock(string userId,string blockId,string blockType,string templateId,string isManage)
		{
			string temp=@"<div class='block_editor_a'>标题：</div>
			<div class='block_editor_b'><input type='text' maxlength='20' style='width:100px' name='blocktitle_{0}' class='block_input'
					id='blocktitle' onchange=changeDragText('{0}') value='{1}' ></div>
			<div class='block_editor_a'>显示条数：</div>
			<div class='block_editor_b'><input type='text' maxlength='2' name='blockrow' style='width:30px' class='block_input'
					id='blockrow_{0}' value='{2}' onkeyup=value=value.replace(/[^0-9.]/g,''); onbeforepaste=value=value.replace(/[^0-9.]/g,'');></div>
			<div class='block_editor_a' style='display:none;'>内容长度：</div>
			<div class='block_editor_b' style='display:none;'><input type='text' maxlength='2' name='subjectlength' style='width:30px' class='block_input'
					id='subjectlength_{0}' value='{3}' onkeyup=value=value.replace(/[^0-9.]/g,''); onbeforepaste=value=value.replace(/[^0-9.]/g,'');></div>
			<div class='block_editor_a'>颜色：</div>
			<div class='block_editor_b'>
				<div>
					<div class='colorblock' style='background:#FFB0B0;cursor:hand' onclick=switchTpl('{0}','navarat')></div>
					<div class='colorblock' style='background:#FFC177;cursor:hand' onclick=switchTpl('{0}','orange')></div>
					<div class='colorblock' style='background:#FFED77;cursor:hand' onclick=switchTpl('{0}','yellow')></div>
					<div class='colorblock' style='background:#CBE084;cursor:hand' onclick=switchTpl('{0}','green')></div>
					<div class='colorblock' style='background:#A1D9ED;cursor:hand' onclick=switchTpl('{0}','blue')></div>
					<div class='colorblock' style='background:#BBBBBB;cursor:hand' onclick=switchTpl('{0}','gray')></div>
				</div>
			</div>
			<div class='block_editor_a'></div>
			<div class='block_editor_b'>
				<div>
					<div class='colorblock' style='background:#e55147;cursor:hand' onclick=switchTpl('{0}','o_navarat')></div>
					<div class='colorblock' style='background:#fed9a5;cursor:hand' onclick=switchTpl('{0}','o_orange')></div>
					<div class='colorblock' style='background:#72ca97;cursor:hand' onclick=switchTpl('{0}','o_yellow')></div>
					<div class='colorblock' style='background:#85d35e;cursor:hand' onclick=switchTpl('{0}','o_green')></div>
					<div class='colorblock' style='background:#5690e4;cursor:hand' onclick=switchTpl('{0}','o_blue')></div>
					<div class='colorblock' style='background:#a6baec;cursor:hand' onclick=switchTpl('{0}','o_gray')></div>
				</div>
				<input type='hidden' name='blocktpl_{0}' id='blocktpl_{0}' value='{4}' colorvalue='{5}'>
			</div>
			<div style='width:100%;'><input class='block_button' type='button' value='确定' onclick=saveDragEditor('{0}') ID='Button1'
					NAME='Button1'> <input type='button' value='取消' class='block_button' onclick=modifyBlock('{0}') ID='Button2'
					NAME='Button2'></div>";
			//DbRecord dr = new DbRecord(daAccess,"DoorTemplate","UserId",userId,"IsDefault","T","BlockType",blockType);
            WebPartTemplate dr = GetWebPartTemplateRecord(userId, blockType, templateId, isManage);
			string tempXml = dr.TemplateXml;
			DataCollection list = new DataCollection(tempXml);
			DataElement item = list.GetElement("Id",blockId);
			temp = string.Format(temp,item.GetAttr("Id"),item.GetAttr("BlockTitle"),item.GetAttr("RepeatItemCount"),item.GetAttr("RepeatItemLength"),item.GetAttr("Color"),item.GetAttr("ColorValue"));
			return temp;
		}

		//更新用户自己的block设置
        public static bool UpdateUserBlock(string userId, string blockId, string title, string rptcount, string rptlength, string color, string colorvalue, string blockType, string templateId, string isManage)
        {
            try
            {
                WebPartTemplate dr = GetWebPartTemplateRecord(userId, blockType, templateId, isManage);
                //DbRecord dr = new DbRecord(daAccess,"DoorTemplate","UserId",userId,"IsDefault","T","BlockType",blockType);
                string tempXml = dr.TemplateXml;
                DataCollection list = new DataCollection(tempXml);
                DataElement item = list.GetElement("Id", blockId);
                item.SetAttr("BlockTitle", title);
                item.SetAttr("RepeatItemCount", rptcount);
                item.SetAttr("RepeatItemLength", rptlength);
                item.SetAttr("Color", color);
                item.SetAttr("ColorValue", colorvalue);
                dr.TemplateXml = list.ToString();
                dr.Update();
                return true;
            }
#if debug
                catch (Exception ex)
#else
            catch
#endif
            { return false; }
        }

		//获取所有版块
		public static string GetAllBlockNames(string userId,string blockType,string templateId,string isManage,string DBName)
		{
			string temp =@"<div align='left' class='panelcon'>
					<img src='{2}' align='absmiddle' class='panelicon'>{1} <img src='/Modules/WebPart/add2.gif' align='absmiddle' class='paneladdimg' onclick=addBlock('{0}','{1}');>
				</div>
				";
			string tempDept =@"<div align='left' class='panelcon'>
					<img src='{2}' align='absmiddle' class='panelicon'><font color='blue'>{1}</font> <img src='/Modules/WebPart/add2.gif' align='absmiddle' class='paneladdimg' onclick=addBlock('{0}','{1}');>
				</div>
				";
			string html = "";
			/*string sql = "select Id,BlockTitle,BlockImage,BlockType from DoorBlock where IsHidden='F' and BlockType='"+blockType+"'";
			if ( blockType == "Dept" )
				sql = "select Id,BlockTitle,BlockImage,BlockType from DoorBlock where IsHidden='F' and (BlockType='Portal' OR (BlockType='"+blockType+"' and TemplateId = '"+templateId+"'))";
			*/
			/*string sql = "SELECT Id,BlockTitle,BlockImage,BlockType FROM DoorBlock DB WHERE IsHidden='F' AND BlockType='Portal' OR "+
                "EXISTS( SELECT * FROM " + DBName + "..NewsType PIC WHERE DB.BlockKey = PIC.Id AND ( PIC.AllowQueryId IS NULL OR PIC.AllowQueryId = '' OR PIC.AllowQueryId LIKE '%" + userId + "%' OR " +
				"EXISTS( SELECT * FROM OgUserProperty WHERE PATINDEX ( '%,'+Property+',%' , ','+REPLACE(PIC.AllowQueryId,'_',',')+',' ) > 0 AND LEN(Property)>0 AND UserId = '"+userId+"') ) ) ORDER BY BlockType DESC,BlockTitle";*/
            string sql = "SELECT Id,BlockTitle,BlockImage,BlockType FROM WebPart where isHidden=0";
            if (blockType != null && blockType.Trim() != "")
                sql = "SELECT Id,BlockTitle,BlockImage,BlockType FROM WebPart where isHidden=0 and BlockType='"+blockType+"'";
           /* ISession sess = ActiveRecordMediator.GetSessionFactoryHolder().CreateSession(typeof(ActiveRecordBase));
            SqlDataAdapter sda = new SqlDataAdapter(sql, sess.Connection.ConnectionString);
            DataTable ds = new DataTable();
            sda.Fill(ds);
            ActiveRecordMediator.GetSessionFactoryHolder().ReleaseSession(sess);*/
            DataTable ds = DataHelper.QueryDataTable(sql);
            DataCollection list = CommonDataHelper.DataTableToDataCollection(ds);
            ds.Dispose();
            //sda.Dispose();
			foreach(DataElement item in list.GetElements(""))
			{
				if ( item.GetAttr("BlockType") == "Dept" )
					html +=string.Format(tempDept,item.GetAttr("Id"),item.GetAttr("BlockTitle"),item.GetAttr("BlockImage"));
				else
					html +=string.Format(temp,item.GetAttr("Id"),item.GetAttr("BlockTitle"),item.GetAttr("BlockImage"));
			}
			return html;
		}

		//获取单个块的html
		public static string GetOneBlockHtmls(string blockId,string userId)
		{
			string blockHtmls = "";
            string BlockBeginHtml = "<DIV class=\"drag_div\" id=\"drag_{0}\" style=\"BORDER-COLOR: {1}; BACKGROUND: #fff;Height:{2}\">";
			string BlockEndHtml = @"<DIV id='drag_switch_{0}'>
						<DIV class='drag_editor' id='drag_editor_{0}' style='DISPLAY: none'>
							<DIV id='loadeditorid_{0}' style='WIDTH: 100px'><IMG src='/Modules/WebPart/loading.gif'><SPAN id='loadeditortext_{0}' style='COLOR: #333'></SPAN></DIV>
						</DIV>
						<DIV class='drag_content' style='background-color:{2}' id='drag_content_{0}'>
							<DIV id='loadcontentid_{0}' style='WIDTH: 100px'><IMG src='/Modules/WebPart/loading.gif'><SPAN id='loadcontenttext_{0}' style='COLOR: #333'></SPAN></DIV>
						</DIV>
						<SCRIPT>{1}</SCRIPT>
					</DIV>
				</DIV>";
			WebPart bl =WebPart.Find(blockId);
			blockHtmls +=string.Format(BlockBeginHtml,blockId,bl.ColorValue,bl.DefaultHeight);
            WebPartExt wb = new WebPartExt(bl, userId);
            blockHtmls += wb.GetHeadHtml();
			//是否延时加载
			if(bl.DelayLoadSecond.ToString().Trim()!="0")
			{
				blockHtmls +=string.Format(BlockEndHtml,blockId,"window.setTimeout(\"loadDragContent('"+blockId+"','"+bl.RepeatItemCount+"')\","+Convert.ToString(bl.DelayLoadSecond*1000)+");",bl.ContentColor);
			}
			else
			{
                blockHtmls += string.Format(BlockEndHtml, blockId, "loadDragContent('" + blockId + "','" + bl.RepeatItemCount + "');", bl.ContentColor);
			}
			return blockHtmls;
		}

		//获取单个块的html
		public static string GetOneBlockHtmls(string userId,string blockId,string blockType)
		{
			string blockHtmls = "";
            string BlockBeginHtml = "<DIV class=\"drag_div\" id=\"drag_{0}\" style=\"BORDER-COLOR: {1}; BACKGROUND: #fff;Height:{2}\">";
			string BlockEndHtml = @"<DIV id='drag_switch_{0}'>
						<DIV class='drag_editor' id='drag_editor_{0}' style='DISPLAY: none'>
							<DIV id='loadeditorid_{0}' style='WIDTH: 100px'><IMG src='/Modules/WebPart/loading.gif'><SPAN id='loadeditortext_{0}' style='COLOR: #333'></SPAN></DIV>
						</DIV>
						<DIV class='drag_content' id='drag_content_{0}' style='background-color:{2}'>
							<DIV id='loadcontentid_{0}' style='WIDTH: 100px'><IMG src='/Modules/WebPart/loading.gif'><SPAN id='loadcontenttext_{0}' style='COLOR: #333'></SPAN></DIV>
						</DIV>
						<SCRIPT>{1}</SCRIPT>
					</DIV>
				</DIV>";

			if(blockId!="")
			{
                WebPart bl = WebPart.Find(blockId);
                WebPartTemplate dr = WebPartTemplate.FindAllByProperties("UserId", userId, "IsDefault", "T", "BlockType", blockType)[0];
				string templateXml = dr.TemplateXml;
				DataCollection list = new DataCollection(templateXml);
				DataElement item = list.GetElement("Id",blockId);
				if(item!=null)
				{
					bl.BlockKey = item.GetAttr("BlockKey");
					bl.BlockImage = item.GetAttr("BlockImage");
					bl.BlockTitle = item.GetAttr("BlockTitle");
					bl.BlockType = item.GetAttr("BlockType");
					bl.ColorValue = item.GetAttr("ColorValue");
					bl.Color = item.GetAttr("Color");
					bl.RepeatItemCount = int.Parse(item.GetAttr("RepeatItemCount"));
					bl.RepeatItemLength = int.Parse(item.GetAttr("RepeatItemLength"));
					bl.DelayLoadSecond = int.Parse(item.GetAttr("DelayLoadSecond"));
				}
                blockHtmls += string.Format(BlockBeginHtml, blockId, bl.ColorValue,bl.DefaultHeight);
                WebPartExt wb = new WebPartExt(bl, userId);
                blockHtmls += wb.GetHeadHtml();
				//是否延时加载
				if(bl.DelayLoadSecond.ToString().Trim()!="0")
				{
					blockHtmls +=string.Format(BlockEndHtml,blockId,"window.setTimeout(\"loadDragContent('"+blockId+"','"+bl.RepeatItemCount+"')\","+Convert.ToString(bl.DelayLoadSecond*1000)+");",bl.ContentColor);
				}
				else
				{
					blockHtmls +=string.Format(BlockEndHtml,blockId,"loadDragContent('"+blockId+"','"+bl.RepeatItemCount+"');",bl.ContentColor);
				}
			}
			return blockHtmls;
		}

		//添加新模块了,保存主页个人模版
		public static void UpdateAfterAddNewOneBlock(string userId,string blockId,string blockType,string templateId,string isManage)
		{
            WebPartTemplate dr = GetWebPartTemplateRecord(userId, blockType, templateId, isManage);
			//DbRecord dr = new DbRecord(daAccess,"DoorTemplate","UserId",userId,"IsDefault","T","BlockType",blockType);
			string tempXml = dr.TemplateXml;
			DataCollection list = new DataCollection(tempXml);
            WebPart bl = WebPart.Find(blockId);
			DataElement item = list.NewElement();
			item.SetAttr("Id",bl.Id);
			item.SetAttr("BlockTitle",bl.BlockTitle);
			item.SetAttr("BlockKey",bl.BlockKey);
			item.SetAttr("BlockImage",bl.BlockImage);
			item.SetAttr("RepeatItemCount",bl.RepeatItemCount.Value.ToString());
            item.SetAttr("RepeatItemLength", bl.RepeatItemLength.Value.ToString());
			item.SetAttr("Color",bl.Color);
			item.SetAttr("ColorValue",bl.ColorValue);
            item.SetAttr("DelayLoadSecond", bl.DelayLoadSecond.Value.ToString());
			dr.TemplateXml = list.ToString();
			dr.TemplateString = dr.TemplateString +","+blockId;
			dr.Update();
		}

		//删除模版中的块
		public static void DeleteBlockFromTemplate(string userId,string blockId,string blockType,string templateId,string isManage)
		{
            WebPartTemplate dr = GetWebPartTemplateRecord(userId, blockType, templateId, isManage);
			string tempXml = dr.TemplateXml;
			DataCollection list = new DataCollection(tempXml);
			DataElement item = list.GetElement("Id",blockId);
			list.Remove(item);
			dr.TemplateXml = list.ToString();
			dr.TemplateString = dr.TemplateString.IndexOf(","+blockId)>=0?dr.TemplateString.Replace(","+blockId,""):dr.TemplateString.Replace(blockId+",","");
			if(dr.TemplateString.IndexOf(blockId)>=0)
				dr.TemplateString = dr.TemplateString.Replace(blockId,"");
			dr.Update();
		}

		//设置全局色
		public static void SetGlobalColor(string userId,string color,string colorValue,string blockType,string templateId,string isManage)
		{
			WebPartTemplate dr = GetWebPartTemplateRecord(userId,blockType,templateId,isManage);
			string tempXml = dr.TemplateXml;
			DataCollection list = new DataCollection(tempXml);
			foreach(DataElement item in list.GetElements(""))
			{
				item.SetAttr("Color",color);
				item.SetAttr("ColorValue",colorValue);
			}
			dr.TemplateXml = list.ToString();
			dr.GlobalColor = color;
			dr.GlobalColorValue = colorValue;
			dr.Update();
		}

        private static WebPartTemplate GetWebPartTemplateRecord(string userId, string blockType, string templateId, string isManage)
        {
            return GetWebPartTemplateRecord(userId,"",blockType,templateId,isManage);
        }
		private static WebPartTemplate GetWebPartTemplateRecord(string userId,string userName,string blockType,string templateId,string isManage)
		{
            WebPartTemplate wp = null;

            if (!String.IsNullOrEmpty(blockType) && blockType!="Portal")
            {
                wp = WebPartTemplate.FindFirstByProperties("BlockType", blockType);
            }
            else
            {
                if (userId == "46c5f4df-f6d1-4b36-96ac-d39d3dd65a5d" || isManage == "T")
                {
                    SimpleQuery query = new SimpleQuery(typeof(WebPartTemplate),"FROM WebPartTemplate WHERE Type IS NULL AND UserId IS NULL AND BlockType='Portal'");
                    wp = ((WebPartTemplate[])WebPartTemplate.ExecuteQuery(query))[0];
                    //wp = WebPartTemplate.FindAllByProperties("Type", "", "UserId", "", "BlockType", "Portal")[0];
                }
                else
                {
                    if (WebPartTemplate.Exists(" UserId=? and IsDefault='T' and BlockType=? and Type='User'", userId,"Portal") && isManage != "T")
                    {
                        wp = WebPartTemplate.FindAllByProperties("UserId", userId, "IsDefault", "T", "BlockType", "Portal")[0];
                    }
                    else
                    {
                        SimpleQuery query = new SimpleQuery(typeof(WebPartTemplate), "FROM WebPartTemplate WHERE Type IS NULL AND UserId IS NULL AND BlockType='Portal'");
                        WebPartTemplate wpT = ((WebPartTemplate[])WebPartTemplate.ExecuteQuery(query))[0];
                        wp = new WebPartTemplate();
                        wp.BaseTemplateId = wpT.Id;
                        wp.Type = "User";
                        wp.UserId = userId;
                        wp.UserName = userName;
                        wp.IsDefault = "T";
                        wp.TemplateColWidth = wpT.TemplateColWidth;
                        wp.TemplateString = wpT.TemplateString;
                        wp.TemplateXml = wpT.TemplateXml;
                        wp.GlobalColor = wpT.GlobalColor;
                        wp.GlobalColorValue = wpT.GlobalColorValue;
                        wp.BlockType = wpT.BlockType;
                        wp.Save();
                    }
                }
                //wp = WebPartTemplate.FindFirst();
            }

            if (wp != null)
            {
                return wp;
            }

            if (!String.IsNullOrEmpty(templateId))
            {
                if (WebPartTemplate.Exists(" UserId=? and IsDefault='T' and BlockType=? and BaseTemplateId = ?", userId, templateId) && isManage != "T")
                {
                    wp = WebPartTemplate.FindAllByProperties("UserId", userId, "IsDefault", "T", "BlockType", blockType, "BaseTemplateId", templateId)[0];
                }
                else
                {
                    WebPartBaseTemplate wpT = WebPartBaseTemplate.Find(templateId);
                    wp = new WebPartTemplate();
                    wp.BaseTemplateId = wpT.Id;
                    wp.Type = "User";
                    wp.UserId = userId;
                    wp.UserName = "";
                    wp.IsDefault = "T";
                    wp.TemplateColWidth = wpT.TemplateColWidth;
                    wp.TemplateString = wpT.TemplateString;
                    wp.TemplateXml = wpT.TemplateXml;
                    wp.GlobalColor = wpT.GlobalColor;
                    wp.GlobalColorValue = wpT.GlobalColorValue;
                    wp.BlockType = wpT.BlockType;
                    wp.Save();
                }
            }
            else
            {
                wp = WebPartTemplate.FindFirstByProperties("UserId", userId, "IsDefault", "T", "BlockType", blockType);
            }

            return wp;
		}

    }
}
