using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneralWcfService;
using WIPWcfService;

namespace GeneralConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> arrParameter = new List<string>();

            //GeneralService oService = new GeneralService(true);
            //FuncTagInfo TagInfo = new FuncTagInfo();
            

            //arrParameter.Add("aa");
            //arrParameter.Add("aa");
            //TagInfo.InputParams = arrParameter;
            //TagInfo.FuncName = "AuthenticateUser";

            //oService.Request(TagInfo);


            WIPWcfService.WipService wipWcfService = new WIPWcfService.WipService(true);
            FuncTagInfo TagInfo = new WipFuncTagInfo();
            arrParameter.Add("qweM2150922001");
            TagInfo.FuncName = "GetCellBatchInfo";
            TagInfo.InputParams = arrParameter;

            wipWcfService.Request(TagInfo);

            
            //Console.ReadLine();
        }
    }
}
