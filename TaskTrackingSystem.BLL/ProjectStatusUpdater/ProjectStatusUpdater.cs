using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskTrackingSystem.DAL.Interfaces;
using TaskTrackingSystem.DAL.Enums;
using Microsoft.EntityFrameworkCore;

namespace TaskTrackingSystem.BLL
{
    public static class ProjectStatusUpdater
    {
        public static void UpdateInfo(IUnitOfWork uow)
        {
            foreach (var proj in uow.ProjectRepo.GetAll().ToList())
            {
                if (proj.Tasks == null)
                    continue;

                if (proj.Tasks.Count > 0)
                {
                    var oldStatus = proj.Status;
                    var completedTasksCount = proj.Tasks
                        .Count(x => x.Status.Equals(Status.Completed));
                    var tasksCount = proj.Tasks.Count;
                    if (proj.Status.Equals(Status.Completed))
                    {
                        var uncompletedTasksCount = tasksCount - completedTasksCount;
                        if (uncompletedTasksCount > 0)
                        {
                            proj.PercentCompletion = 100.0 * completedTasksCount / tasksCount;
                            proj.Status = Status.OnProgress;
                            uow.ProjectRepo.Edit(proj);//Entry(proj).State = EntityState.Modified;
                            uow.SaveChanges();
                            return;
                        }
                    }

                    var oldPercent = proj.PercentCompletion;

                    proj.PercentCompletion = 100.0 * completedTasksCount / tasksCount;

                    if (proj.PercentCompletion == 100.0)
                    {

                        proj.Status = Status.Completed;
                        uow.ProjectRepo.Edit(proj);
                    }
                    else if (oldPercent < proj.PercentCompletion &&
                        proj.Status.Equals(Status.Started))
                    {
                        proj.Status = Status.OnProgress;
                        uow.ProjectRepo.Edit(proj);
                    }
                    //context.Entry(proj).State = EntityState.Modified;
                    uow.SaveChanges();
                }
            }
        }
    }
}
