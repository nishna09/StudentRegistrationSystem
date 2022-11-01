using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RepositoryLibrary.Entities
{
    public class Results
    {
        public int ResultId { get; private set; }
        public Subject Subject { get; set; }
        public Grade Grade { get; set; }
    }
}
