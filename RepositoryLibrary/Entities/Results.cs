﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary.Entities
{
    public class Results
    {
        public int ResultId { get; private set; }
        public int SubjectId { get; set; }
        public  char Grade { get; set; }
    }
}