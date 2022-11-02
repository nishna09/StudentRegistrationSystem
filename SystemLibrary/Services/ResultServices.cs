using RepositoryLibrary.Entities;
using RepositoryLibrary.Repository;
using System.Collections.Generic;

namespace ServicesLibrary.Services
{
    public class ResultServices : IResultServices
    {
        private readonly IResultRepository ResultRepository;
        public ResultServices(IResultRepository repository)
        {
            ResultRepository = repository;
        }
        public List<Subject> GetAllSubjects()
        {
            List<Subject> subjects = ResultRepository.GetAllSubjects();
            return subjects;
        }

       
    }
}
