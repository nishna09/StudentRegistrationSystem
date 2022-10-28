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
        private readonly IResultRepository _resultRepository;
        public ResultServices(IResultRepository repository)
        {
            _resultRepository = repository;
        }

       
        public List<Subject> GetAllSubjects()
        {
            List<Subject> subjects = _resultRepository.GetAllSubjects();
            return subjects;
        }

       
    }
}
