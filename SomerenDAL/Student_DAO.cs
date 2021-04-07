using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using SomerenModel;

namespace SomerenDAL
{
    public class Student_DAO : Base
    {
      
        public List<Student> Db_Get_All_Students()
        {
            string query = "SELECT StudentNummer, StudentNationaliteit, StudentNaam, KamerNummer FROM [STUDENT]";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        public List<Student> Db_Get_All_KamerNummers()
        {
            string query = "SELECT KamerNummer FROM Kamer GROUP BY KamerNummer ORDER BY KamerNummer;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTablesKamerNummers(ExecuteSelectQuery(query, sqlParameters));
        }
        public List<Student> Db_Get_All_Students_By_Room(int kamernummer)
        {
            string query = $"SELECT Student.StudentNaam FROM Student WHERE Student.KamerNummer = {kamernummer}";
            SqlParameter[] sqlParameters = new SqlParameter[0];

            return ReadTableStudent(ExecuteSelectQuery(query, sqlParameters));
        }


        private List<Student> ReadTables(DataTable dataTable)
        {
            List<Student> students = new List<Student>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Student student = new Student()
                {
                    Number = (int)dr["StudentNummer"],
                    StudentNationality = (string)dr["StudentNationaliteit"],
                    Name = (string)(dr["StudentNaam"]),
                    RoomNumber = (int)dr["KamerNummer"],
                    //ActivityNumber = (int)dr["ActiviteitNummer"],
                    //OrderNumber = (int)dr["BestellingNummer"]
                };
                students.Add(student);
            }
            return students;
        }

        private List<Student> ReadTablesKamerNummers(DataTable dataTable)
        {
            List<Student> kamerIndeling = new List<Student>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Student indeling = new Student()
                {
                    RoomNumber = (int)dr["KamerNummer"],
                };
                kamerIndeling.Add(indeling);
            }
            return kamerIndeling;
        }
        private List<Student> ReadTableStudent(DataTable dataTable)
        {
            List<Student> studentenInKamer = new List<Student>();
            foreach (DataRow dr in dataTable.Rows)
            {
                string naam = (string)dr["StudentNaam"];
                Student Student = new Student();
                Student.Name = naam;
                studentenInKamer.Add(Student);
            }
            return studentenInKamer;
        }

    }
}
