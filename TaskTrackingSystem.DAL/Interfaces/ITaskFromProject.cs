using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackingSystem.DAL.Models;

namespace TaskTrackingSystem.DAL.Interfaces
{
    public interface ITaskRepository : IRepository<TaskProject>
    {
        TaskProject GetWithDetails(string taskName, string projName);
    }
}
