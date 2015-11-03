using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using WcfRequestNS.ServiceReference;

namespace WcfRequestNS
{
    public static class WcfRequests
    {
        public delegate void GeneralServiceStringReceiveDelegate(CallBackData _result);
        public delegate void WipServiceStringReceiveDelegate(CallBackData _result);

        private static GeneralService _oGeneralService = new GeneralService();
        private static WipService _oWipService = new WipService();

        public static void SendGeneralRequest(GeneralServiceStringReceiveDelegate _evt, FuncTagInfo _tagInfo)
        {

            try
            {
                _oGeneralService.RequestGeneralService(_tagInfo, _evt);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void SendWipRequest(WipServiceStringReceiveDelegate _evt, FuncTagInfo _tagInfo)
        {

            try
            {
                _oWipService.RequestWipService(_tagInfo, _evt);
            }
            catch (Exception)
            {
                throw;
            }
        }


        #region wcf service
        private class GeneralService : IDisposable
        {
            private static GeneralServiceClient _proxy =null;

            public GeneralService()
            {
                try
                {
                    _proxy = new GeneralServiceClient();
                }
                catch (Exception)
                {
                    
                    throw;
                }
                
            }

            public void RequestGeneralService(FuncTagInfo _oTagInfo, GeneralServiceStringReceiveDelegate _Receiver)
            {
                try
                {
                    _Receiver(_proxy.Request(_oTagInfo));
                }
                catch (Exception er)
                {
                    throw er;
                }
            }

            public void Dispose()
            {
                _proxy.Close();
            }


        }


        private class WipService : IDisposable
        {
            private static WipServiceClient _wipServiceProxy = null;

            public WipService()
            {
                try
                {
                    _wipServiceProxy = new WipServiceClient();
                }
                catch (Exception)
                {

                    throw;
                }

            }

            public void RequestWipService(FuncTagInfo _oTagInfo, WipServiceStringReceiveDelegate _Receiver)
            {
                try
                {
                    _Receiver(_wipServiceProxy.Request(_oTagInfo));
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public void Dispose()
            {
                _wipServiceProxy.Close();
            }


        }

        #endregion
        

    }
}
