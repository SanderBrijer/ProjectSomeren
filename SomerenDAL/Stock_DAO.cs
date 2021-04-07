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
    public class Stock_DAO : Base
    {

        public List<Stock> Db_Get_All_StocksCondition()
        {
            string query = "SELECT d.DrankNummer, d.DrankNaam, d.DrankSoort, d.DrankPrijs, d.DrankBTW, d.VerkoopAantal, v.VoorraadAantal" +
                            " FROM drank AS d JOIN Voorraad AS V ON V.VoorraadNummer = d.DrankNummer" +
                            " WHERE DrankNaam != 'Water' AND DrankNaam != 'Kersensap' AND DrankNaam != 'Sinas' AND VoorraadAantal > 1 AND DrankPrijs > 1" +
                            " ORDER BY VoorraadAantal, DrankPrijs, VerkoopAantal";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        public List<Stock> Db_Get_All_Stocks()
        {
            string query = "SELECT Drank.DrankNummer, Drank.DrankNaam, Drank.DrankSoort, Drank.DrankPrijs, Drank.DrankBTW, Drank.VerkoopAantal, Voorraad.VoorraadAantal FROM drank JOIN Voorraad ON Voorraad.VoorraadNummer = Drank.DrankNummer";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        public void Db_Add_To_Stock(int VoorraadAantal, string DrankNaam, string DrankSoort, int DrankPrijs, int DrankBTW, int VerkoopAantal)
        {
            string query = $"INSERT INTO Voorraad VALUES ({VoorraadAantal}) INSERT INTO Drank VALUES ('{DrankNaam}', '{DrankSoort}', {DrankPrijs}, {DrankBTW}, {VerkoopAantal})";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteSelectQueryVoid(query, sqlParameters);
        }

        public void Db_Change_Stock(int VoorraadAantal, string DrankNaam, string DrankSoort, int DrankPrijs, int VerkoopAantal, string DrankNaamOld)
        {
            string query = $"UPDATE Drank SET DrankNaam = '{DrankNaam}', DrankSoort = '{DrankSoort}', DrankPrijs = {DrankPrijs}, VerkoopAantal = {VerkoopAantal} WHERE DrankNaam = '{DrankNaamOld}'";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteSelectQueryVoid(query, sqlParameters);
        }

        public void Db_Delete_Stock(string DrankNaam, int Id)
        {
            string query = $"DELETE FROM Drank WHERE DrankNaam = '{DrankNaam}' DELETE FROM Voorraad WHERE VoorraadNummer = {Id}";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            ExecuteSelectQueryVoid(query, sqlParameters);
        }

        public int Db_GetIdByName(string DrankNaam, List<Stock> stockpiles)
        {
            foreach (Stock stock in stockpiles)
            {
                if (stock.Name == DrankNaam)
                {
                    return (stock.Number);
                }
            }
            return 0;
        }

        private List<Stock> ReadTables(DataTable dataTable)
        {
            List<Stock> stockpiles = new List<Stock>();
            foreach (DataRow dr in dataTable.Rows)
            {
                Stock stock = new Stock()
                {
                    Number = (int)dr["DrankNummer"],
                    Name = (string)dr["DrankNaam"],
                    Kind = (string)dr["DrankSoort"],
                    Sellprice = (int)(dr["DrankPrijs"]),
                    Tax = (int)(dr["DrankBTW"]),
                    SellAmounts = (int)dr["VerkoopAantal"],
                    Stockpile = (int)dr["VoorraadAantal"],
                };

                stockpiles.Add(stock);

            }
            return stockpiles;
        }
    }
}
