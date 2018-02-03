using PagedList;
using SupportAsu.DTO.Claim;
using SupportAsu.DTO.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupportAsu.DTO.Comment;
using SupportAsu.DTO.User;

namespace SupportAsu.Claim.Services.Abstract
{
    public interface IClaimService
    {
        void Insert(Model.Claim model);
        StaticPagedList<ClaimListModel> GetClaims(ClaimFilter filter,SortModel sort, int page, bool? isAll, int pageSize);
        void ViewClaim(int id);
        CommentModel Comment(CommentModel model);
        void ChangeStatus(int statusId, int id);
        void ApproveOrReject(int id, bool isApprove);
        IList<UserSmallModel> GetAuthors(string likeAuthor);
        IList<CommentModel> GetComments(int claimId);
    }
}
