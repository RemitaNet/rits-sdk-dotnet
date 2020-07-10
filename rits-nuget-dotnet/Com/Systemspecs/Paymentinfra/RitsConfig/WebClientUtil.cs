using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsConfig;

namespace RemitaRITsGateway.Com.Systemspecs.Paymentinfra.RitsConfig
{

    /// <summary>
    /// 
    /// </summary>
    public class WebClientUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="POST"></param>
        /// <param name="BaseURL"></param>
        /// <param name="_Headers"></param>
        /// <returns></returns>
        //public static string PostResponse(String BaseURL, String APIMethod, String body, List<Header> headers)
        public static string PostResponse(String BaseURL,  String body, List<Header> headers)
        {
            Console.WriteLine("+++++++++ URL: " + $"{BaseURL}");

            Console.WriteLine();
            Console.WriteLine("++++++++++++++Body: " + body);
            Console.WriteLine();

            String response = string.Empty;
            try
            {
                var client = new WebClient();
                
                foreach (var i in headers)
                    client.Headers.Add(i.header, i.value);

                client.Encoding = System.Text.Encoding.UTF8;
                string method = "POST";

                //response = client.UploadString($"{BaseURL}{APIMethod}", method, body);
                response = client.UploadString($"{BaseURL}", method, body);
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;
        }
    }
}
