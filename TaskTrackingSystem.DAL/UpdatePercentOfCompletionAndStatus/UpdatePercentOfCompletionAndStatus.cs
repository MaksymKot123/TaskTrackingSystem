using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskTrackingSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskTrackingSystem.DAL.UpdatePercentOfCompletionAndStatus
{
    public static class UpdatePercentOfCompletionAndStatus
    {
        public static void  UpdateInfo(DatabaseContext context)
        {
            foreach (var proj in context.Projects.Include(x => x.Tasks).ToList())
            {
                if (proj.Tasks == null)
                    continue;
                
                if (proj.Tasks.Count > 0)
                {
                    var oldStatus = proj.Status;
                    var completedTasksCount = proj.Tasks
                        .Count(x => x.Status.Equals(Enums.Status.Completed));
                    var tasksCount = proj.Tasks.Count;
                    if (proj.Status.Equals(Enums.Status.Completed))
                    {
                        var uncompletedTasksCount = tasksCount - completedTasksCount;
                        if (uncompletedTasksCount > 0)
                        {
                            proj.PercentCompletion = 100.0 * completedTasksCount / tasksCount;
                            proj.Status = Enums.Status.OnProgress;
                            context.Entry(proj).State = EntityState.Modified;
                            context.SaveChanges();
                            return;
                        }
                    }

                    var oldPercent = proj.PercentCompletion;
                    
                    proj.PercentCompletion = 100.0 * completedTasksCount / tasksCount;
                    
                    if (proj.PercentCompletion == 100.0)
                    {
                        
                        proj.Status = Enums.Status.Completed;
                    }
                    else if (oldPercent < proj.PercentCompletion &&
                        proj.Status.Equals(Enums.Status.Started))
                    {
                        proj.Status = Enums.Status.OnProgress;
                    }
                    context.Entry(proj).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }
    }
}
