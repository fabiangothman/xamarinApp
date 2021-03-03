using RestSharp;
using System;

namespace SURA.Services
{
    public class SURAContactService
    {
        public void ContactSURA(string name, string mail, string cellphone, string asunto, string description, string action, string companyName = null)
        {
            var client = new RestClient();
            IRestResponse EXresponse = new RestResponse();
            try
            {
                var request = new RestRequest("https://sura--uat.my.salesforce.com/servlet/servlet.WebToCase", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("orgid", "00D3F000000DCp4");
                request.AddParameter("00N3F0000026unf", "APP Mobile");
                request.AddParameter("retURL", "https://segurossura.com.pa");
                request.AddParameter("name", name);
                request.AddParameter("email", mail);
                request.AddParameter("phone", cellphone);
                request.AddParameter("subject", asunto);
                request.AddParameter("description", description);
                request.AddParameter("company", companyName);
                request.AddParameter("00N3F0000026q02", action);
                request.RequestFormat = DataFormat.Json;
                request.Timeout = 30000;
                IRestResponse response = client.Execute(request);
                var content = response.Content;
                //string errorMessage = response.ErrorMessage;
                //Exception errorException = response.ErrorException;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}