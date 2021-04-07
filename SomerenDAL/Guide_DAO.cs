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
    public class Guide_DAO : Base
    {

        public List<Guide> Db_Get_All_Guides()
        {
            string query = "SELECT Docent.DocentNaam, Begeleider.BegeleiderId FROM Docent JOIN Begeleider ON Begeleider.DocentID = Docent.DocentNummer";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        public List<Guide> Db_Get_All_GuidesById(int ActiviteitId)
        {
            string query = "SELECT Rooster.BegeleiderId, Docent.DocentNaam" +
            " FROM Rooster" +
            " JOIN Begeleider ON Begeleider.BegeleiderId = Rooster.BegeleiderId" +
            " JOIN Docent ON Docent.DocentNummer = Begeleider.DocentId" +
            $" WHERE Rooster.ActiviteitId = {ActiviteitId}";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        private List<Guide> ReadTables(DataTable dataTable)
        {
            List<Guide> guides = new List<Guide>();
            foreach (DataRow dr in dataTable.Rows)
            {
                Guide guide = new Guide()
                {
                    GuideName = (string)dr["DocentNaam"],
                    GuideNumber = (int)(dr["BegeleiderId"]),
                };

                guides.Add(guide);

            }
            return guides;
        }
    }
}
