using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WcfRequestNS;
using System.Text.RegularExpressions;
using System.Net;
using WcfRequestNS.ServiceReference;

namespace Login
{
    public partial class frmLogin : Form
    {
        public delegate void AfterLogin(string UserID);
        public event AfterLogin cusEvtLogin;

        private bool bExecuted=false;
        private FuncTagInfo TagInfo;

        string _sPC = Dns.GetHostName();
        string _sCurrentUser = string.Empty;

        public frmLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置关闭按钮
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;
            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                //捕捉关闭窗体消息      
                Application.ExitThread();
                return;
            }
            base.WndProc(ref m);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Trim()==""||pwdPassword.Text=="")
            {
                return;
            }

            if (bExecuted == true)
            {
                return;
            }
            else
            {
                bExecuted = true;
            }
            
            string[] arrParameter = new string[2];

            arrParameter[0] = txtUserName.Text.ToLower().ToString();
            arrParameter[1] = pwdPassword.Text;

            TagInfo = new FuncTagInfo();
            TagInfo.FuncName = "AuthenticateUser";
            TagInfo.InputParams = arrParameter;

            
            WcfRequests.SendGeneralRequest(OnAuthenticateUserCompleted, TagInfo);

        }

        private void OnAuthenticateUserCompleted(CallBackData _Result)
        {
            try
            {
                bExecuted = false;
                btnLogin.Enabled = true;

                string[] saRsult = Regex.Split(_Result.Result, "@");
                string sUserID = saRsult[0];
                string sReuslt = saRsult[1];

                if (sReuslt.Equals("Ok"))
                {
                    cusEvtLogin(sUserID);//txtUserName.Text);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Account/Password is invalid, please check caps is lock or not!");
                }
            }
            catch (Exception _ex)
            {
                MessageBox.Show(_Result.Result + " " + _ex.ToString());
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
            txtUserName.Select();

            txtUserName.KeyDown += new KeyEventHandler(txtUserName_KeyDown);
            pwdPassword.KeyDown += new KeyEventHandler(pwdPassword_KeyDown);
        }

        void pwdPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !txtUserName.Text.Equals(""))
            {
                btnLogin_Click(this, EventArgs.Empty);
            }
            else if (e.KeyCode == Keys.Enter && txtUserName.Text.Equals(""))
            {
                txtUserName.Focus();
            }

        }

        void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                pwdPassword.Focus();
                pwdPassword.SelectAll();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            pwdPassword.Text = "";

            txtUserName.Focus();
            txtUserName.Select();
        }
    }
}
