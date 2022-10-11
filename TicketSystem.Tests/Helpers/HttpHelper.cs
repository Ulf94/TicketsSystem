using Newtonsoft.Json;
using System.Text;

namespace TicketSystem.Tests.Helpers
{
    public static class HttpHelper
    {
        public static HttpContent ToJsonHttpContent(this object obj)
        {

            var json = JsonConvert.SerializeObject(obj);
            var httpContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            return httpContent;
        }
    }
}
