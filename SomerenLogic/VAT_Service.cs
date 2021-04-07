using System;
using SomerenDAL;
using SomerenModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenLogic
{
    public class VAT_Service
    {
        VAT_DAO VAT_db = new VAT_DAO();

        public List<VAT> GetVAT() 
        {
            try
            {
                List<VAT> allVAT = VAT_db.Db_Get_All_VAT();
                return allVAT;
            }
            catch (Exception)
            {
                throw new Exception("Someren (Order) couldn't connect to the database");
            }
        }
    }
}
