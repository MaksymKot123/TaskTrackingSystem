using System;
using System.Collections.Generic;
using TaskTrackingSystem.DAL.Models;

namespace TaskTrackingSystem.DAL.Interfaces
{
    public interface IGetUserWithDetails
    {
        User GetUserWithDetails(string email);
    }
}
