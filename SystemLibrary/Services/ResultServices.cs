using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryLibrary.Repository;
using RepositoryLibrary.Entities;

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
