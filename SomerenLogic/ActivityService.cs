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
    public class Activity_Service
    {
        Activity_DAO activity_db = new Activity_DAO();
        public List<Activity> GetActivities()
        {
            try
            {
                List<Activity> activities = activity_db.Db_Get_All_Activities();

                return activities;
            }
            catch (Exception)
            {
                throw new Exception("Someren (activities) couldn't connect to the database");
            }
        }

        public Schedule GetSchedule(int activiteitId)
        {
            try
            {
                Schedule schedule = activity_db.Db_Get_ScheduleById(activiteitId);

                return schedule;
            }
            catch (Exception)
            {
                throw new Exception("Someren (schedules) couldn't connect to the database");
            }
        }

        public Activity GetActivityByName(string activiteitNaam)
        {
            try
            {
                Activity activity = activity_db.Db_Get_ActivityByName(activiteitNaam);

                return activity;
            }
            catch (Exception)
            {
                throw new Exception("Someren (activities) couldn't connect to the database");
            }
        }

        public void AddToActivities(string activiteitNaam, int aantalStudenten, int aantalBegeleiders)
        {
            try
            {
                activity_db.Db_Add_To_Activities(activiteitNaam, aantalStudenten, aantalBegeleiders);
            }
            catch (Exception)
            {
                throw new Exception("Someren couldn't add a new item to the activities");
            }

        }

        public void ChangeActivity(string activiteitNaam, int aantalStudenten, int aantalBegeleiders, string ActiviteitNaamOud)
        {
            try
            {
                activity_db.Db_Change_Activity(activiteitNaam, aantalStudenten, aantalBegeleiders, ActiviteitNaamOud);
            }
            catch (Exception)
            {
                throw new Exception("Someren couldn't change the item in activities");
            }
        }

        public void AddToSchedule(int activiteitId, int begeleiderId, DateTime datum, TimeSpan startTijd, TimeSpan eindtijd)
        {
            try
            {
                activity_db.Db_Add_Schedule(activiteitId, begeleiderId, datum, startTijd, eindtijd);
            }
            catch (Exception)
            {
                throw new Exception("Someren couldn't add the item in schedule");
            }
        }

        public void ChangeSchedule(int activiteitId, int begeleiderId, DateTime datum, TimeSpan startTijd, TimeSpan eindtijd)
        {
            try
            {
                activity_db.Db_Change_Schedule(activiteitId, begeleiderId, datum, startTijd, eindtijd);
            }
            catch (Exception)
            {
                throw new Exception("Someren couldn't change the item in schedule");
            }
        }

        public void DeleteFromSchedule(int begeleiderId)
        {
            try
            {
                activity_db.Db_Delete_Schedule(begeleiderId);
            }
            catch (Exception)
            {
                throw new Exception("Someren couldn't delete the item in schedule");
            }
        }

        public void DeleteActivity(string ActiviteitNaam, int ActiviteitNummer)
        {
            try
            {
                activity_db.Db_Delete_Activity(ActiviteitNaam, ActiviteitNummer);
            }
            catch (Exception)
            {
                throw new Exception("Someren couldn't delete the item in activities");
            }

        }
    }
}
