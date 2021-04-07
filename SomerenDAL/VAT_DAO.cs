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
    public class VAT_DAO : Base
    {
        public List<VAT> Db_Get_All_VAT() 
        {
            string query = "SELECT B.BestelDatum, D.DrankPrijs, B.DrankAantal, D.DrankBTW FROM Bestelling AS B JOIN Drank as D on B.DrankNummer = D.DrankNummer";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTablesLowVAT(ExecuteSelectQuery(query, sqlParameters));
        }
        private List<VAT> ReadTablesLowVAT(DataTable dataTable)
        {
            List<VAT> allVAT = new List<VAT>();
            foreach (DataRow dr in dataTable.Rows)
            {
                VAT vat = new VAT()
                {
                    drankPrijs = (int)dr["DrankPrijs"],
                    drankBTW = (int)dr["DrankBTW"],
                    verkoopAantal = (int)dr["DrankAantal"],
                    Besteldatum = (DateTime)dr["BestelDatum"],
                };
                allVAT.Add(vat);
            }
            return allVAT;
        }
    }
}
