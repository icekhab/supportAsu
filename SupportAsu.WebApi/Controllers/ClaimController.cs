using SupportAsu.Claim.Services.Abstract;
using SupportAsu.DTO.Claim;
using SupportAsu.DTO.Roles;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using SupportAsu.Dictionary.Services.Abstract;
using SupportAsu.DTO.Comment;
using SupportAsu.DTO.Dictionary;
using SupportAsu.DTO.User;
using SupportAsu.Model;

namespace SupportAsu.WebApi.Controllers
{
    public class ClaimController : ApiController
    {
        private readonly IClaimService _claimService;
        private readonly IDictionaryService _dictionaryService;

        public ClaimController(IClaimService claimService,
            IDictionaryService dictionaryService)
        {
            _claimService = claimService;
            _dictionaryService = dictionaryService;
        }

        // GET api/values
        //[Authorize(Roles = Role.Administrator)]
        [Route("claims")]
        [HttpPost]
        public IEnumerable<ClaimMobileDto> Get([FromBody]ClaimFilterMobileDto filter)
        {            
            return _claimService.GetClaims(filter, new DTO.Sorting.SortModel {Column="Date",Order="desc" }, filter.Page, true, 20)
                .Select(x=> new ClaimMobileDto
                {
                    Id = x.Id,
                    Author = x.Author,
                    Category = x.Category,
                    Date = x.Date.ToLocalTime().Date.ToShortDateString(),
                    IsView = x.IsView,
                    Title = x.Title,
                    StatusName = x.Status.Value
                });
        }

        // GET api/values
        //[Authorize(Roles = Role.Administrator)]
        [Route("claim/statuses")]
        [HttpGet]
        public IEnumerable<DicValueSmallModel> GetStatusClaims()
        {
            return _dictionaryService.GetDictionaryValues("ClaimStatus").Select(x=> new DicValueSmallModel
            {
                Id = x.Id,
                Code = x.Code,
                Value = x.Value
            });
        }

        [Route("claim/authors/{likeAuthor}")]
        [HttpGet]
        public IList<UserSmallModel> GetAuthors(string likeAuthor)
        {
            return _claimService.GetAuthors(likeAuthor);
        }

        [Route("claim/comments/{claimId}")]
        [HttpGet]
        public IList<CommentModel> GetComments(int claimId)
        {
            return _claimService.GetComments(claimId);
        }

        [Route("claim/newcomments")]
        [HttpPost]
        public CommentModel PostComment([FromBody] CommentModel comment)
        {
            return _claimService.Comment(comment);
        }
       
    }
}
