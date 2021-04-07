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
    public class Activity_DAO : Base
    {
        public List<Activity> Db_Get_All_Activities()
        {
            string query = "SELECT Activiteit.ActiviteitId, Activiteit.ActiviteitNaam, Activiteit.AantalStudenten, Activiteit.AantalBegeleiders FROM Activiteit";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        public Schedule Db_Get_ScheduleById(int activiteitId)
        {
            string query = $"SELECT RoosterId, ActiviteitId, BegeleiderId, Datum, StartTijd, EindTijd FROM Rooster WHERE ActiviteitId = '{activiteitId}'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTablesSchedules(ExecuteSelectQuery(query, sqlParameters));
        }

        public Activity Db_Get_ActivityByName(string activiteitNaam)
        {
            string query = $"SELECT Activiteit.ActiviteitId, Activiteit.ActiviteitNaam, Activiteit.AantalStudenten, Activiteit.AantalBegeleiders FROM Activiteit WHERE Activiteit.ActiviteitNaam = '{activiteitNaam}'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTable(ExecuteSelectQuery(query, sqlParameters));
        }

        public void Db_Add_To_Activities(string activiteitNaam, int aantalStudenten, int aantalBegeleiders)
        {
            string query = $"INSERT INTO Activiteit VALUES ('{activiteitNaam}', '{aantalStudenten}', '{aantalBegeleiders}')";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteSelectQueryVoid(query, sqlParameters);
        }

        public void Db_Change_Activity(string ActiviteitNaam, int aantalStudenten, int aantalBegeleiders, string ActiviteitNaamOud)
        {
            string query = $"UPDATE Activiteit SET ActiviteitNaam = '{ActiviteitNaam}', AantalStudenten = '{aantalStudenten}', AantalBegeleiders = '{aantalBegeleiders}' WHERE ActiviteitNaam = '{ActiviteitNaamOud}'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteSelectQueryVoid(query, sqlParameters);
        }

        public void Db_Delete_Activity(string ActiviteitNaam, int ActiviteitId)
        {
            string query = $"DELETE FROM Activiteit WHERE ActiviteitNaam = '{ActiviteitNaam}' DELETE FROM Rooster WHERE ActiviteitId = '{ActiviteitId}'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteSelectQueryVoid(query, sqlParameters);
        }

        public void Db_Add_Schedule(int activiteitId, int begeleiderId, DateTime datum, TimeSpan startTijd, TimeSpan eindtijd)
        {
            string query;
            if (begeleiderId != 0)
                query = $"INSERT INTO Rooster VALUES ('{activiteitId}', '{begeleiderId}', '{datum}', '{startTijd}', '{eindtijd}')";
            else
                query = $"INSERT INTO Rooster VALUES ('{activiteitId}', NULL, '{datum}', '{startTijd}', '{eindtijd}')";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteSelectQueryVoid(query, sqlParameters);
        }

        public void Db_Change_Schedule(int activiteitId, int begeleiderId, DateTime datum, TimeSpan startTijd, TimeSpan eindtijd)
        {
            string query = $"UPDATE Rooster SET ActiviteitId = '{activiteitId}', BegeleiderId = '{begeleiderId}', Datum = '{datum}', StartTijd = '{startTijd}', EindTijd = '{eindtijd}' FROM Activiteit WHERE ActiviteitNaam = '{activiteitId}'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteSelectQueryVoid(query, sqlParameters);
        }

        public void Db_Delete_Schedule(int begeleiderId)
        {
            string query;
            if (begeleiderId != 0)
                query = $"DELETE FROM Rooster WHERE BegeleiderId = '{begeleiderId}'";
            else
                query = $"DELETE FROM Rooster WHERE BegeleiderId IS NULL";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteSelectQueryVoid(query, sqlParameters);
        }

        private List<Activity> ReadTables(DataTable dataTable)
        {
            List<Activity> activities = new List<Activity>();
            foreach (DataRow dr in dataTable.Rows)
            {
                Activity activity = new Activity()
                {
                    ActivityName = (string)dr["ActiviteitNaam"],
                    ActivityNumber = (int)dr["ActiviteitId"],
                    NumberOfStudents = (int)dr["AantalStudenten"],
                    NumberOfGuides = (int)dr["AantalBegeleiders"],
                };

                activities.Add(activity);

            }
            return activities;
        }

        private Schedule ReadTablesSchedules(DataTable dataTable)
        {
            foreach (DataRow dr in dataTable.Rows)
            {
                Schedule schedule = new Schedule()
                {
                    ActivityId = (int)dr["RoosterId"],
                    GuideId = (int)dr["ActiviteitId"],
                    Date = (DateTime)dr["Datum"],
                    StartTime = (DateTime)dr["StartTijd"],
                    EndTime = (DateTime)dr["EindTijd"],
                };
                return schedule;
            }
            return null;
        }

        private Activity ReadTable(DataTable dataTable)
        {
            foreach (DataRow dr in dataTable.Rows)
            {
                Activity activity = new Activity()
                {
                    ActivityName = (string)dr["ActiviteitNaam"],
                    ActivityNumber = (int)dr["ActiviteitId"],
                    NumberOfStudents = (int)dr["AantalStudenten"],
                    NumberOfGuides = (int)dr["AantalBegeleiders"],
                };
                return activity;
            }
            return null;
        }
    }
}
