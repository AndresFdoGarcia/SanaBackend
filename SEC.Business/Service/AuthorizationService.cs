using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SEc.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SEc.Data.Models;

namespace SEC.Business.Service
{
    public class AuthorizationService
    {
        private readonly AutherManagerConfiguration _connectionString;

        public AuthorizationService(AutherManagerConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<SanaUser> ValidateCredentials(string credential)
        {
            if (string.IsNullOrEmpty(credential) || string.IsNullOrEmpty(credential.Split(' ')[1]))
                return null;
            var client = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri =
                    new Uri(
                        _connectionString.AutherString),
                Headers =
                {
                    {
                        HttpRequestHeader.Authorization.ToString(), $"Bearer {credential.Split(' ')[1]}"
                    },
                    { HttpRequestHeader.Accept.ToString(), "application/json" }
                }
            };
            var result = client.SendAsync(httpRequestMessage).Result;
            var response = await result.Content.ReadAsStringAsync();
            var json = JObject.Parse(response).GetValue("result");
            return JsonConvert.DeserializeObject<SanaUser>(json?.ToString() ?? string.Empty);
        }
    }
}
