using SomerenDAL;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenLogic
{
    public class Student_Service
    {
        Student_DAO student_db = new Student_DAO();

        public List<Student> GetStudents()
        {
            try
            {
                List<Student> student = student_db.Db_Get_All_Students();
                return student;
            }
            catch (Exception)
            {
                throw new Exception("Someren (students) couldn't connect to the database");
            }

        }

        public List<Student> GetKamers()
        {
            try
            {
                List<Student> kamers = student_db.Db_Get_All_KamerNummers();
                return kamers;
            }
            catch (Exception)
            {
                throw new Exception("Someren (rooms) couldn't connect to the database");
            }
        }
        public List<Student> GetStudentsByRoom(int kamernummer)
        {
            try
            {
                List<Student> StudentsInRoom = student_db.Db_Get_All_Students_By_Room(kamernummer);
                return StudentsInRoom;
            }
            catch (Exception)
            {
                throw new Exception("Someren (rooms) couldn't connect to the database");
            }
        }
    }
}
