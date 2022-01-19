using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService
{
    /* 
     * Класс отображающий список выполненных ремонтов
     */
    class Repairs
    {
        public string MechanicName { get; private set; }
        public string CustomerName { get; private set; }
        public string CarRegNumber { get; private set; }
        public int MechanicId { get; private set; }
        public int OrderId { get; private set; }

        public Repairs(string mechanicName, string customerName, string carRegNumber, int mechanicId, int orderId)
        {
            MechanicName = mechanicName;
            CustomerName = customerName;
            CarRegNumber = carRegNumber;
            MechanicId = mechanicId;
            OrderId = orderId;

        }
    }
}
