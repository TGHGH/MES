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
using Module.BasicClassLib;
using System.Net;
using WcfRequestNS.ServiceReference;

namespace SysConfig
{
    public partial class MachineMaintain : Form
    {
        //FuncTagInfo functionTag = null;
        //待完善--空间自适应
        //AutoSizeFormClass asc = new AutoSizeFormClass();
        Timer changeColorTimer = new Timer();
        string _user = string.Empty;//当前用户

        public MachineMaintain()
        {
            InitializeComponent();
        }

        private void MachineMaintain_Load(object sender, EventArgs e)
        {

            #region 获取当前主机的登录用户
            FuncTagInfo functionTag = null;
            functionTag = new FuncTagInfo();
            functionTag.FuncName = "getCurrentUser";
            functionTag.ComputerName = Dns.GetHostName(); ;

            WcfRequests.SendGeneralRequest(onCurrentUserReceive, functionTag);
            #endregion


            BasicClass.EnterToTab(groupBoxAddMachine);
            //asc.controllInitializeSize(this);

            //this.SizeChanged += new EventHandler(MachineMaintain_SizeChanged);

            changeColorTimer.Interval = 1000 * 60;//60 second
            changeColorTimer.Tick += new EventHandler(changeColorTimer_Tick);
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            #region 检查必输项
            if (machine_id.Text.Trim()==string.Empty||machine_name.Text.Trim()==string.Empty
                ||worksite_id.Text.Trim()==string.Empty)
            {
                MessageBox.Show("必选项不能为空！");
                return;
            }
            if (machine_id.Text.Trim().Length!=9)
            {
                MessageBox.Show("设备编码不符合规定，请重新编码！");
                return;
            }

            string workshop = machine_id.Text.Substring(0, 3);
            if (!System.Text.RegularExpressions.Regex.IsMatch(workshop, "^M\\d{2}$"))
            {
                MessageBox.Show("设备编码不符合规定，请重新编码！");
                return;
            }
            #endregion

            List<string> inParameters=new List<string>();
            inParameters.Add(machine_id.Text.Trim());
            inParameters.Add(machine_name.Text.Trim());
            inParameters.Add(manufacturer.Text.Trim());
            inParameters.Add(worksite_id.Text.Trim());
            inParameters.Add(remark.Text.Trim());
            inParameters.Add(workshop);


            FuncTagInfo functionTag = null;
            functionTag = new FuncTagInfo();
            functionTag.FuncName = "MachineMaintain";
            functionTag.UserName = _user;
            functionTag.InputParams = inParameters.ToArray();

            WcfRequests.SendGeneralRequest(onReceiveComoboxItems, functionTag);

        }
        
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            resetControlState();
        }


        #region wcf received
        /// <summary>
        /// wcf返回
        /// </summary>
        /// <param name="receiveData"></param>
        private void onReceiveComoboxItems(CallBackData receiveData)
        {
            if (receiveData.bIsOK)
            {
                toolStripStatusLabelRight.Text = "成功插入一条新数据";
                toolStripStatusLabelRight.BackColor = Color.Green;
                changeColorTimer.Start();
                resetControlState();
            }
            else
            {
                MessageBox.Show(receiveData.Result);
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
            machine_id.Focus();
        }
        
        
        /// <summary>
        /// 清空所有文本框的内容
        /// </summary>
        private void resetControlState()
        {
            foreach (Control item in groupBoxAddMachine.Controls)
            {
                if (item is TextBox)
                {
                    if (!((TextBox)item).ReadOnly)
                    {
                        ((TextBox)item).Text = "";
                    }
                    
                }
            }

            machine_id.Focus();
        }

        //void MachineMaintain_SizeChanged(object sender, EventArgs e)
        //{
        //    asc.controllInitializeSize(this);
        //}

        #endregion

        
        
    }
}
