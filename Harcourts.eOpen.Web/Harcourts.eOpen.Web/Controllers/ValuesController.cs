using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Hosting;
using System.Web.Http;
using Harcourts.eOpen.Web.Models;

namespace Harcourts.eOpen.Web.Controllers
{
    [RoutePrefix("visitors")]
    public class ValuesController : ApiController
    {
        private readonly VisitorRepository _repository;

        public ValuesController()
        {
            var appDataFolder = HostingEnvironment.MapPath("~/App_Data/");
            _repository = new VisitorRepository(appDataFolder);
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(_repository.All());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("")]
        [HttpPost, HttpPut]
        public IHttpActionResult Post([BindVisitor] Visitor value)
        {
            try
            {
                if (value == null || string.IsNullOrEmpty(value.Name))
                {
                    throw new ValidationException("Invalid visitor.");
                }

                _repository.Create(value);
                return Ok(new ApiResult {Success = true, Message = string.Empty});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("notifications/")]
        [HttpPost]
        public IHttpActionResult Notify()
        {
            try
            {
                var visitors=_repository.All();
                foreach (var visitor in visitors)
                {
                    var facebookVisitor = visitor as FacebookVisitor;
                    if (facebookVisitor == null)
                    {
                        continue;
                    }

                    if (!facebookVisitor.InTouch)
                    {
                        continue;
                    }

                    // PushNotification(facebookVisitor.FacebookUserId)
                }

                return Ok(new ApiResult {Success = true, Message = string.Empty});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
