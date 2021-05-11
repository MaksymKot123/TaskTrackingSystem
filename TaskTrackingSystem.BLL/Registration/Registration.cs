using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackingSystem.DAL.Interfaces;
using TaskTrackingSystem.DAL.Models;

namespace TaskTrackingSystem.BLL.Registation
{
    public class Registration
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DatabaseContext _database;

        public Registration(IUnitOfWork unitOfWork, DatabaseContext database)
        {
            _unitOfWork = unitOfWork;
            _database = database;
        }
    }
}
