using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemLibrary.Entities;
using SystemLibrary.Repository;

namespace SystemLibrary.Services
{
    public interface IResultServices
    {
        List<Subject> GetAllSubjects();
        List<Grade> GetAllGrades();
        Grade GetGrade(int id);
    }
    public class ResultServices : IResultServices
    {
        private readonly IResultRepository _resultRepository;
        public ResultServices(IResultRepository repository)
        {
            _resultRepository=repository;
        }

        public List<Grade> GetAllGrades()
        {

            return _resultRepository.GetAllGrades();
        }

        public List<Subject> GetAllSubjects()
        {
            List<Subject> subjects = _resultRepository.GetAllSubjects();
            return subjects;
        }

        public Grade GetGrade(int id)
        {
            return _resultRepository.GetGrade(id);
        }
    }

}
