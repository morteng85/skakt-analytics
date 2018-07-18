using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using SkaktAnalytics.Models;
using SkaktAnalytics.Services;
using System;
using System.Net;
using System.Web;
using System.Web.Http;

namespace SkaktAnalytics.Controllers
{
    public class EntryController : ApiController
    {
        private TelemetryClient telemetryClient = new TelemetryClient();

        [HttpGet]
        public IHttpActionResult Add([FromUri(Name = "n")]string name, [FromUri(Name = "u")]string url, [FromUri(Name = "v")]string version, [FromUri(Name = "t")]string theme = "", [FromUri(Name = "l")]string lines = "", [FromUri(Name = "a")]string highlight = "")
        {
            var request = HttpContext.Current.Request;

            try
            {
                url = Utils.Reverse(url);
                name = Utils.Reverse(name);

                var entry = new Entry(name, url)
                {
                    Version = version,
                    Agent = request.UserAgent,
                    IpAddress = request.UserHostAddress
                };

                try
                {
                    var hostInfo = Dns.GetHostEntry(IPAddress.Parse(request.UserHostAddress));

                    entry.HostName = hostInfo.HostName;
                }
                catch { }
                
                var svc = new EntryService();

                svc.AddEntry(entry);

                var userSvc = new UserTableRepository();

                userSvc.AddOrUpdate(new User(name, version, theme, lines, highlight));
            }
            catch (Exception ex)
            {
                var telemetry = new ExceptionTelemetry(ex);

                telemetry.Properties.Add("message", ex.Message);
                telemetry.Properties.Add("userName", name);
                telemetry.Properties.Add("url", url);
                telemetry.Properties.Add("requestUrl", request.Url.AbsoluteUri.ToString());

                telemetryClient.TrackException(telemetry);
            }
            
            return Ok("data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7");
        }
    }
}