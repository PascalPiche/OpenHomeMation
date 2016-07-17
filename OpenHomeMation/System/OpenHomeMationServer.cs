﻿using System;

using System.ServiceModel;
using System.ServiceModel.Description;

namespace OHM.Sys
{

    [ServiceContract(Namespace = "http://Microsoft.ServiceModel.Samples")]
    public interface IOpenHomeMationServer
    {
        [OperationContract]
        String Query(String query);
    }


    public sealed class OpenHomeMationServer : IOpenHomeMationServer
    {
        public string Query(string query)
        {
            return "test";
        }
    }

    public sealed class OpenHomeMationServerImplementation
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
