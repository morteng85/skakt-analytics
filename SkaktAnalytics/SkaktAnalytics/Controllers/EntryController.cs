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
        public IHttpActionResult Add([FromUri]string n, [FromUri]string u, [FromUri]string v)
        {
            try
            {
                var request = HttpContext.Current.Request;
                var hostInfo = Dns.GetHostEntry(IPAddress.Parse(request.UserHostAddress));

                var entry = new Entry(n, u)
                {
                    Version = v,
                    Agent = request.UserAgent,
                    IpAddress = request.UserHostAddress,
                    HostName = hostInfo.HostName
                };

                var svc = new EntryService();

                svc.AddEntry(entry);

                var userSvc = new UserTableRepository();

                userSvc.AddIfNotExists(new User(n, v));
            }
            catch (Exception ex)
            {
                var telemetry = new ExceptionTelemetry(ex);

                telemetry.Properties.Add("message", ex.Message);
                telemetry.Properties.Add("userName", n);
                telemetry.Properties.Add("url", u);

                telemetryClient.TrackException(telemetry);
            }
            
            return Ok("data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7");
        }
    }
}