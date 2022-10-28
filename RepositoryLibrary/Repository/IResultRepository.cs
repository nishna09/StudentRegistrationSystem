using System;
using System.Collections.Generic;
using RepositoryLibrary.Entities;

namespace RepositoryLibrary.Repository
{
    public interface IResultRepository
    {
        List<Subject> GetAllSubjects();
    }

    
}
