using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoService
{
    /* 
     * Вспомогательный класс для реализации служебного функционала
     */
    class Utils
    {
        /* 
         * Получение уникальных номеров для мастеров и заказов
         */
        public static int GetUniqueNumber()
        {
            TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
            return (int)t.TotalSeconds;
        }

        /* 
         * Получить самого незанятого механика
         */
        public static Mechanics GetAvailableMechanic(Dictionary<int, Mechanics> idMechanicsMap)
        {
            int counter = int.MaxValue;
            Mechanics master = null;

            foreach (Mechanics m in idMechanicsMap.Values)
            {
                int size = m.MapOrderIdCustomer.Count;
                if (size < counter)
                {
                    master = m;
                    counter = size;
                }
            }

            return master;
        }
        
        /* 
         * Получить список всех механиков в системе 
         */
        public static void ToListAllMechanics(Dictionary<int, Mechanics> idMechanicsMap)
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                         Список всех нанятых мастеров");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("---------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("     Имя мастера      Табельный номер     Клиентов в работе       Всего выполененных ремонтов");
            Console.WriteLine("---------------------------------------------------------------------------------------------------");
            Console.WriteLine();

            foreach (Mechanics m in idMechanicsMap.Values)
            {
                Console.WriteLine("{0,15}      {1,15}         {2,10}         {3,5}", 
                    m.Name, m.MechanicId, m.MapOrderIdCustomer.Count, m.MapOrderIdRepair.Count);
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------");
            Console.WriteLine();
        }

        /* 
         *  Получить текущий список машин в ремонте
         */
        public static void ToListAllCarsInServing(Dictionary<int, Mechanics> idMechanicsMap)
        {
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                 Список автомобилей находящихся в ремонте в данный момент");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("     Номер авто      Имя клиента     Имя мастера       Табельный номер мастера        Идентификатор заказа");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();

            foreach (Mechanics m in idMechanicsMap.Values)
            {
                foreach(Customers c in m.MapOrderIdCustomer.Values)
                {
                    Console.WriteLine("{0,10}      {1,10}         {2,10}         {3,10}         {4,10}", 
                        c.Car.RegNumber, c.Name, m.Name, m.MechanicId, c.OrderId);
                }
            }
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
        }

        /* 
         * Получить список выполнненных ремонтов
         */
        public static void ToListAllCarsofRepairs(Dictionary<int, Mechanics> idMechanicsMap)
        {
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                 Список автомобилей по которым выполненны ремонты");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Номер авто      Имя клиента     Имя мастера       Табельный номер мастера        Идентификатор заказа");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            Console.WriteLine();

            foreach (Mechanics m in idMechanicsMap.Values)
            {
                foreach (Repairs r in m.MapOrderIdRepair.Values)
                {
                    Console.WriteLine("{0,10}      {1,10}         {2,10}         {3,10}         {4,10}",
                        r.CarRegNumber, r.CustomerName, r.MechanicName, r.MechanicId, r.OrderId);
                }
            }
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
        }
    }
}
