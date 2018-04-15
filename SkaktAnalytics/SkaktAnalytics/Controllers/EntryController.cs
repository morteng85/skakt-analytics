using SkaktAnalytics.Models;
using SkaktAnalytics.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SkaktAnalytics.Controllers
{
    public class EntryController : ApiController
    {
        // GET api/<controller>
        /*
        public IHttpActionResult Get()
        {
            var svc = new EntryService();

            return Ok(svc.GetEntries(50));
        }*/
        
        [HttpGet]
        public IHttpActionResult Add([FromUri]string n, [FromUri]string u, [FromUri]string v)
        {
            try
            {
                var entry = new Entry(n, u, v, HttpContext.Current.Request.UserHostAddress);
                var svc = new EntryService();

                svc.AddEntry(entry);
            }
            catch { }
            
            return Ok("data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7");
        }
    }
}