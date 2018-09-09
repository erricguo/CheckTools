using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.Utils;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SharpUpdate;
using System.Drawing;
using System.Reflection;

namespace 支票列印工具
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm,ISharpUpdatable
    {
        //public static string MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //public static string HRMDIR = MyDocumentsPath + "\\HRMSystem";
        const string NodeSoftWare = "Software";
        const string NodeCK = "CheckTools";
        const string NodePath = @"HKEY_CURRENT_USER\Software\" + NodeCK;
        string FScript = "";
        DataTable detail = null;

        #region ISharpUpdatable 成員
        public string ApplicationName
        {
            get { return "WeekReports"; }
        }

        public string ApplicationID
        {
            get { return "WeekReports"; }
        }

        public Assembly ApplicationAssembly
        {

            get { return Assembly.GetExecutingAssembly(); }
        }

        public Icon ApplicationIcon
        {
            get { return this.Icon; }
        }

        public Uri UpdateXmlLocation
        {
            get
            {
                //IConfigSource source = new IniConfigSource(fc.INIPath);
                // string mPath = source.Configs["SYSTEM"].Get("UPDATEPATH", "http://kupoautos.com/DSC/WeekReports/update.xml");
                string mPath = "http://kupoautos.com/DSC/CheckPrint/update.xml";
                return new Uri(mPath);
            }
        }

        public Form Context
        {
            get { return this; }
        }

        #endregion
        private SharpUpdater update;
        public Form1()
        {
            
            InitializeComponent();
            update = new SharpUpdater(this);
            update.DoUpdate(false);
            DevExpress.UserSkins.BonusSkins.Register();
            string mID = "";
            string mPW = "";
            string mIP = "";
            string mDB = "";

            //打開 子機碼 路徑。
            RegistryKey Reg = Registry.CurrentUser.OpenSubKey(NodeSoftWare, true);
            ////檢查mDB子機碼是否存在，檢查資料夾是否存在。
            if (Reg.GetSubKeyNames().Contains(NodeCK))
            {
                tbID.Text = Registry.GetValue(NodePath, "ID", "").ToString();  //sa
                tbPW.Text = Registry.GetValue(NodePath, "PW", "").ToString();  //dsc!23
                tbIP.Text = Registry.GetValue(NodePath, "IP", "").ToString();  //192.168.1.100
                tbDB.Text = Registry.GetValue(NodePath, "DB", "").ToString();  //
            }
            Reg.Close();

            dtB.Properties.Mask.MaskType = MaskType.DateTime;
            dtB.Properties.Mask.EditMask = "yyyy/MM/dd";
            dtB.Properties.DisplayFormat.FormatType = FormatType.DateTime;
            dtB.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";

            dtE.Properties.Mask.MaskType = MaskType.DateTime;
            dtE.Properties.Mask.EditMask = "yyyy/MM/dd";
            dtE.Properties.DisplayFormat.FormatType = FormatType.DateTime;
            dtE.Properties.DisplayFormat.FormatString = "yyyy/MM/dd";

            btnClear.Enabled = false;
            btnNew.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }


        private void Et_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            try
            {
                string date = e.Value.ToString();
                string pattern = "yyyyMMdd"; // define a date pattern according to your requirements
                DateTime parsedDate;
                bool parsedSuccessfully = DateTime.TryParseExact(date, pattern, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
                if (parsedSuccessfully)
                    e.DisplayText = parsedDate.Date.ToString("yyyy/MM/dd");
            }
            catch (System.Exception ex)
            {
            	
            }

        }

        public string makeConnectString()
        {
            string mID = "";
            string mPW = "";
            string mIP = "";
            string mDB = "";

            //打開 子機碼 路徑。
            RegistryKey Reg = Registry.CurrentUser.OpenSubKey(NodeSoftWare, true);
            ////檢查mDB子機碼是否存在，檢查資料夾是否存在。
            if (Reg.GetSubKeyNames().Contains(NodeCK))
            {
                mID = Registry.GetValue(NodePath, "ID", "").ToString();
                mPW = Registry.GetValue(NodePath, "PW", "").ToString();
                mIP = Registry.GetValue(NodePath, "IP", "").ToString();
                mDB = Registry.GetValue(NodePath, "DB", "").ToString();
            }
            Reg.Close();

            string ConnStr = $"Data Source = {mIP} ;Initial catalog = {mDB} ;" +
                             $"User id = {mID} ; Password = {mPW}";

            return ConnStr;
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            string ConnStr;
            ConnStr = $"Data Source = {tbIP.Text} ;Initial catalog = {tbDB.Text} ;" +
                      $"User id = {tbID.Text} ; Password = {tbPW.Text}";
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(ConnStr);
            try
            {
                
                conn.Open();
                MsgForm msg = new MsgForm("連線成功!","訊息",1);
                //MsgForm msg = new MsgForm("C# 6.0 不是 C# 程式設計中的激進革命。不像引入泛型的 C# 2.0、 C# 3.0 和其開創性的方式到程式集合與 LlNQ 或簡化的非同步程式設計模式 5.0 C# 中，C# 6.0 不會改變發展。這就是說，C# 6.0 將改變在特定的場景，特點是效率高太多，你可能會忘了還有另一種方式，他們的代碼編寫 C# 代碼的方式。它介紹了新的語法快捷方式裝瘋賣傻，減少儀式和最終使編寫 C# 代碼的精簡。在這篇文章!", "訊息", 1);
                msg.ShowDialog();

                //打開 子機碼 路徑。
                RegistryKey Reg = Registry.CurrentUser.OpenSubKey(NodeSoftWare, true);
                ////檢查子機碼是否存在，檢查資料夾是否存在。
                if (!Reg.GetSubKeyNames().Contains(NodeCK))
                {
                    //建立子機碼，建立資料夾。
                    Reg.CreateSubKey(NodeCK);
                }

                //寫入資料 Name,Value,"寫入類型"
                RegKey(NodePath, lbID.Tag.ToString(), tbID.Text, RegistryValueKind.String);
                RegKey(NodePath, lbPW.Tag.ToString(), tbPW.Text, RegistryValueKind.String);
                RegKey(NodePath, lbIP.Tag.ToString(), tbIP.Text, RegistryValueKind.String);
                RegKey(NodePath, lbDB.Tag.ToString(), tbDB.Text, RegistryValueKind.String);
                Reg.Close();
            }
            catch
            {
                
            }
            finally
            {
                
            }
        }

        private void RegKey(string xPAth, string xKey, string xValue, RegistryValueKind xKind)
        {
            Registry.SetValue(xPAth, xKey, xValue, xKind);
        }

        private void F2Control(object sender, EventArgs e)
        {
            
        }


        private void btnTicket_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string mSQL = " SELECT TA001,MA003,TA003,TA004,TA005 " +
                          " FROM NOTTA " +
                          " LEFT JOIN PURMA AS P ON TA009=P.MA001  " +
                          " WHERE TA008='1' order by TA001 ";
            string[] mSchema = new string[] { "TA001","MA003","TA003","TA004","TA005" };
            string[] mSchemaName = new string[] { "票號","公司","金額","開票日","到期日" };
            F2 f2 = new F2(mSQL, mSchema, mSchemaName);
            f2.ShowDialog();
            btnTicketB.Text = f2.FResult;
        }

        private void SetGridView2Filter()
        {
            if (gridView2.RowCount > 0)
            {
                if (gridView1.RowCount > 0)
                {
                    var mMA001 = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MA001").ToString();
                    ((DataTable)gridControl2.DataSource).DefaultView.RowFilter = string.Format("ZA001 = '{0}'", mMA001);
                }
                else
                {
                    ((DataTable)gridControl2.DataSource).DefaultView.RowFilter = "1=2";
                }
            }
        }
        private void barbtnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CheckTicket report = new CheckTicket();
            report.LoadLayout("CheckTicket.repx");
            report.DataMember = string.Empty;
            report.DataSource = gridControl1.DataSource;

            Parameter param1 = new Parameter();
            param1.Name = "TA005";
            param1.Visible = false;
            param1.MultiValue = true;
            param1.Type = typeof(System.String);
            List<string> mList = new List<string>();

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                mList.Add(gridView1.GetRowCellValue(i, "TA005").ToString().Trim());
            }

            param1.Value = mList.ToArray();
            report.Parameters.Add(param1);
            report.PrintingSystem.ShowMarginsWarning = false;
            using (ReportPrintTool printTool = new ReportPrintTool(report))
            {
                printTool.ShowRibbonPreviewDialog();
            }
        }

        private void barbtnReportDesign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CheckTicket report = new CheckTicket();
            report.LoadLayout("CheckTicket.repx");
            report.DataMember = string.Empty;
            report.DataSource = gridControl1.DataSource;
            Parameter param1 = new Parameter();
            param1.Name = "TA005";
            param1.Visible = false;
            param1.MultiValue = true;
            param1.Type = typeof(System.String);

            List<string> mList = new List<string>();

            for (int i = 0; i < gridView1.RowCount; i++)
            {
                mList.Add(gridView1.GetRowCellValue(i, "TA005").ToString().Trim());
            }

            param1.Value = mList.ToArray();
            report.Parameters.Add(param1);
            report.PrintingSystem.ShowMarginsWarning = false;
            ReportDesignTool designTool = new ReportDesignTool(report);
            designTool.ShowRibbonDesigner();
        }

        private void barbtnExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "";
            sfd.Filter = "xls|*.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                gridControl1.ExportToXls(sfd.FileName);
            }
        }

        private void barbtnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void gridView1_MasterRowGetChildList(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs e)
        {
           /* GridView view = (GridView)sender;
            string contactID =(string)view.GetRowCellValue(e.RowHandle, "MA001");
            var query = from row in detail.AsEnumerable()
                        where (row.Field<string>("MA001") == contactID)
                        select row;

            e.ChildList = query.ToList();*/
        }

        private void gridView1_MasterRowEmpty(object sender, MasterRowEmptyEventArgs e)
        {
            /*if (e.RowHandle == 0)
                e.IsEmpty = false;*/
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SetGridView2Filter();
            tbTitle.Text = "";
        }

        private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //tbTitle.Text = gridView2.GetRowCellValue(e.FocusedRowHandle, "ZA003").ToString();
        }

        private void gridView2_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Clicks == 1)
            {
                if (gridControl2.DataSource != null)
                    tbTitle.Text = gridView2.GetRowCellValue(e.RowHandle, "ZA003").ToString();
            }
            else if (e.Clicks == 2)
            {
                int GV1RowHandle = gridView1.FocusedRowHandle;
                gridView1.SetRowCellValue(GV1RowHandle, "MA003", gridView2.GetRowCellValue(e.RowHandle, "ZA003").ToString());
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbTitle.Text = "";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (tbTitle.Text == "")
            {
                return;
            }
            else
            {
                int ColumnCount = gridView2.Columns.Count;
                gridView2.AddNewRow();
                //Get the handle of the new row  
                int GV1RowHandle = gridView1.FocusedRowHandle;
                var mZA001 = gridView1.GetRowCellValue(GV1RowHandle, "MA001").ToString();
                
                int newRowHandle = gridView2.FocusedRowHandle;
                object newRow = gridView2.GetRow(newRowHandle);
                if (ColumnCount > 0)
                {
                    var mZA002 = gridView2.RowCount.ToString().PadLeft(4, '0');
                    gridView2.SetRowCellValue(newRowHandle, "ZA001", mZA001);
                    gridView2.SetRowCellValue(newRowHandle, "ZA002", mZA002);
                    gridView2.SetRowCellValue(newRowHandle, "ZA003", tbTitle.Text);
                    gridView2.UpdateCurrentRow();
                    UpdateValue((DataTable)gridControl2.DataSource);
                    
                    gridView2.RefreshData();
                    tbTitle.Text = "";
                }                
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int ColumnCount = gridView2.Columns.Count;
            int RowHandle = gridView2.FocusedRowHandle;
            if (ColumnCount > 0)
            {
                gridView2.SetRowCellValue(RowHandle, "ZA003", tbTitle.Text);
                gridView2.UpdateCurrentRow();
                UpdateValue((DataTable)gridControl2.DataSource);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int ColumnCount = gridView2.Columns.Count;
            int RowHandle = gridView2.FocusedRowHandle;
            if (ColumnCount > 0)
            {
                gridView2.DeleteRow(RowHandle);
                gridView2.UpdateCurrentRow();
                UpdateValue((DataTable)gridControl2.DataSource);
                tbTitle.Text = "";
            }
        }
        private void UpdateValue(DataTable dt)
        {

            //splashScreenManager1.ShowWaitForm();
            string mSQL = " SELECT * FROM PURZA order by ZA002";
            var Conn = new SqlConnection(makeConnectString());
            SqlDataAdapter dataAdapter = new SqlDataAdapter(mSQL, Conn);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
            commandBuilder.ConflictOption = ConflictOption.OverwriteChanges;
            dataAdapter.InsertCommand = commandBuilder.GetInsertCommand().Clone();
            dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand().Clone();
            dataAdapter.DeleteCommand = commandBuilder.GetDeleteCommand().Clone();

            Conn.Open();
            SqlTransaction trans = Conn.BeginTransaction();
            try
            {
                if (dataAdapter.InsertCommand != null)
                    dataAdapter.InsertCommand.Transaction = trans;
                if (dataAdapter.UpdateCommand != null)
                    dataAdapter.UpdateCommand.Transaction = trans;
                if (dataAdapter.DeleteCommand != null)
                    dataAdapter.DeleteCommand.Transaction = trans;

                var dt2 = dt.GetChanges();
                if (dt2.Rows.Count > 0)
                {
                    dataAdapter.Update(dt2);                    
                    trans.Commit();
                    dt.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                //ErrorLog(ex.Message);
                trans.Rollback();
            }
            finally
            {
                Conn.Close();
                
                //splashScreenManager1.CloseWaitForm();
            }
        }

        private void barbtnSignBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SignBack report = new SignBack();
            //report.LoadLayout("SignBack.repx");
            report.DataSource =  gridControl1.DataSource;
            using (ReportPrintTool printTool = new ReportPrintTool(report))
            {
                printTool.ShowRibbonPreviewDialog();
            }
        }

        private void btnSearchCheck_Click(object sender, EventArgs e)
        {
            string mTicket = "";
            string mDate = "";
            string mCurrency = "";
            if (btnTicketB.Text.Trim() == "" && btnTicketE.Text.Trim() == "")
            {
                //nothing
            }
            else if ((btnTicketB.Text.Trim() != "" && btnTicketE.Text.Trim() == "")/* ||
                     (btnTicketB.Text.Trim() == btnTicketE.Text.Trim())*/)
            {
                mTicket = " and (TA001 >= N'" + btnTicketB.Text.Trim() + "')";
            }
            else if (btnTicketB.Text.Trim() == "" && btnTicketE.Text.Trim() != "")
            {
                mTicket = " and (TA001 <= N'" + btnTicketE.Text.Trim() + "' OR TA001 IS NULL) ";
            }
            else if ((btnTicketB.Text.Trim() != "" && btnTicketE.Text.Trim() != "") /*&&
                     (btnTicketB.Text.Trim() != btnTicketE.Text.Trim())*/)
            {
                mTicket = " and (TA001 Between N'" + btnTicketB.Text.Trim() + "' and N'" + btnTicketE.Text.Trim() + "')";
            }

            if (dtB.Text.Trim() == "" && dtE.Text.Trim() == "")
            {
                //nothing
            }
            else if ((dtB.Text.Trim() != "" && dtE.Text.Trim() == "") /*||
                     (dtB.Text.Trim() == dtE.Text.Trim())*/)
            {
                mDate = " and (TA004 >= N'" + dtB.Text.Replace("/", "") + "')";
            }
            else if (dtB.Text.Trim() == "" && dtE.Text.Trim() != "")
            {
                mDate = " and (TA004 <= N'" + dtE.Text.Replace("/", "") + "' OR TA004 IS NULL) ";
            }
            else if ((dtB.Text.Trim() != "" && dtE.Text.Trim() != "") /*&&
                     (dtB.Text.Trim() != dtE.Text.Trim())*/)
            {
                mDate = " and (TA004 Between N'" + dtB.Text.Replace("/", "") + "' and N'" + dtE.Text.Replace("/", "") + "')";
            }

            /*if (btnCurrency.Text.Trim() != "")
            {
                mCurrency = " and (TA002 = '" + btnCurrency.Text.Trim() + "')";
            }*/


            /* string mSQL = " SELECT TA009+' '+TA010 AS TA009T, TB006+'-'+TB007 AS TB006T, "+  
                           " N.MA002 AS NOTMA002, N.MA004 AS NOTMA004, P.MA003 AS PURMA003, MF002 AS CMSMF002 "+
                           " FROM NOTTA AS NOTTA "+
                           " LEFT JOIN NOTTB ON TA001=TB001  "+
                           " LEFT JOIN NOTMA AS N ON TA006=N.MA001  "+
                           " LEFT JOIN PURMA AS P ON TA009=P.MA001  "+
                           " LEFT JOIN CMSMF ON TA002=MF001 "+
                           " WHERE (TA008='1' "+ 
                             mTicket +
                             mDate +
                             mCurrency +
                           " ) ORDER BY TA001, TA004";*/
            string mSQL = " SELECT MUITY, TA001 ,P.ZA003 AS MA003,TA003,TA004,TA005,P.ZA001 AS MA001 " +
                          " FROM NOTTA AS NOTTA " +
                          " LEFT JOIN NOTTB ON TA001=TB001  " +
                          " LEFT JOIN NOTMA AS N ON TA006=N.MA001  " +
                          //" LEFT JOIN PURMA AS P ON TA009=P.MA001  " +                         
                          " LEFT JOIN PURZA AS P ON TA009=P.ZA001 AND P.ZA002='0001'  " +
                          " LEFT JOIN CMSMF ON TA002=MF001 " +
                          " LEFT JOIN (SELECT ZA001,CASE WHEN COUNT(ZA002) > 1 THEN 'Y' ELSE 'N' END MUITY  FROM PURZA GROUP BY ZA001) A ON A.ZA001=TA009 " +
                          " WHERE (TA008='1' " +
                            mTicket +
                            mDate +
                            mCurrency +
                          " ) ORDER BY TA001, TA004";

            /*
             Detail
             */
            string mSQL2 = " SELECT * FROM PURZA order by ZA002";

            var Conn2 = new SqlConnection(makeConnectString());
            var da2 = new SqlDataAdapter(mSQL2, Conn2);
            DataTable dd2 = new DataTable();
            da2.Fill(dd2);
            gridControl2.DataSource = dd2;

            gridView2.Columns["ZA001"].Visible = false;
            gridView2.Columns["ZA002"].Caption = "序號";
            gridView2.Columns["ZA003"].Caption = "抬頭";
            gridView2.Columns["ZA002"].Width = 80;
            gridView2.Columns["ZA003"].Width = 450;
            gridView2.Columns["ZA002"].OptionsColumn.ReadOnly = true;
            gridView2.Columns["ZA003"].OptionsColumn.ReadOnly = true;
            gridView2.Columns["ZA002"].OptionsColumn.AllowEdit = false;
            gridView2.Columns["ZA003"].OptionsColumn.AllowEdit = false;

            if (dd2.Rows.Count > 0)
            {
                btnNew.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnClear.Enabled = true;
            }

            //主Table
            var Conn = new SqlConnection(makeConnectString());
            var da1 = new SqlDataAdapter(mSQL, Conn);
            DataTable dd = new DataTable("Main");
            da1.Fill(dd);

            gridControl1.DataSource = dd;

            RepositoryItemTextEdit et = new RepositoryItemTextEdit();
            et.CustomDisplayText += Et_CustomDisplayText;
            et.Mask.MaskType = MaskType.Custom;
            gridControl1.RepositoryItems.Add(et);

            RepositoryItemCheckEdit chk = new RepositoryItemCheckEdit();
            chk.ValueChecked = "Y";
            chk.ValueUnchecked = "N";
            chk.AllowGrayed = false;
            chk.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.UserDefined;
            chk.PictureChecked = Properties.Resources.star1;
            chk.PictureUnchecked = Properties.Resources.star0;
            gridControl1.RepositoryItems.Add(chk);

            RepositoryItemComboBox et1 = new RepositoryItemComboBox();
            gridControl1.RepositoryItems.Add(et1);
            et1.Items.AddRange(new List<string>() { "A", "B" });

            gridView1.Columns["TA003"].DisplayFormat.FormatType = FormatType.Numeric;
            gridView1.Columns["TA003"].DisplayFormat.FormatString = "n0";

            gridView1.Columns["MUITY"].ColumnEdit = chk;
            gridView1.Columns["TA004"].ColumnEdit = et;
            gridView1.Columns["TA005"].ColumnEdit = et;
            gridView1.Columns["MUITY"].Caption = "多抬頭";
            gridView1.Columns["TA001"].Caption = "票號";
            gridView1.Columns["MA003"].Caption = "公司名稱";
            gridView1.Columns["TA003"].Caption = "金額";
            gridView1.Columns["TA004"].Caption = "開票日";
            gridView1.Columns["TA005"].Caption = "到期日";
            gridView1.Columns["MA001"].Caption = "廠商代號";
            gridView1.Columns["TA005"].Visible = false;
            gridView1.Columns["MA001"].Visible = false;
            gridView1.Columns["MUITY"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["TA001"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["MA003"].OptionsColumn.ReadOnly = false;
            gridView1.Columns["TA003"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["TA004"].OptionsColumn.ReadOnly = true;
            gridView1.Columns["TA005"].OptionsColumn.ReadOnly = true;
            //gridView1.Columns["TA001"].OptionsColumn.AllowEdit = false;
            gridView1.Columns["TA003"].OptionsColumn.AllowEdit = false;
            gridView1.Columns["TA004"].OptionsColumn.AllowEdit = false;
            gridView1.Columns["TA005"].OptionsColumn.AllowEdit = false;

            gridView1.Columns["TA001"].Width = 150;
            gridView1.Columns["MA003"].Width = 400;
            gridView1.Columns["TA003"].Width = 180;
            gridView1.Columns["TA004"].Width = 120;
            gridView1.Columns["TA005"].Width = 120;

            gridView1.Columns["TA001"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridView1.Columns["TA004"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridView1.Columns["TA005"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;


            gridView1.Columns["TA003"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridView1.Columns["TA003"].SummaryItem.DisplayFormat = "合計: {0:n0}";
            gridView1.Columns["TA004"].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
            gridView1.Columns["TA004"].SummaryItem.DisplayFormat = "筆數: {0:n0}";


            SetGridView2Filter();
            tbTitle.Text = "";
        }
    }
}
