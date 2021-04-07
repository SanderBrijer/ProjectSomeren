using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomerenModel
{
    public class VAT
    {
        public DateTime Besteldatum { get; set; }
        public int drankPrijs { get; set; }
        public int drankBTW { get; set; }
        public int verkoopAantal { get; set; }
        public double totaalOmzet { get { return drankPrijs * this.verkoopAantal; } }
        public double BTW { get { return (totaalOmzet / 100) * drankBTW; } }

    }
}
