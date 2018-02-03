using SupportAsu.Dictionary.Services.Abstract;
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
    public class Director : User
    {
        private IGenericRepository _repository;
        private IDictionaryService _dicService;
        public Director()
        {
            _dicService = DependencyResolver.Current.GetService<IDictionaryService>();
            _repository = DependencyResolver.Current.GetService<IGenericRepository>();
        }

        public override IQueryable<ClaimListModel> GetClaims(bool? isAll = null)
        {
            bool isFilter = false;
            if (isAll.HasValue && isAll.Value)
            {
                isFilter = true;
            }
            var dateend = DateTime.Now.Date.AddYears(-1);
            var confirmStatus = _dicService.GetDictionaryValue("ClaimStatus", "OnConfirm").Id;

            var claims = (
                from c in _repository.TableNoTracking<Model.Claim>().Include(x => x.Category).Include(x => x.Status).Include(x => x.User)
                join vc in _repository.TableNoTracking<ViewedClaim>().Where(x => x.UserId == Id) on c.Id equals vc.ItemId
                into view
                where !c.Deleted && c.UpdatedAt >= dateend && isFilter ? c.StatusId == confirmStatus : c.UserId == Id
                select new ClaimListModel
                {
                    Author = c.User.Name,
                    Category = c.Category.Value,
                    Date = c.CreatedAt,
                    Id = c.Id,
                    Status = new DicValueSmallModel { Id = c.Status.Id, Code = c.Status.Code, Value = c.Status.Value },
                    Title = c.Title,
                    IsView = isFilter ? view.Any() : true
                });
            return claims;
        }

        public override ClaimViewModel GetClaim(int claimId)
        {
            var dateend = DateTime.Now.Date.AddYears(-1);
            var confirmStatus = _dicService.GetDictionaryValue("ClaimStatus", "OnConfirm").Id;

            var claim = (
                from c in _repository.TableNoTracking<Model.Claim>().Include(x => x.Category).Include(x => x.Status).Include(x => x.User).Include(x=>x.Auditory)
                join cm in _repository.TableNoTracking<CommentClaim>().Include(x => x.User) on c.Id equals cm.ItemId
                into comment
                where !c.Deleted && c.UpdatedAt >= dateend && c.Id == claimId && (c.StatusId == confirmStatus || c.UserId == Id)
                select new ClaimViewModel
                {
                    Id = c.Id,
                    Author = c.User.Name,
                    Category = c.Category.Value,
                    Date = c.CreatedAt,
                    CloseDate = c.CloseDate,
                    isNeedApprove = c.StatusId == confirmStatus,
                    Status = new DicValueSmallModel { Id = c.Status.Id, Code = c.Status.Code, Value = c.Status.Value },
                    Title = c.Title,
                    Comments = comment.Select(x => new CommentModel { ItemId = c.Id, Date = x.CreatedAt, Name = x.User.Name, Text = x.Text }).ToList(),
                    Text=c.Text,
                    Auditory = c.Auditory == null ? null : new DicValueSmallModel { Id = c.Auditory.Id, Code = c.Auditory.Code, Value = c.Auditory.Value }
                }).FirstOrDefault();
            return claim;
        }

        public override string HomePage
        {
            get
            {
                return "Claim/List?isAll=True";
            }
        }
    }
}
