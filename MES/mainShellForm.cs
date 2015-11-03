using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using WcfRequestNS;
using Login;
using System.IO;
using System.Xml.Linq;
using System.Net;
using System.Runtime.InteropServices;
using WcfRequestNS.ServiceReference;



namespace MES
{
    public partial class MESShell : Form
    {
        #region 读写ini文件
        // <summary>
        /// 写入INI文件
        /// </summary>
        /// <param name="section">节点名称[如[TypeName]]</param>
        /// <param name="key">键</param>
        /// <param name="val">值</param>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键</param>
        /// <param name="def">值</param>
        /// <param name="retval">stringbulider对象</param>
        /// <param name="size">字节大小</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);

        #endregion

        static string _currentUser="";

        public MESShell()
        {
            InitializeComponent();
        }

        private void MESShell_Load(object sender, EventArgs e)
        {
            //调试
#if !DEBUG
            UpdateDLLModels();
#endif
            frmLogin login = new frmLogin();
            login.cusEvtLogin += new frmLogin.AfterLogin(login_cusEvtLogin);  
            login.ShowDialog();

        }

        /// <summary>
        /// 根据配置文件中的模块更新程序
        /// </summary>
        private void UpdateDLLModels()
        {
            string currentPath = Environment.CurrentDirectory;

            System.Diagnostics.Process p=new System.Diagnostics.Process();
            p.StartInfo.FileName= "cmd.exe ";
            p.StartInfo.UseShellExecute=false;
            p.StartInfo.RedirectStandardInput=true;
            p.StartInfo.RedirectStandardOutput=true;   
            p.StartInfo.RedirectStandardError=true;
            p.StartInfo.CreateNoWindow=true;
            p.Start();
            //调试
            string cmdLine = @"copy /y \\192.168.0.91\tools\mesupdate\config.ini " + currentPath + "\\config.ini";
            //MessageBox.Show(cmdLine);
            p.StandardInput.WriteLine(cmdLine);    //调试 注意，程序目录一定不能有空格，否则无法下载
            p.StandardInput.WriteLine( "exit");    
            p.StandardOutput.ReadToEnd();   
            p.Close();

            string strFilePath = currentPath + "\\config.ini";
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString("Models", "models", "", temp, 1024, strFilePath);
            string[] models = temp.ToString().Split('|');

            p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe ";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();

            for (int i = 0; i < models.Count(); i++)
            {
                string model = models[i];
                p.StandardInput.WriteLine(@"copy /y \\192.168.0.91\tools\mesupdate\" + model + ".dll " + currentPath + "\\" + model + ".dll");    //调试
            }
            p.StandardInput.WriteLine("exit");   
            p.StandardOutput.ReadToEnd();
            p.Close();
        }

        void login_cusEvtLogin(string UserID)
        {
            string[] userInfo = UserID.Split('|');
            _currentUser = userInfo[0];
            this.toolStripUserLabel.Text = userInfo[1];

            string[] arrParameter = new string[2];

            arrParameter[0] = _currentUser;

            FuncTagInfo oFuncTagInfo = new FuncTagInfo();
            oFuncTagInfo.FuncName = "GetMenuItem";
            oFuncTagInfo.InputParams = arrParameter;
            WcfRequests.SendGeneralRequest(OnGetUserPermissionsCompleted, oFuncTagInfo);
        }

        private void OnGetUserPermissionsCompleted(CallBackData _Result)
        {
            //if (true)
            //{
            //    StreamWriter sw = new StreamWriter(Application.StartupPath + "\\permission.xml", true);
            //    sw.WriteLine(_Result.Result);
            //    sw.Close();

            //    return;
            //}

            TextReader oTR = new StringReader(_Result.Result);
            XElement oResult = XElement.Load(oTR);
            oTR.Close();

            IEnumerable<XElement> cReports =
                (from xNode in oResult.Descendants("MenuItem")
                 select xNode);

            BuildMenuItem(cReports);

            setCurrentUserInfo();
        }

        #region 创建菜单栏
        private void BuildMenuItem(IEnumerable<XElement> _cRports)
        {
            MenuStrip mainMenu = new MenuStrip();

            Dictionary<string, ToolStripMenuItem> dicMenuItem = new Dictionary<string, ToolStripMenuItem>();

            foreach (XElement oModuleNode in _cRports)
            {
                string sTopMenu = ((XElement)oModuleNode).Attribute("AccessName").Value.ToString();
                string sSubMenu = ((XElement)oModuleNode).Attribute("DisplayName").Value.ToString();
                string sFunctionName = ((XElement)oModuleNode).Attribute("FunctionName").Value.ToString();
                string sAssemblyName = ((XElement)oModuleNode).Attribute("Assembly").Value.ToString();

                if (dicMenuItem.Keys.Contains(sTopMenu))
                {
                    ToolStripMenuItem subMenu = new ToolStripMenuItem();
                    subMenu.Text = sSubMenu;
                    subMenu.Tag = sAssemblyName+"|"+sFunctionName;
                    subMenu.Click += new EventHandler(subMenu_Click);

                    dicMenuItem[sTopMenu].DropDownItems.Add(subMenu);
                }
                else
                {
                    ToolStripMenuItem topMenu = new ToolStripMenuItem();
                    topMenu.Text = sTopMenu;

                    ToolStripMenuItem subMenu = new ToolStripMenuItem();
                    subMenu.Text = sSubMenu;
                    subMenu.Tag = sAssemblyName + "|" + sFunctionName;
                    subMenu.Click += new EventHandler(subMenu_Click);

                    topMenu.DropDownItems.Add(subMenu);

                    dicMenuItem.Add(sTopMenu, topMenu);
                    
                    mainMenu.MdiWindowListItem = topMenu;
                    mainMenu.Items.Add(topMenu);
                }
            }

            mainMenu.Dock = DockStyle.Top;
            //将窗体的MainMenuStrip梆定为mainMenu.
            this.MainMenuStrip = mainMenu;
            //这句很重要。如果不写这句菜单将不会出现在主窗体中。
            this.Controls.Add(mainMenu);
            
        }

        /**/
        /// <summary>
        /// 菜单单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void subMenu_Click(object sender, EventArgs e)
        {
            //tag属性在这里有用到。
            string[] functionTag = ((ToolStripMenuItem)sender).Tag.ToString().Split('|');
            string assemblyName = functionTag[0];
            string functionName = functionTag[1];
            CreateFormInstance(assemblyName,functionName);
        }

        /**/
        /// <summary>
        /// 创建form实例。
        /// </summary>
        /// <param name="formName">form的类名</param>
        private void CreateFormInstance(string assemblyName, string functionName)
        {
            bool flag = false;
            //遍历主窗口上的所有子菜单
            for (int i = 0; i < this.MdiChildren.Length; i++)
            {
                //如果所点的窗口被打开则重新激活
                if (this.MdiChildren[i].Tag.ToString().ToLower() == functionName.ToLower())
                {
                    this.MdiChildren[i].Activate();
                    this.MdiChildren[i].Show();
                    this.MdiChildren[i].WindowState = FormWindowState.Maximized;
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                //如果不存在则用反射创建form窗体实例。
                Assembly asm = Assembly.Load(assemblyName);//程序集名
                object frmObj = asm.CreateInstance(assemblyName+"." + functionName);//程序集+form的类名。
                Form frms = (Form)frmObj;
                frms.Tag = functionName.ToString();
                frms.MdiParent = this;
                frms.WindowState = FormWindowState.Maximized;
                frms.Show();
            }
        }
    
        #endregion

        private void setCurrentUserInfo()
        {
            string[] arrParameter = new string[2];

            arrParameter[0] = _currentUser;
            arrParameter[1] = Dns.GetHostName();

            FuncTagInfo oFuncTagInfo = new FuncTagInfo();
            oFuncTagInfo.FuncName = "setCurrentUserInfo";
            oFuncTagInfo.UserName = _currentUser;
            oFuncTagInfo.ComputerName = Dns.GetHostName();
            oFuncTagInfo.InputParams = arrParameter;
            WcfRequests.SendGeneralRequest(onSetCurrentUserInfoCompleted, oFuncTagInfo);
        }

        private void onSetCurrentUserInfoCompleted(CallBackData userInfo)
        {
            if (!userInfo.bIsOK)
            {
                //自动退出
                Application.Exit();
            }
        }

    }
}
