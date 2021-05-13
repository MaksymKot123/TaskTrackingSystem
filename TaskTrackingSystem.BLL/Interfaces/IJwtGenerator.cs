using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackingSystem.DAL.Models;

namespace TaskTrackingSystem.BLL.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(User user, string role);
    }
}
