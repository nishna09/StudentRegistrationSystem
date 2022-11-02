namespace RepositoryLibrary.Entities
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Subject() {}
        public Subject(int subjectId)
        {
            SubjectId = subjectId;
        }
    }
}
