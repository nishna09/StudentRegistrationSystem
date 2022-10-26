using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.DAL;
using SystemLibrary.Entities;

namespace SystemLibrary.Services
{
    public class ResultServices : IResultServices
    {
        private readonly IResultDAL _resultRepository;
        public ResultServices(IResultDAL repository)
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
