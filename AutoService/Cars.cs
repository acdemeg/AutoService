using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService
{
    /* 
     * Класс отображающий автомобили
     */
    class Cars
    {
        public string RegNumber { get; private set; }
 
        public Cars(string regnumber)
        {
            RegNumber = regnumber;
        }
    }
}
