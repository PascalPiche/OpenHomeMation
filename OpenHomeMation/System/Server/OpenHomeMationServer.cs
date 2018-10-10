using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace OHM.Sys
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public sealed class OpenHomeMationServer : IOpenHomeMationServer
    {
        public OpenHomeMationServer(string test)
        {

        }

        public void Login(string user)
        {
            //TODO LOG

            //TODO VALIDATE CLIENT
            IOpenHomeMationCallback callback = OperationContext.Current.GetCallbackChannel<IOpenHomeMationCallback>();

            if (true)
            {
                callback.CallBackFunction("Calling from Call Back");
            }
            else
            {
                //Not authorize
            }
        }
    }

    public sealed class OpenHomeMationServerCreator
    {

        public static bool Launch(IDictionary<string, object> config)
        {
            ServiceHost ohmService = null;
            bool result = false;
            try
            {
                string protocole = "http";
                string host = "localhost";
                string path = "/ohm/api/";

                //Base Address
                Uri httpBaseAddress = new Uri(protocole + "://" + host + ":" + config["port"] + path);
                
                //Instantiate ServiceHost
                var server = new OpenHomeMationServer("todo");
                ohmService = new ServiceHost(server, httpBaseAddress);

                //Add Endpoint to Host
                ohmService.AddServiceEndpoint(typeof(IOpenHomeMationServer), new WSDualHttpBinding(), "");

                //Metadata Exchange
                ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior();
                serviceBehavior.HttpGetEnabled = true;
                ohmService.Description.Behaviors.Add(serviceBehavior);

                //Open
                ohmService.Open();
                Console.WriteLine("Server Endpoint is live now at: {0}", httpBaseAddress);
                result = true;
            }
            catch (Exception ex)
            {
                ohmService = null;
                Console.WriteLine("There is an issue with the server endpoint: {0}", ex.Message);
                result = false;
            }
            return result;
        }
    }
}
