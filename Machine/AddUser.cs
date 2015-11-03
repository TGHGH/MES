using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using GeneralWcfService;
using WcfRequestNS;
using System.IO;
using System.Xml.Linq;
using System.Net;
using Module.BasicClassLib;
using WcfRequestNS.ServiceReference;

namespace SysConfig
{
    public partial class AddUser : Form
    {
        string _user = string.Empty;//当前用户

        public AddUser()
        {
            InitializeComponent();
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            #region 获取当前主机的登录用户
            FuncTagInfo functionTag = null;
            functionTag = new FuncTagInfo();
            functionTag.FuncName = "getCurrentUser";
            functionTag.ComputerName = Dns.GetHostName(); ;

            WcfRequests.SendGeneralRequest(onCurrentUserReceive, functionTag);
            #endregion

            #region 初始化comobox下拉项
            List<string> inParameters = new List<string>();
            inParameters.Add(BasicClass.comoboxItemName.Workshop);
            inParameters.Add(BasicClass.comoboxItemName.Dept);

            functionTag = new FuncTagInfo();
            functionTag.FuncName = "GetComoboxItems";
            functionTag.InputParams = inParameters.ToArray();

            WcfRequests.SendGeneralRequest(onReceiveComoboxItems, functionTag);
            #endregion
            
        }

        #region wcf received
        /// <summary>
        /// 初始化下拉项
        /// </summary>
        /// <param name="receiveData"></param>
        private void onReceiveComoboxItems(CallBackData receiveData)
        {
            //注意：车间

            TextReader oTR = new StringReader(receiveData.Result);
            XElement oResult = XElement.Load(oTR);
            oTR.Close();

            IEnumerable<XElement> cReports =
                (from xNode in oResult.Descendants("Comoboxes")
                 select xNode);

            foreach (XElement oModuleNode in cReports)
            {
                string ComoboxName = ((XElement)oModuleNode).Attribute("ComoboxName").Value.ToString();
                string displayName = ((XElement)oModuleNode).Attribute("displayName").Value.ToString();
                string sourceName = ((XElement)oModuleNode).Attribute("sourceName").Value.ToString();

                if (ComoboxName == BasicClass.comoboxItemName.Workshop)
                {
                    comboBox_workshop.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName=="dept")
                {
                    comboBox_dept.Items.Add(new ListItem(sourceName, displayName));
                }
            }
            
        }

        /// <summary>
        /// 获取到当前用户
        /// </summary>
        /// <param name="receiveData"></param>
        private void onCurrentUserReceive(CallBackData receiveData)
        {
            _user = receiveData.Result;
        }
        
        #endregion

        

        private void btn_query_Click(object sender, EventArgs e)
        {

        }

        private void button_del_Click(object sender, EventArgs e)
        {

        }

        private void button_reset_Click(object sender, EventArgs e)
        {

        }

        private void button_insert_Click(object sender, EventArgs e)
        {
            string workshopDisplayName = ((ListItem)comboBox_workshop.SelectedItem).Name;
            string workshopSourCode = ((ListItem)comboBox_workshop.SelectedItem).ID;

            MessageBox.Show(workshopDisplayName+" "+workshopSourCode);
        }
    }
}
