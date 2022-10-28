﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary.Entities
{
    public class Subject
    {
        public int SubjectId { get; private set; }
        public string SubjectName { get; set; }

        public Subject(int subjectId)
        {
            SubjectId = subjectId;
        }
    }
}
