using System;

using System.ServiceModel;
using System.ServiceModel.Description;

namespace OHM.Sys
{

    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples", CallbackContract=typeof(IOpenHomeMationCallback))]
    public interface IOpenHomeMationServer
    {
        [OperationContract(IsOneWay=true)]
        void Login(String user);
    }

    public interface IOpenHomeMationCallback
    {
        [OperationContract(IsOneWay = true)]
        void CallBackFunction(string str);
    }

    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public sealed class OpenHomeMationServer : IOpenHomeMationServer
    {
        public void Login(string user)
        {
            IOpenHomeMationCallback callback = OperationContext.Current.GetCallbackChannel<IOpenHomeMationCallback>();

            callback.CallBackFunction("Calling from Call Back");

            //return "test";
        }
    }

    public sealed class OpenHomeMationServerImplementation
    {
        public static void Run() {
            ServiceHost ohmService = null;
            try
            {
                //Base Address
                Uri httpBaseAddress = new Uri("http://localhost/ohm/api/");

                //Instantiate ServiceHost
                ohmService = new ServiceHost(typeof(OpenHomeMationServer), httpBaseAddress);

                //Add Endpoint to Host
                ohmService.AddServiceEndpoint(typeof(IOpenHomeMationServer), new WSDualHttpBinding(), "");

                //Metadata Exchange
                ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior();
                serviceBehavior.HttpGetEnabled = true;
                ohmService.Description.Behaviors.Add(serviceBehavior);

                //Open
                ohmService.Open();
                Console.WriteLine("Service is live now at : {0}", httpBaseAddress);

            }
            catch (Exception ex)
            {
                ohmService = null;
                Console.WriteLine("There is an issue with ohmService" + ex.Message);
            }
        }
    }
}
