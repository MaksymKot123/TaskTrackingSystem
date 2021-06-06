using TaskTrackingSystem.DAL.Models;

namespace TaskTrackingSystem.DAL.Interfaces
{
    public interface ITaskRepository : IRepository<TaskProject>
    {
        /// <summary>
        /// Get task of project by name
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="projName"></param>
        /// <returns><see cref="DAL.Models.TaskProject"/></returns>
        TaskProject GetWithDetails(string taskName, string projName);
    }
}
