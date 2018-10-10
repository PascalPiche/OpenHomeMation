using System;
using System.ServiceModel;

namespace OHM.Sys
{

    [ServiceContract(Namespace = "http://scopollif.com", CallbackContract=typeof(IOpenHomeMationCallback))]
    public interface IOpenHomeMationServer
    {
        [OperationContract(IsOneWay=true)]
        void Login(String user);
    }
}
