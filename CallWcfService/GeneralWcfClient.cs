using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using BasicClassLib;

namespace CallWcfService
{
    public class GeneralWcfClient : ClientBase<IGeneralService>, IGeneralService  
    {
        private static EndpointAddress oCSEA = new EndpointAddress("net.tcp://ms-20150815qapl:4502/GeneralWcfService/GeneralService.svc");
        private static BindingElement oCSMetaElement = new TcpTransportBindingElement();
        private static CustomBinding oCSMetaBind = new CustomBinding(oCSMetaElement);



    }
}
