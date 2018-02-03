using SupportAsu.Claim.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportAsu.Model;
using SupportAsu.Repository;
using System.Web;
using SupportAsu.Dictionary.Services.Abstract;
using SupportAsu.Helpers;
using PagedList;
using SupportAsu.DTO.Claim;
using UserManagment.Managers;
using SupportAsu.DTO.Roles;
using SupportAsu.DTO.Sorting;
using System.Linq.Dynamic;
using System.Data.Entity.Core.Objects;
using SupportAsu.DTO.Comment;
using SupportAsu.DTO.User;

namespace SupportAsu.Claim.Services
{
    public class ClaimService : IClaimService
    {
        private IGenericRepository _repository;
        private IDictionaryService _dicService;
        private IUserManager _userManager;
        public ClaimService(IUserManager userManager, IGenericRepository repository, IDictionaryService dicService)
        {
            _userManager = userManager;
            _dicService = dicService;
            _repository = repository;
        }
        public void Insert(Model.Claim model)
        {
            var user = HttpContext.Current.User.Identity.GetUserInfo();
            model.UserId = user.Id;
            var projectCategoty = _dicService.GetDictionaryValue("ClaimCategory", "Projector").Id;
            var confirmStatus = _dicService.GetDictionaryValue("ClaimStatus", "OnConfirm").Id;
            var inconsiderationStatus = _dicService.GetDictionaryValue("ClaimStatus", "Inconsideration").Id;
            if (model.CategoryId == projectCategoty && user.Role != Role.Director)
            {
                model.StatusId = confirmStatus;
            }
            else
            {
                model.StatusId = inconsiderationStatus;
            }
            _repository.InsertOrUpdate(model);
        }

        public StaticPagedList<ClaimListModel> GetClaims(ClaimFilter filter, SortModel sort, int page, bool? isAll, int pageSize = 10)
        {
            var user = HttpContext.Current.User.Identity.GetUserInfo();
            var userClaims = user.GetClaims(isAll);

            var claims = userClaims;

            #region Filter
            if (filter != null)
            {
                if (filter.DateStart != DateTime.MinValue)
                {
                    claims = claims.Where(x => EntityFunctions.TruncateTime(x.Date) >= filter.DateStart.Date);
                }
                if (filter.DateEnd != DateTime.MinValue)
                {
                    claims = claims.Where(x => EntityFunctions.TruncateTime(x.Date) <= filter.DateEnd.Date);
                }
                if (!string.IsNullOrEmpty(filter.Author))
                {
                    claims = claims.Where(x => x.Author.ToLower().Contains(filter.Author.ToLower()));
                }
                if (filter.Status != null && filter.Status.Count != 0)
                {
                    claims = claims.Where(x => filter.Status.Contains(x.Status.Id.Value));
                }
            }
            #endregion

            int totalCount = claims.Count();
            var claimsPagedList = claims.OrderBy(string.Format("{0} {1}", sort.Column, sort.Order)).Skip((page - 1) * pageSize).Take(pageSize);
            var pagedList = new StaticPagedList<ClaimListModel>(claimsPagedList, page, pageSize, totalCount);
            return pagedList;
        }

        public void ViewClaim(int id)
        {
            var user = HttpContext.Current.User.Identity.GetUserInfo();
            if (user.Role != Role.User)
            {
                if (!_repository.TableNoTracking<ViewedClaim>().Any(x => (x.ItemId == id && x.UserId == user.Id)))
                {
                    var viewedClaim = new ViewedClaim
                    {
                        ItemId = id,
                        UserId = user.Id
                    };
                    _repository.InsertOrUpdate(viewedClaim);
                }
            }
        }
        
        public CommentModel Comment(CommentModel model)
        {
            var user = HttpContext.Current.User.Identity.GetUserInfo();
            model.Date = DateTime.Now;
            model.Name = user.Name;
            var comment = new CommentClaim
            {
                UserId = user.Id,
                ItemId = model.ItemId,
                Text = model.Text
            };
            _repository.InsertOrUpdate(comment);
            return model;
        }

        public void ChangeStatus(int statusId, int id)
        {
            try
            {
                var user = HttpContext.Current.User.Identity.GetUserInfo();
                if(user.GetClaim(id)!=null)
                {
                    var claim = _repository.Table<Model.Claim>().FirstOrDefault(x => x.Id == id);
                    claim.StatusId = statusId;

                    var doneStatus = _dicService.GetDictionaryValue("ClaimStatus", "Done").Id;
                    if(doneStatus==statusId)
                    {
                        claim.CloseDate = DateTime.Now;
                    }

                    _repository.InsertOrUpdate(claim);
                }
            }
            catch { }
        }

        public void ApproveOrReject(int id, bool isNeedApprove)
        {
            try
            {
                var user = HttpContext.Current.User.Identity.GetUserInfo();
                if (user.GetClaim(id) != null)
                {
                    var claim = _repository.Table<Model.Claim>().FirstOrDefault(x => x.Id == id);
                    var approveStatus = _dicService.GetDictionaryValue("ClaimStatus", "Inconsideration").Id;
                    var rejectedStatus= _dicService.GetDictionaryValue("ClaimStatus", "Rejected").Id;
                    claim.StatusId = isNeedApprove ? approveStatus : rejectedStatus;
                    _repository.InsertOrUpdate(claim);
                }
            }
            catch { }
        }

        public IList<UserSmallModel> GetAuthors(string likeAuthor)
        {
            likeAuthor = likeAuthor.ToLower();
            return (from claim in _repository.TableNoTracking<Model.Claim>()
                join user in _repository.TableNoTracking<Model.User>() on claim.UserId equals user.Id
                where user.Name.ToLower().Contains(likeAuthor)
                select new UserSmallModel
                {
                    Id = user.Id,
                    Name = user.Name
                }).Distinct().ToList();
            
        }

        public IList<CommentModel> GetComments(int claimId)
        {
            return (_repository.TableNoTracking<CommentClaim>()
                .Include(x => x.User)
                .Where(x => x.ItemId == claimId)
                .Select(x => new CommentModel
                {
                    ItemId = x.ItemId,
                    Date = x.CreatedAt,
                    Name = x.User.Name,
                    Text = x.Text
                })).ToList();
        }
    }
}
