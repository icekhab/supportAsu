using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SupportAsu.Model;
using SupportAsu.DTO;
using SupportAsu.DTO.Prophylaxis;

namespace SupportAsu.Prophylaxis.Services.Abstract
{
    public interface IProphylaxisService
    {
        SelectList GetAuditoriesList(int auditoryId);
        SelectList GetDaysList(int dayId);
        SelectList GetLessonsList(int lessonId);
        SelectList GetResponsibleList(int responsibleId);
        void InsertOrUpdate(Model.Prophylaxis model);
        List<ProphylaxisModel> GetProphylaxisList();
        Model.Prophylaxis GetProphylaxisList(int id);
        void Delete(int id);
    }
}
