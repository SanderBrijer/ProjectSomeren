using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Room
    {
        public int Number { get; set; } // RoomNumber
        public int Capacity { get; set; } // number of beds
        public string Type { get; set; } //Type of room (student/teacherroom)
    }
}
