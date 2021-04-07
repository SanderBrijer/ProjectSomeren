using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using SomerenModel;
using SomerenDAL;
using System.Data.Sql;

namespace SomerenDAL
{
    public class Revenue_DAO : Base
    {
        public List<Revenue> Db_Get_All_Revenue(string[] bereik)
        {
            string query = "Select " +
                "SUM([DrankAantal])AS[Afzet], " +
                "SUM(DrankAantal * DrankPrijs)AS[Omzet], " +
                "Count(BestellingNummer)AS[AantalKlanten] " +
                "FROM Drank " +
                "JOIN Bestelling " +
                "ON Drank.[DrankNummer]=Bestelling.DrankNummer " +
                "WHERE BestelDatum " +
                "BETWEEN '" + bereik[0] + "' AND '" + bereik[1] + "'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuerySUM(query, sqlParameters));
        }

        private List<Revenue> ReadTables(Revenue newrevenue)
        {
            List<Revenue> revenues = new List<Revenue>();

            revenues.Add(newrevenue);
            return revenues;
        }
    }
}
