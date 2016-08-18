using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public IEnumerable<Visitor> Get()
        {
            return _repository.All();
        }

        [Route("")]
        [HttpPost, HttpPut]
        public ApiResult Post([BindVisitor] Visitor value)
        {
            try
            {
                if (value == null || string.IsNullOrEmpty(value.Name))
                {
                    throw new ValidationException("Invalid visitor.");
                }

                _repository.Create(value);
                return new ApiResult {Success = true, Message = string.Empty};
            }
            catch (Exception ex)
            {
                return new ApiResult {Success = false, Message = ex.Message};
            }
        }
    }
}
