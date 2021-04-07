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
    public class CashRegister_DAO : Base
    {
        public List<CashRegister> Db_Get_All_CashRegister()
        {
            string query = "SELECT DrankNummer, DrankNaam FROM Drank";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }
      
        private List<CashRegister> ReadTables(DataTable dataTable)
        {
            List<CashRegister> kassas = new List<CashRegister>();

            foreach (DataRow dr in dataTable.Rows)
            {

                CashRegister kassa = new CashRegister();
                kassa.DrankNaam = (string)dr["DrankNaam"];
                kassa.StudentNaam = (string)dr["StudentNaam"];


                kassas.Add(kassa);
            }
            return kassas;
        }
    }
}
