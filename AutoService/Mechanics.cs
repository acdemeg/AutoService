using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService
{
    /* 
     * Класс отображающий мастеров автосервиса
     */
    class Mechanics
    {
        public string Name { get; private set; }
        public int MechanicId { get; private set; }
        public Dictionary<int, Customers> MapOrderIdCustomer { get; private set; }
        public Dictionary<int, Repairs> MapOrderIdRepair { get; private set; }


        public Mechanics(string name, int mechanicId)
        {
            Name = name;
            MechanicId = mechanicId;
            MapOrderIdCustomer = new Dictionary<int, Customers>();
            MapOrderIdRepair = new Dictionary<int, Repairs>();
        }
    }
}
