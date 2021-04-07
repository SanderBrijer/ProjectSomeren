using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Log
    {
        public string Message { get; set; } // Log message
        public string Fullname { get; set; } // Fullname
        public string Source { get; set; } // Source of the log
        public string Method { get; set; } // Method of the problem
        public DateTime Date { get; set; } // Date & time of the accident
    }
}
