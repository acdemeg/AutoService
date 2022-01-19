using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService
{
    /* 
     * Класс отображающий клиентов автосервиса
     */
    class Customers
    {
        public int OrderId { get; private set; }
        public string Name { get; private set; }
        public Cars Car { get; private set; }

        public Customers(string name, Cars car, int clientId)
        {
            OrderId = clientId;
            Name = name;
            Car = car;
        }
    }
}
