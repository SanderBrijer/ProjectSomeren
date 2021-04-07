using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SomerenDAL;
using SomerenModel;

namespace SomerenLogic
{
    public class CashRegister_Service
    {
         CashRegister_DAO cashregister_db = new CashRegister_DAO();

        public List<CashRegister> GetCashRegisters()
        {
            try
            {
                List<CashRegister> cashRegisters = cashregister_db.Db_Get_All_CashRegister();

                return cashRegisters;
            }
            catch (Exception e)
            {
                List<CashRegister> cashRegisters = new List<CashRegister>();
                CashRegister cashRegister = new CashRegister();

                cashRegister.DrankNaam = "Wijn";
                cashRegister.StudentNaam = "Louëlla Creemers";
                cashRegisters.Add(cashRegister);

                Console.Write(e);
                return cashRegisters;
            }

        }
    }
}
