using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using Microsoft.Win32;
using DevExpress.Utils;
using DevExpress.XtraEditors.Repository;
using System.Globalization;

namespace 支票列印工具
{
    public partial class F2 : DevExpress.XtraEditors.XtraForm
    {
        const string NodeSoftWare = "Software";
        const string NodeCK = "CheckTools";
        const string NodePath = @"HKEY_CURRENT_USER\Software\" + NodeCK;
        string FSQL = "";
        string[] FSchema = null;
        string[] FSchemaName = null;
        string FWhere = "";
        public F2()
        {
            InitializeComponent();
        }

        public F2(string xSQL, string[] xSchema, string[] xSchemaName)
        {
            InitializeComponent();
            FSQL = xSQL;
            FSchema = xSchema;
            FSchemaName = xSchemaName;
            GOQuery(FSQL);
            cbSchema.Properties.Items.AddRange(xSchemaName);
            if (cbSchema.Properties.Items.Count > 0)
                cbSchema.SelectedIndex = 0;

        }

        public string FResult{get;set;}

        private void Et_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            string date = e.Value.ToString();
            string pattern = "yyyyMMdd"; // define a date pattern according to your requirements
            DateTime parsedDate;
            bool parsedSuccessfully = DateTime.TryParseExact(date, pattern, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
            if (parsedSuccessfully)
                e.DisplayText = parsedDate.Date.ToString("yyyy/MM/dd");
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

        private void btnNo_Click(object sender, EventArgs e)
        {

        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            //0. =
            //1. >
            //2. <
            //3. >=
            //4. <=
            //5. %LIKE
            //6. LIKE%
            //7. %LIKE%
            if (tbInput.Text.Trim() != "")
            {
                int idx = cbSymbol.SelectedIndex;
                int i = cbSchema.SelectedIndex;
                switch (idx)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        FWhere = " WHERE " + FSchema[i] + cbSymbol.Text + " '" + tbInput.Text + "' ";
                        break;
                    case 5:
                        FWhere = " WHERE " + FSchema[i] + " LIKE '%" + tbInput.Text + "' ";
                        break;
                    case 6:
                        FWhere = " WHERE " + FSchema[i] + " LIKE '" + tbInput.Text + "%' ";
                        break;
                    case 7:
                        FWhere = " WHERE " + FSchema[i] + " LIKE '%" + tbInput.Text + "%' ";
                        break;
                }
                FWhere += " AND ";
                string mSQL = FSQL.Replace("WHERE", FWhere);
                GOQuery(mSQL);
            }
            else
            {
                GOQuery(FSQL);
            }
        }
        private void GOQuery(string xSQL)
        {
            var Conn = new SqlConnection(makeConnectString());
            var da2 = new SqlDataAdapter(xSQL, Conn);
            DataTable dd = new DataTable();
            da2.Fill(dd);
            gridControl1.DataSource = dd;
            RepositoryItemTextEdit et = new RepositoryItemTextEdit();
            et.CustomDisplayText += Et_CustomDisplayText;
            gridControl1.RepositoryItems.Add(et);

            gridView1.Columns["TA003"].DisplayFormat.FormatType = FormatType.Numeric;
            gridView1.Columns["TA003"].DisplayFormat.FormatString = "n0";
            gridView1.Columns["TA004"].ColumnEdit = et;
            gridView1.Columns["TA005"].ColumnEdit = et;
            gridView1.Columns["TA001"].Caption = "票號";
            gridView1.Columns["MA003"].Caption = "公司名稱";
            gridView1.Columns["TA003"].Caption = "金額";
            gridView1.Columns["TA004"].Caption = "開票日";
            gridView1.Columns["TA005"].Caption = "到期日";

            gridView1.Columns["TA001"].Width = 150;
            gridView1.Columns["MA003"].Width = 200;
            gridView1.Columns["TA003"].Width = 120;
            gridView1.Columns["TA004"].Width = 120;
            gridView1.Columns["TA005"].Width = 120;

            gridView1.Columns["TA001"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridView1.Columns["TA004"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gridView1.Columns["TA005"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FResult = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "TA001").ToString();
        }
    }
}