using SupportAsu.DTO.Roles;
using SupportAsu.Helpers;
using SupportAsu.Model;
using SupportAsu.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace SupportAsu.WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IGenericRepository _repository;

        public ValuesController(IGenericRepository repository)
        {
            _repository = repository;
        }

        // GET api/values
        [System.Web.Http.Authorize(Roles = Role.User)]
        public IEnumerable<string> Get()
        {
            var lolq = User.Identity.GetUserInfo();
            var lol = _repository.TableNoTracking<Model.Dictionary>().ToList();
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
