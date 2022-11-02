using RepositoryLibrary.Entities;
using System.Collections.Generic;

namespace RepositoryLibrary.Repository
{
    public interface IResultRepository
    {
        List<Subject> GetAllSubjects();
    }

    
}
