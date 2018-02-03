using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SupportAsu.DTO.ProjectorShedules;

namespace SupportAsu.ProjectorShedule.Services.Abstract
{
    public interface IProjectorSheduleService
    {
        IList<ProjectorSheduleDto> GetProjectorShedule();
        ProjectorSheduleDto InsertOrUpdate(Model.ProjectorShedule projectorShedule);
        void Delete(int id);
        Model.ProjectorShedule GetById(int id);
        SelectList GetDictionaryList(string dicCode, int itemId);
        SelectList GetResponsible(int responsibleId);
    }
}
