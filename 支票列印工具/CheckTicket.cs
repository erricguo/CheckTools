using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace 支票列印工具
{
    public partial class CheckTicket : DevExpress.XtraReports.UI.XtraReport
    {
        public enum MoneyType
        {
            /// <summary>
            /// 表示新台幣。
            /// </summary>
            NTD,
            /// <summary>
            /// 表示人民幣。
            /// </summary>
            RMB
        }
        public CheckTicket()
        {
            InitializeComponent();
            //sqlDataSource1.Connection.ConnectionString = "XpoProvider=MSSqlServer;Data Source=127.0.0.1;Initial Catalog=02953_WishTech;Persist Security Info=True;User ID=sa;Password=123";
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
                       
        }


        public string FormatDateForDisplay(string text,int type)
        {
            string year = text.Substring(0, 4);
            int yeari = Int32.Parse(year) - 1911;            
            string month = text.Substring(4, 2);
            string day = text.Substring(6, 2);
            if (type == 0)
            { return $"{yeari}      {month}      {day}"; }
            else if (type == 1)
            { return $"{year}/{month}/{day}"; }
            else
                return text;
        }

        /// <summary>
        /// 將新台幣或人民幣轉換為金額大寫形式。
        /// </summary>
        /// <param name="d" />要轉換的數值</param>
        /// <returns>返回轉換的結果。</returns>
        public static string GetMoneyUpper(decimal d, MoneyType moneyType)
        {
            string o = d.ToString();
            string dq = "", dh = "";
            if (o.Contains("."))
            {
                dq = o.Split('.')[0];
                dh = o.Split('.')[1];
            }
            else
            {
                dq = o;
            }
            string ret = GetMoney(dq, true, "元", moneyType) + GetMoney(dh, false, "", moneyType);
            return ret + "整";
           /* if (ret.Contains("厘") || ret.Contains("分"))
                return cc.ToTraditional(ret);
            if (moneyType != MoneyType.NTD)
            {
                ret = ret.Replace("參", "叁");
            }
            return cc.ToTraditional(ret + "整");*/
        }
        public static string GetMoney(string number, bool left, string lastdw, MoneyType type) // 內部函數。
        {
            string[] NTD = new string[10] { "零", "壹", "貳", "參", "肆", "伍", "陸", "柒", "捌", "玖" };
            string[] DW = new string[8] { "厘", "分", "角", "", "拾", "佰", "仟", "萬" };
            int first = 4;
            string str = "";
            if (!left)
            {
                first = 1;
                if (number.Length == 1)
                {
                    number += "00";
                }
                else if (number.Length == 2)
                {
                    number += "0";
                }
                else number = number.Substring(0, 3);

            }
            else
            {
                if (number.Length >= 9)
                {
                    return GetMoney(number.Substring(0, number.Length - 8), true, "億", type) + GetMoney(number.Substring(number.Length - 8, 8), true, "元", type);
                }
                if (number.Length >= 5)
                {
                    return GetMoney(number.Substring(0, number.Length - 4), true, "萬", type) + GetMoney(number.Substring(number.Length - 4, 4), true, "元", type);
                }
            }
            bool has0 = false;
            for (int i = 0; i < number.Length; ++i)
            {
                int w = number.Length - i + first - 2;
                if (int.Parse(number[i].ToString()) == 0)
                {
                    has0 = true;
                    continue;
                }
                else
                {
                    if (has0)
                    {
                        if (type == MoneyType.RMB)
                            str += "零";
                        has0 = false;
                    }
                }
                str += NTD[int.Parse(number[i].ToString())];
                str += DW[w];
            }
            if (left)
                str += lastdw;
            return str;
        }
        private void xr00_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRLabel)sender).Text += " 台照";
        }
        private void xr01_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRLabel)sender).Text = FormatDateForDisplay(((XRLabel)sender).Text,1) + " 開票";
        }
        private void xr02_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRLabel)sender).Text += " 元　";
        }
        private void xr03_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRLabel)sender).Text = FormatDateForDisplay(((XRLabel)sender).Text, 1) + " 兌現";
        }
        private void xr04_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((XRLabel)sender).Text = FormatDateForDisplay(((XRLabel)sender).Text, 0);
        }
        private void xr06_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            decimal d = Decimal.Parse(((XRLabel)sender).Text);
            ((XRLabel)sender).Text = GetMoneyUpper(d, MoneyType.NTD);
        }

        private void CheckTicket_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            decimal d = 2;
            d = d + 2;
        }
    }
}
