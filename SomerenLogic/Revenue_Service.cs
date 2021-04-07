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
    public class Revenue_Service
    {
        Revenue_DAO revenue_db = new Revenue_DAO();

        public List<Revenue> GetRevenueSellingCount(string[] datumbereik)
        {
            Revenue[] info = new Revenue[3];
            List<Revenue> infolist = new List<Revenue>();
            try
            {
                infolist = revenue_db.Db_Get_All_Revenue(datumbereik);

                return infolist;
            }
            catch (Exception)
            {
                throw new Exception("Someren (rooms) couldn't connect to the database");
            }

        }
    }
}
