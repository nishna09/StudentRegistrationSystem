using System;
using System.Collections.Generic;
using SystemLibrary.Entities;

namespace SystemLibrary.DAL
{
    public interface IResultDAL
    {
        List<Subject> GetAllSubjects();
        List<Grade> GetAllGrades();
        Grade GetGrade(int id);
    }

    
}
