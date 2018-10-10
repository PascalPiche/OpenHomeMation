using System.ServiceModel;

namespace OHM.Sys
{
    public interface IOpenHomeMationCallback
    {
        [OperationContract(IsOneWay = true)]
        void CallBackFunction(string str);
    }
}
