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
        public IHttpActionResult Add([FromUri(Name = "n")]string name, [FromUri(Name = "u")]string url, [FromUri(Name = "v")]string version)
        {
            try
            {
                var request = HttpContext.Current.Request;

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

                userSvc.AddIfNotExists(new User(name, version));
            }
            catch (Exception ex)
            {
                var telemetry = new ExceptionTelemetry(ex);

                telemetry.Properties.Add("message", ex.Message);
                telemetry.Properties.Add("userName", name);
                telemetry.Properties.Add("url", url);

                telemetryClient.TrackException(telemetry);
            }
            
            return Ok("data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7");
        }
    }
}