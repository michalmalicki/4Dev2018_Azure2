
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace AzureTest
{
    public static class HttpOrderFormSave
    {
        [FunctionName("HttpOrderFormSave")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            PhotoOrder orderData = null;
            try
            {
                string requestBody = new StreamReader(req.Body).ReadToEnd();
                orderData = JsonConvert.DeserializeObject<PhotoOrder>(requestBody);
            } catch (System.Exception)
            {
                return new BadRequestObjectResult("Received data invalid");
            }
            if (orderData == null)
                return (ActionResult)new OkObjectResult($"Order object is null");
            return (ActionResult)new OkObjectResult($"Order processed successfully: e=" + orderData.CustomerEmail + " fn=" + orderData.FileName + " w=" + orderData.RequiredWidth + "h=" + orderData.RequiredHeight);

        }

        public class PhotoOrder
        {
            public string CustomerEmail { get; set; }
            public string FileName { get; set; }
            public int RequiredHeight { get; set; }
            public int RequiredWidth { get; set; }
        }
    }
}
