using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Student
    { 
        public string Name { get; set; } //StudentName
        public int Number { get; set; } // StudentNumber, e.g. 474791
        public DateTime BirthDate { get; set;  } // Date of birth
        public string StudentNationality { get; set; } // Nationality of the student (EN/NL)
        public int RoomNumber { get; set; } // Roomnummer of the student
        public int ActivityNumber { get; set; } // Activity number
        public int OrderNumber { get; set; } // The ordernumber of the student

    }
}
