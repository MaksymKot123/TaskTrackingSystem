using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskTrackingSystem.DAL.Interfaces;
using TaskTrackingSystem.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using TaskTrackingSystem.BLL.EmailSender;

namespace TaskTrackingSystem.BLL.ProjectStatusUpdater
{
    public static class ProjectStatusUpdater
    {
        public static void UpdateInfo(IUnitOfWork uow)
        {
            foreach (var proj in uow.ProjectRepo.GetAll().ToList())
            {
                if (proj.Tasks == null)
                    continue;

                if (proj.Tasks.Count > 0 && !proj.Status.Equals(Status.Completed))
                {
                    var oldStatus = proj.Status;
                    var completedTasksCount = proj.Tasks
                        .Count(x => x.Status.Equals(Status.Completed));
                    var tasksCount = proj.Tasks.Count;

                    var oldPercent = proj.PercentCompletion;

                    proj.PercentCompletion = 100.0 * completedTasksCount / tasksCount;

                    if (proj.PercentCompletion == 100.0)
                    {

                        proj.Status = Status.Completed;
                        uow.ProjectRepo.Edit(proj);
                        EmailSender.EmailSender.SendEmail(proj.ClientEmail, proj.Status);
                    }
                    else if (oldPercent < proj.PercentCompletion &&
                        proj.Status.Equals(Status.Started))
                    {
                        proj.Status = Status.OnProgress;
                        uow.ProjectRepo.Edit(proj);
                        EmailSender.EmailSender.SendEmail(proj.ClientEmail, proj.Status);
                    }
                    uow.SaveChanges();
                }
            }
        }
    }
}
