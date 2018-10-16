using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace LogicAppFunction
{
    public static class KeywordFunction
    {
        [FunctionName("KeywordFunction")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {

            log.Info("C# HTTP trigger function processed a request.");
            string host = "<insert cog services account url .. ex. https://westus.api.cognitive.microsoft.com>";
            string path = "/text/analytics/v2.0/keyPhrases";
            // Translate to German.

            string uri = host + path;

            // NOTE: Replace this example key with a valid subscription key.
            string key = "<sub key>";
            //deserialize correctly so I just get the content
            dynamic data = await req.Content.ReadAsAsync<object>();

            Text text = new Text { language = "en", id = "1", text = data["content"].ToString() };
            DocumentsRequest body = new DocumentsRequest();
            Text[] docs = new Text[] {text};
            body.documents = docs;
            var requestBody = JsonConvert.SerializeObject(body);

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage())
            {
                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(uri);
                request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                request.Headers.Add("Ocp-Apim-Subscription-Key", key);

                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<DocumentsResponse>(responseBody);
                return req.CreateResponse(HttpStatusCode.OK, String.Join(",", result.documents[0].keyPhrases));
            }

                
        }
    }
}
