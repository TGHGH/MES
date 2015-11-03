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

namespace Producing
{
    public partial class CellDistribute : Form
    {
        Timer changeColorTimer = new Timer();
        string _user = string.Empty;//当前用户

        public CellDistribute()
        {
            InitializeComponent();
        }

        private void CellDistribute_Load(object sender, EventArgs e)
        {
            #region 获取当前主机的登录用户
            FuncTagInfo functionTag = null;
            functionTag = new FuncTagInfo();
            functionTag.FuncName = "getCurrentUser";
            functionTag.ComputerName = Dns.GetHostName(); ;

            WcfRequests.SendGeneralRequest(onCurrentUserReceive, functionTag);
            #endregion


            #region 初始化comobox的下拉项
            List<string> inParameters = new List<string>();
            inParameters.Add(BasicClass.comoboxItemName.Workshop);
            inParameters.Add(BasicClass.comoboxItemName.Cell_comp);
            inParameters.Add(BasicClass.comoboxItemName.Cell_color);
            //inParameters[1] = "wo_type";
            //inParameters[2] = "cristal_type";
            //inParameters[3] = "cell_size";
            //inParameters[4] = "cell_consumption";
            //inParameters[5] = "cell_type";
            //inParameters[6] = "wo_type";

            functionTag = new FuncTagInfo();
            functionTag.FuncName = "GetComoboxItems";
            functionTag.InputParams = inParameters.ToArray();

            WcfRequests.SendGeneralRequest(onReceiveComoboxItems, functionTag);
            #endregion

            BasicClass.EnterToTab(groupBox_cell_info);
            //asc.controllInitializeSize(this);

            //this.SizeChanged += new EventHandler(MachineMaintain_SizeChanged);

            changeColorTimer.Interval = 1000 * 60;//60 second
            changeColorTimer.Tick += new EventHandler(changeColorTimer_Tick);

            //创建文件夹，打印用
            if (!Directory.Exists(@"c:\BarcodePrint_cell"))
            {
                Directory.CreateDirectory(@"c:\BarcodePrint_cell");
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            
            //tbx_module_id.Text = tbx_module_id.Text.Trim().ToUpper();
            #region 检查输入格式是否正确
            string sCheckString = tbx_eff_low.Text + "|" + 
                tbx_eff_up.Text + "|" + 
                tbx_cellbatch_count.Text + "|" + 
                tbx_cell_power.Text;
            if (!BasicClass.isNumeric(sCheckString))
	        {
                MessageBox.Show("数字格式输入有误，请检查！");
                return;
	        }

            if (cbx_cell_comp.Text==string.Empty||          //电池片厂商
                cbx_workshop.Text==string.Empty||           //车间
                tbx_cell_erplot.Text==string.Empty||        //批次
                cbx_cell_color.Text == string.Empty ||         //电池片颜色
                tbx_eff_low.Text==string.Empty||            //转换效率低
                tbx_eff_up.Text==string.Empty||             //转换效率高
                tbx_cell_power.Text==string.Empty||         //单片功率
                tbx_cell_grade.Text==string.Empty||         //片源等级
                tbx_cellbatch_count.Text==string.Empty      //片源数量
                )
            {
                MessageBox.Show("必输项不能为空");
                return;
            }
           
            #endregion

            List<string> inParameters = new List<string>();
            inParameters.Add(((ListItem)cbx_workshop.SelectedItem).ID);                    //车间编号
            inParameters.Add(tbx_cell_erplot.Text);                 //电池erp批号
            inParameters.Add(cbx_cell_color.Text);                  //片源颜色
            inParameters.Add(tbx_eff_low.Text);                     //片源效率低      
            inParameters.Add(tbx_eff_up.Text);                      //片源效率高
            inParameters.Add(tbx_cell_power.Text);                  //单片功率
            inParameters.Add(tbx_cell_grade.Text);                  //片源等级
            inParameters.Add(tbx_cellbatch_count.Text);             //分批数量
            inParameters.Add(((ListItem)cbx_cell_comp.SelectedItem).ID);    //电池厂商

            FuncTagInfo functionTag = null;
            functionTag = new FuncTagInfo();
            functionTag.FuncName = "CreateCellBatchNo";
            functionTag.InputParams = inParameters.ToArray() ;
            functionTag.UserName = _user;

            WcfRequests.SendGeneralRequest(onCreateBatchNoCompleted, functionTag);
        }

        #region wcf received
        /// <summary>
        /// wcf返回
        /// </summary>
        /// <param name="receiveData"></param>
        private void onCreateBatchNoCompleted(CallBackData receiveData)
        {
            if (!receiveData.bIsOK)
            {
                MessageBox.Show(receiveData.Result);
                return;
            }

            string strWriteFileStream = receiveData.Result;
            
            toolStripStatusLabelRight.Text = "成功插入一条新数据";
            toolStripStatusLabelRight.BackColor = Color.Green;
            changeColorTimer.Start();
            BasicClass.resetControlState(groupBox_cell_info);

            #region 打印电池片分批条码

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"c:\BarcodePrint_cell\cell.txt", false))
            {
                file.WriteLine(strWriteFileStream);
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"c:\BarcodePrint_cell\usb.bat", false))
            {
                file.WriteLine("@echo off");
                file.WriteLine("Bartend.exe /F=\"C:\\BarcodePrint_cell\\cell.btw\" /D=\"C:\\BarcodePrint_cell\\cell.txt\" /P /x");
            }

            try
            {
                System.Diagnostics.Process.Start(@"c:\BarcodePrint_cell\usb.bat");
            }
            catch (Exception)
            {
                throw;
            }
            #endregion

        }

        /// <summary>
        /// 初始化下拉项
        /// </summary>
        /// <param name="receiveData"></param>
        private void onReceiveComoboxItems(CallBackData receiveData)
        {
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
                    cbx_workshop.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Cell_comp)
                {
                    cbx_cell_comp.Items.Add(new ListItem(sourceName, displayName));
                }
                else if (ComoboxName == BasicClass.comoboxItemName.Cell_color)
                {
                    cbx_cell_color.Items.Add(new ListItem(sourceName, displayName));
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


        #region useful functions
        /// <summary>
        /// 重置状态栏颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void changeColorTimer_Tick(object sender, EventArgs e)
        {
            changeColorTimer.Stop();
            toolStripStatusLabelRight.Text = "";
            toolStripStatusLabelRight.BackColor = Color.Transparent;
            //machine_id.Focus();
        }

        #endregion

        

    }
}
