namespace 支票列印工具
{
    partial class CheckTicket
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery1 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckTicket));
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.xr04 = new DevExpress.XtraReports.UI.XRLabel();
            this.xr00 = new DevExpress.XtraReports.UI.XRLabel();
            this.xr01 = new DevExpress.XtraReports.UI.XRLabel();
            this.xr03 = new DevExpress.XtraReports.UI.XRLabel();
            this.xr06 = new DevExpress.XtraReports.UI.XRLabel();
            this.xr05 = new DevExpress.XtraReports.UI.XRLabel();
            this.xr07 = new DevExpress.XtraReports.UI.XRLabel();
            this.xr02 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.Dpi = 254F;
            this.topMarginBand1.HeightF = 20F;
            this.topMarginBand1.Name = "topMarginBand1";
            this.topMarginBand1.Visible = false;
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.Dpi = 254F;
            this.bottomMarginBand1.HeightF = 20F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            this.bottomMarginBand1.Visible = false;
            // 
            // Detail
            // 
            this.Detail.Dpi = 254F;
            this.Detail.Font = new System.Drawing.Font("細明體", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.StylePriority.UseFont = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "127.0.0.1_WishTech_Connection";
            this.sqlDataSource1.Name = "sqlDataSource1";
            customSqlQuery1.Name = "Query";
            customSqlQuery1.Sql = "SELECT TA001,ZA003 MA003,TA003,TA004,TA005  \r\nFROM NOTTA  \r\nLEFT JOIN PURZA AS P " +
    "ON TA009=P.ZA001 AND P.ZA002=\'0001\'\r\nWHERE TA008=\'1\'\r\norder by TA001 \r\n";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            customSqlQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
            this.DetailReport.Dpi = 254F;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xr04,
            this.xr00,
            this.xr01,
            this.xr03,
            this.xr06,
            this.xr05,
            this.xr07,
            this.xr02});
            this.Detail1.Dpi = 254F;
            this.Detail1.HeightF = 2280F;
            this.Detail1.Name = "Detail1";
            // 
            // xr04
            // 
            this.xr04.Angle = 270F;
            this.xr04.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.TA005")});
            this.xr04.Dpi = 254F;
            this.xr04.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.xr04.LocationFloat = new DevExpress.Utils.PointFloat(657F, 1761.986F);
            this.xr04.Name = "xr04";
            this.xr04.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xr04.Scripts.OnBeforePrint = "xr04_BeforePrint";
            this.xr04.SizeF = new System.Drawing.SizeF(40F, 280F);
            this.xr04.StylePriority.UseFont = false;
            this.xr04.TextTrimming = System.Drawing.StringTrimming.None;
            // 
            // xr00
            // 
            this.xr00.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.MA003")});
            this.xr00.Dpi = 254F;
            this.xr00.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.xr00.LocationFloat = new DevExpress.Utils.PointFloat(233F, 256.0139F);
            this.xr00.Name = "xr00";
            this.xr00.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xr00.Scripts.OnBeforePrint = "xr00_BeforePrint";
            this.xr00.SizeF = new System.Drawing.SizeF(516.4167F, 55F);
            this.xr00.StylePriority.UseFont = false;
            this.xr00.StylePriority.UseTextAlignment = false;
            this.xr00.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xr01
            // 
            this.xr01.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.TA004")});
            this.xr01.Dpi = 254F;
            this.xr01.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.xr01.LocationFloat = new DevExpress.Utils.PointFloat(233F, 311.0139F);
            this.xr01.Name = "xr01";
            this.xr01.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xr01.Scripts.OnBeforePrint = "xr01_BeforePrint";
            this.xr01.SizeF = new System.Drawing.SizeF(516.4167F, 55F);
            this.xr01.StylePriority.UseFont = false;
            this.xr01.StylePriority.UseTextAlignment = false;
            this.xr01.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xr03
            // 
            this.xr03.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.TA004")});
            this.xr03.Dpi = 254F;
            this.xr03.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.xr03.LocationFloat = new DevExpress.Utils.PointFloat(233F, 421.0693F);
            this.xr03.Name = "xr03";
            this.xr03.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xr03.Scripts.OnBeforePrint = "xr03_BeforePrint";
            this.xr03.SizeF = new System.Drawing.SizeF(516.4167F, 55F);
            this.xr03.StylePriority.UseFont = false;
            this.xr03.StylePriority.UseTextAlignment = false;
            this.xr03.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xr06
            // 
            this.xr06.Angle = 270F;
            this.xr06.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.TA003")});
            this.xr06.Dpi = 254F;
            this.xr06.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.xr06.LocationFloat = new DevExpress.Utils.PointFloat(493F, 995.9861F);
            this.xr06.Name = "xr06";
            this.xr06.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xr06.Scripts.OnBeforePrint = "xr06_BeforePrint";
            this.xr06.SizeF = new System.Drawing.SizeF(80F, 1070F);
            this.xr06.StylePriority.UseFont = false;
            this.xr06.StylePriority.UseTextAlignment = false;
            this.xr06.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xr05
            // 
            this.xr05.Angle = 270F;
            this.xr05.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.MA003")});
            this.xr05.Dpi = 254F;
            this.xr05.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.xr05.LocationFloat = new DevExpress.Utils.PointFloat(577F, 995.9861F);
            this.xr05.Name = "xr05";
            this.xr05.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xr05.SizeF = new System.Drawing.SizeF(80F, 1070F);
            this.xr05.StylePriority.UseFont = false;
            this.xr05.StylePriority.UseTextAlignment = false;
            this.xr05.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xr07
            // 
            this.xr07.Angle = 270F;
            this.xr07.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.TA003", "{0:n0}")});
            this.xr07.Dpi = 254F;
            this.xr07.Font = new System.Drawing.Font("微軟正黑體", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.xr07.LocationFloat = new DevExpress.Utils.PointFloat(403F, 995.9861F);
            this.xr07.Name = "xr07";
            this.xr07.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xr07.SizeF = new System.Drawing.SizeF(80F, 430F);
            this.xr07.StylePriority.UseFont = false;
            this.xr07.StylePriority.UseTextAlignment = false;
            this.xr07.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xr02
            // 
            this.xr02.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Query.TA003", "{0:n0}")});
            this.xr02.Dpi = 254F;
            this.xr02.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.xr02.LocationFloat = new DevExpress.Utils.PointFloat(233F, 366.0417F);
            this.xr02.Name = "xr02";
            this.xr02.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xr02.Scripts.OnBeforePrint = "xr02_BeforePrint";
            this.xr02.SizeF = new System.Drawing.SizeF(516.4167F, 55F);
            this.xr02.StylePriority.UseFont = false;
            this.xr02.StylePriority.UseTextAlignment = false;
            this.xr02.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xr02.TextTrimming = System.Drawing.StringTrimming.None;
            // 
            // CheckTicket
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.topMarginBand1,
            this.bottomMarginBand1,
            this.DetailReport});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "Query";
            this.DataSource = this.sqlDataSource1;
            this.Dpi = 254F;
            this.FilterString = "[TA005] In (?TA005)";
            this.Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20);
            this.PageHeight = 2280;
            this.PageWidth = 831;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.ReportPrintOptions.DetailCountAtDesignTime = 3;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.ScriptsSource = resources.GetString("$this.ScriptsSource");
            this.ShowPrintMarginsWarning = false;
            this.SnapGridSize = 31.75F;
            this.Version = "15.2";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.CheckTicket_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.XRLabel xr04;
        private DevExpress.XtraReports.UI.XRLabel xr00;
        private DevExpress.XtraReports.UI.XRLabel xr01;
        private DevExpress.XtraReports.UI.XRLabel xr03;
        private DevExpress.XtraReports.UI.XRLabel xr06;
        private DevExpress.XtraReports.UI.XRLabel xr05;
        private DevExpress.XtraReports.UI.XRLabel xr07;
        private DevExpress.XtraReports.UI.XRLabel xr02;
    }
}
