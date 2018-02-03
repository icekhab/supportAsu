using SupportAsu.Claim.Services.Abstract;
using SupportAsu.DTO.Claim;
using SupportAsu.DTO.Roles;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using SupportAsu.Dictionary.Services.Abstract;
using SupportAsu.DTO.Comment;
using SupportAsu.DTO.Task;
using SupportAsu.DTO.User;
using SupportAsu.Model;
using SupportAsu.Task.Service.Abstract;

namespace SupportAsu.WebApi.Controllers
{
    public class TaskController : ApiController
    {
        private readonly ITaskService _taskService;
        private readonly IDictionaryService _dictionaryService;

        public TaskController(ITaskService taskService,
            IDictionaryService dictionaryService)
        {
            _taskService = taskService;
            _dictionaryService = dictionaryService;
        }

        // GET api/values
        //[Authorize(Roles = Role.Administrator)]
        [Route("ClaimTask/{claimId}")]
        [HttpGet]
        public TaskMobileDto GetClaimTask(int claimId)
        {            
            return _taskService.GetClaimTask(claimId);
        }

    }
}
