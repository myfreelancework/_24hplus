﻿using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace _24hplusdotnetcore.Services
{
    public class CheckInfoServices
    {
        private readonly ILogger<CheckInfoServices> _logger;
        public CheckInfoServices(ILogger<CheckInfoServices> logger)
        {
            _logger = logger;
        }
        public dynamic CheckInfoByType(string greentype, string citizenID, string customerName)
        {
            try
            {
                if (greentype.ToUpper() == "C")
                {
                    return CheckInforFromMC(citizenID, customerName);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
        public dynamic CheckInforFromMC(string citizenID, string customerName)
        {
            try
            {
                string token = GetMCToken();
                if (!string.IsNullOrEmpty(token))
                {
                    var client = new RestClient(string.Format(Common.Config.MC_CheckInfo_URL,citizenID, customerName));
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Content-type", "application/json");
                    request.AddHeader("Authorization", "Bearer "+token+"");
                    request.AddHeader("x-security", ""+Common.Config.CredMC_Security_Key+"");
                    IRestResponse response = client.Execute(request);
                    dynamic content = JsonConvert.DeserializeObject<dynamic>(response.Content);
                    return content;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
        public string GetMCToken()
        {
            string token = "";
            try
            {
                var client = new RestClient(""+Common.Config.CredMC_URL+"");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("x-security", ""+Common.Config.CredMC_Security_Key+"");
                request.AddParameter("application/json", "{\n    \"username\": \""+Common.Config.CredMC_Username+"\",\n    \"password\": \""+Common.Config.CredMC_Password+"\",\n    \"notificationId\": \"notificationId.mekongcredit.3rd\",\n    \"imei\": \"imei.mekongcredit.3rd\",\n    \"osType\": \"ANDROID\"\n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                dynamic content = JsonConvert.DeserializeObject<dynamic>(response.Content);
                if (content.token != null)
                {
                    token = content.token;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return token;
        }
    }
}
