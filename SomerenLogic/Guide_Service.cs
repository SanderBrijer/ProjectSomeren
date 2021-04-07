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
    public class Guide_Service
    {
        Guide_DAO guide_db = new Guide_DAO();

        public List<Guide> GetGuides()
        {
            try
            {
                List<Guide> guides = guide_db.Db_Get_All_Guides();
                return guides;
            }
            catch (Exception)
            {
                throw new Exception("Someren (guides) couldn't connect to the database");
            }
        }

        public List<Guide> GetGuidesByActivityId(int ActivityNumber)
        {
            try
            {
                List<Guide> guides = guide_db.Db_Get_All_GuidesById(ActivityNumber);
                return guides;
            }
            catch (Exception)
            {
                throw new Exception("Someren (guides) couldn't connect to the database");
            }
        }
    }
}
