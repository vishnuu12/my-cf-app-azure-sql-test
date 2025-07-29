using Newtonsoft.Json;
using System.Text;
using TestApplication.Models.Models;

namespace TestApplication.API.LogicalAppIntegration
{
    public class SendEmailViaLogic
    {
        internal async Task SendEmailViaLogicApp(RegisterDto user)
        {
            using (var client = new HttpClient())
            {
                var logicAppUrl = "https://prod-08.northcentralus.logic.azure.com:443/workflows/5cbd9c9cb1b7483db2c3f0197649ac1e/triggers/When_a_HTTP_request_is_received/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2FWhen_a_HTTP_request_is_received%2Frun&sv=1.0&sig=EWQuwRXzVsYTXdS6IqsaHKQHwd_-2blzKiHUywBPyaw"; // Replace with actual URL

                var payload = new
                {
                    fullName = user.FullName,
                    email = user.Email
                };

                var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(logicAppUrl, content);
                response.EnsureSuccessStatusCode(); // Optional: error handling
            }
        }

    }
}
