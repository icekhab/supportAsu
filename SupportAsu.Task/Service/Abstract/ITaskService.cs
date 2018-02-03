using SupportAsu.DTO;
using SupportAsu.DTO.Task;
using SupportAsu.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SupportAsu.Task.Service.Abstract
{
    public interface ITaskService
    {
        TaskModel GetEmptyModel(int? claimId, int? mainTaskId);
        SelectList GetAuditoriesList(int auditoryId);
        SelectList GetResponsibleList(int responsibleId);
        SelectList GetExecutorList();
        void InsertOrUpdate(TaskModel model, int[] Executors);
        void ChangeStatus(int id, string status);
        TaskMobileDto GetClaimTask(int claimId);
    }
}
