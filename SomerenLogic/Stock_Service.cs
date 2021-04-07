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
    public class Stock_Service
    {
        Stock_DAO stock_db = new Stock_DAO();

        public List<Stock> GetStocksCondition()
        {
            try
            {
                List<Stock> stock = stock_db.Db_Get_All_StocksCondition();

                return stock;
            }
            catch (Exception)
            {
                throw new Exception("Someren (stocks) couldn't connect to the database");
            }

        }

        public List<Stock> GetStocks()
        {
            try
            {
                List<Stock> stock = stock_db.Db_Get_All_Stocks();

                return stock;
            }
            catch (Exception)
            {
                throw new Exception("Someren (stocks) couldn't connect to the database");
            }

        }

        public void AddToStock(int VoorraadAantal, string DrankNaam, string DrankSoort, int DrankPrijs, int DrankBTW, int VerkoopAantal)
        {
            try
            {
                stock_db.Db_Add_To_Stock(VoorraadAantal, DrankNaam, DrankSoort, DrankPrijs, DrankBTW, VerkoopAantal);
            }
            catch (Exception)
            {
                throw new Exception("Someren couldn't add a new item to the stock");
            }

        }

        public void ChangeStock(int VoorraadAantal, string DrankNaam, string DrankSoort, int DrankPrijs, int VerkoopAantal, string DrankNaamOld)
        {
            try
            {
                stock_db.Db_Change_Stock(VoorraadAantal, DrankNaam, DrankSoort, DrankPrijs, VerkoopAantal, DrankNaamOld);
            }
            catch (Exception)
            {
                throw new Exception("Someren couldn't change the item in the stock");
            }
        }

        public void DeleteStock(string DrankNaam, int Id)
        {
            try
            {
                stock_db.Db_Delete_Stock(DrankNaam, Id);
            }
            catch (Exception)
            {
                throw new Exception("Someren couldn't delete the item in the stock");
            }

        }

        public int GetIdByName(string DrankNaam)
        {
            try
            {
                List<Stock> stockpiles = stock_db.Db_Get_All_Stocks();
                int Id = stock_db.Db_GetIdByName(DrankNaam, stockpiles);
                return Id;
            }
            catch (Exception)
            {
                throw new Exception("Someren couldn't find the id in the database");
            }

        }
    }
}
