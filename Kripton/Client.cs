using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace Kripton
{
    class Client
    {

        Coins coins;


        public string makeRequest(string url)
        {
            var responseValue = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)request.GetResponse();



                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                            
                            coins = JsonConvert.DeserializeObject<Coins>(responseValue);
                            
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                responseValue = "{\"errorMessages\":[\"" + ex.Message.ToString() + "\"],\"errors\":{}}";
            }
            finally
            {
                if (response != null)
                {
                    ((IDisposable)response).Dispose();
                }
            }

            return responseValue;
        }


        public Coins GetCoins()
        {
            return coins;
        }

    }
}
    


