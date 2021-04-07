using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class Stock
    {
        public int Number { get; set; } // number of the drinks
        public string Name { get; set; } // name of the drinks
        public string Kind { get; set; } // kind of the drinks
        public int Sellprice { get; set; } // price of the drinks
        public int Tax { get; set; } // tax of the drinks
        public int SellAmounts { get; set; } // amounts of the drinks
        public int Stockpile { get; set; } // number of drinks availible
    }
}
