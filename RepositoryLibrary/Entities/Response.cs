﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLibrary.Entities
{
    public class Response
    {
        public bool Flag { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }

        public Response()
        {
        }
        public Response(bool flag, string message)
        {
            Flag = flag;
            Message = message;
        }

        public Response(bool flag, string message, string url)
        {
            Flag = flag;
            Message = message;
            Url = url;
        }
    }
}