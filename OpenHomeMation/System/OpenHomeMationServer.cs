using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.ServiceModel;
using System.ServiceProcess;
using System.Configuration;
using System.Configuration.Install;
using System.ServiceModel.Description;


namespace OHM.Sys
{

    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface IOpenHomeMationServer
    {
        [OperationContract]
        String Query(String query);
    }


    public class OpenHomeMationServer : IOpenHomeMationServer
    {



        public string Query(string query)
        {
            return "test";
        }
    }

    public class OpenHomeMationServerImplementation
    {
        public static void Run() {
            ServiceHost ohmService = null;
            try
            {

                //Base Address for StudentService
                Uri httpBaseAddress = new Uri("http://localhost/ohm/api/");

                //Instantiate ServiceHost
                ohmService = new ServiceHost(typeof(OpenHomeMationServer), httpBaseAddress);

                //Add Endpoint to Host
                ohmService.AddServiceEndpoint(typeof(IOpenHomeMationServer), new WSHttpBinding(), "");

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
