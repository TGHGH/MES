using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Windows.Forms;
using System.Data;

namespace Module.BasicClassLib
{
    public static class BasicClass
    {
        public static string sModulePattern = "^ZX\\d{14}$";

        /// <summary>
        /// 判断字符串是否为数字类型
        /// </summary>
        /// <param name="sStringValue"></param>
        /// <returns></returns>
        public static bool isNumeric(string sStringValue)
        {
            string[] strArray = sStringValue.Split('|');

            for (int i = 0; i < strArray.Count(); i++)
            {
                string sTemp = strArray[i];

                System.Text.RegularExpressions.Regex reg1
                = new System.Text.RegularExpressions.Regex(@"^[-]?\d+[.]?\d*$");

                if (!reg1.IsMatch(sTemp))
                {
                    return false;
                }

            }

            return true;
        }


        public static string getTime()
        {
            //如果当前时间大于7：30，小于19：30，则为白班，否则为晚班
            if (DateTime.Compare(Convert.ToDateTime(DateTime.Now.ToString("t")), Convert.ToDateTime("7:30")) >= 0&&
                DateTime.Compare(Convert.ToDateTime(DateTime.Now.ToString("t")), Convert.ToDateTime("19:30")) < 0)
            {
                return "DAY";
            }
            else if (DateTime.Compare(Convert.ToDateTime(DateTime.Now.ToString("t")), Convert.ToDateTime("19:30")) >= 0&&
                DateTime.Compare(Convert.ToDateTime(DateTime.Now.ToString("t")), Convert.ToDateTime("23:59")) < 0)
            {
                return "NIGHT";
            }
            else
            {
                return "DAWN";
            }
        }

        /// <summary>
        /// 清空所有文本框的内容
        /// </summary>
        public static void resetControlState(GroupBox groupBox)
        {
            foreach (Control item in groupBox.Controls)
            {
                if (item is TextBox)
                {
                    if (!((TextBox)item).ReadOnly)
                    {
                        ((TextBox)item).Text = "";
                    }

                }
                else if (item is ComboBox)
                {
                    ((ComboBox)item).SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// 把某一个控件的所有子控件(TextBox ComboBox)的回车设成Tab
        /// </summary>
        /// <param name="groupControl">容器控件</param>
        public static void EnterToTab(Control groupControl)
        {
            foreach (Control control in groupControl.Controls)
            {
                if (control is TextBox || control is ComboBox)
                    control.KeyPress += new KeyPressEventHandler(control_KeyPress);
            }
        }
        /// <summary>
        /// 注册控件的KeyPress事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void control_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{Tab}");
                e.Handled = false;
            }
        }


        public static ComoboxItemName comoboxItemName = new ComoboxItemName();
    }
}
