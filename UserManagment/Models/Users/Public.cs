using SupportAsu.DTO;
using SupportAsu.DTO.Claim;
using SupportAsu.DTO.Comment;
using SupportAsu.DTO.Dictionary;
using SupportAsu.Model;
using SupportAsu.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace SupportAsu.UserManagment.Models.Users
{
    public class Public : User
    {
        private IGenericRepository _repository;
        public Public()
        {
            _repository = DependencyResolver.Current.GetService<IGenericRepository>();
        }
        public override IQueryable<ClaimListModel> GetClaims(bool? isAll = null)
        {
            var dateend = DateTime.Now.Date.AddYears(-1);
            var claims = _repository.TableNoTracking<Model.Claim>().Include(x => x.Category).Include(x => x.Status).Include(x => x.User).
                Where(x => (x.User.Login == Login && !x.Deleted && x.UpdatedAt >= dateend)).Select(x =>
                  new ClaimListModel
                  {
                      Author = x.User.Name,
                      Category = x.Category.Value,
                      Date = x.CreatedAt,
                      Id = x.Id,
                      Status = new DicValueSmallModel { Id = x.Status.Id, Code = x.Status.Code, Value = x.Status.Value },
                      Title = x.Title
                  });
               
            return claims;
        }
        public override ClaimViewModel GetClaim(int claimId)
        {
            var dateend = DateTime.Now.Date.AddYears(-1);
            var claim = (
                from c in _repository.TableNoTracking<Model.Claim>().Include(x => x.Category).Include(x => x.Status).Include(x => x.User).Include(x=>x.Auditory)
                join cm in _repository.TableNoTracking<CommentClaim>().Include(x => x.User) on c.Id equals cm.ItemId
                into comment
                where !c.Deleted && c.UpdatedAt >= dateend && c.Id == claimId && c.UserId==Id
                select new ClaimViewModel
                {
                    Id = c.Id,
                    Author = c.User.Name,
                    Category = c.Category.Value,
                    Date = c.CreatedAt,
                    CloseDate = c.CloseDate,
                    isNeedApprove = false,
                    Status = new DicValueSmallModel { Id = c.Status.Id, Code = c.Status.Code, Value = c.Status.Value },
                    Title = c.Title,
                    Comments = comment.Select(x => new CommentModel { ItemId = c.Id, Date = x.CreatedAt, Name = x.User.Name, Text = x.Text }).ToList(),
                    Text=c.Text,
                    Auditory= c.Auditory == null ? null : new DicValueSmallModel { Id=c.Auditory.Id,Code=c.Auditory.Code,Value=c.Auditory.Value}
                }).FirstOrDefault();
            return claim;
        }
        public override string HomePage
        {
            get
            {
                return "/Claim/List";
            }
        }
    }
}
