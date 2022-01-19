using System;
using System.Collections.Generic;
using System.Text;

namespace AutoService
{
    class Program
    {
        readonly static Dictionary<int, Mechanics> idMechanicsMap = new Dictionary<int, Mechanics>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            /* 
             *  Главный цикл работы программы
             */
            do
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("                     Информационная учетная система – Автосервис");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(" ГЛАВНОЕ МЕНЮ (введите соответствующуюю цифру)");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" 1 - Добавить мастера");
                    Console.WriteLine(" 2 - Добавить автомобиль в очередь на ремонт");
                    Console.WriteLine(" 3 - Завершить выполненный ремонт  ");
                    Console.WriteLine(" 4 - Отказ от ремонта автомобиля");
                    Console.WriteLine(" 5 - Вывести список всех мастеров");
                    Console.WriteLine(" 6 - Вывести список автомобилей находящихся в ремонте");
                    Console.WriteLine(" 7 - Вывести список выполненных ремонтов");
                    Console.WriteLine();
                    Console.WriteLine(" 0 - Выйти из программы");
                    Console.WriteLine();

                    int value = int.Parse(Console.ReadLine());
                    if (value == 0) return;

                    switch (value)
                    {
                        case 1: AddMechanic();
                            break;
                        case 2: AddCarInQueue();
                            break;
                        case 3: CompleteOrCancelRepair("complete");
                            break;
                        case 4: CompleteOrCancelRepair("cancel");
                            break;
                        case 5: Utils.ToListAllMechanics(idMechanicsMap);
                            break;
                        case 6: Utils.ToListAllCarsInServing(idMechanicsMap);
                            break;
                        case 7: Utils.ToListAllCarsofRepairs(idMechanicsMap);
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" В главном меню вводите только цифры !!!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Console.WriteLine();
                }


            } while (true);
        }

        /* 
         *  Добавление мастера в систему
         */
        private static void AddMechanic()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" Введите имя мастера");
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.White;
            string name = Console.ReadLine();
            int mechanicId = Utils.GetUniqueNumber();
            Mechanics master = new Mechanics(name, mechanicId);
            idMechanicsMap.Add(mechanicId, master);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Мастер с именем {0} и табельным номером {1} добавлен в систему", name, mechanicId);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine();
        }

        /* 
         * Добавление автомобиля в очередь на ремонт
         */
        private static void AddCarInQueue()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(" Введите имя клиента");
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.White;
            string clientName = Console.ReadLine();
            int orderId = Utils.GetUniqueNumber();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(" Введите номер автомобиля");
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.White;
            string regNumber = Console.ReadLine();

            Cars car = new Cars(regNumber);
            Customers customer = new Customers(clientName, car, orderId);
            Mechanics mechanic = Utils.GetAvailableMechanic(idMechanicsMap);
            if (mechanic == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Не найдено доступных мастеров, необходимо нанять хотя бы одного мастера!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine();
                return;
            }
            mechanic.MapOrderIdCustomer.Add(orderId, customer);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Автомобиль с номером {0} был добавлен в очередь на ремонт", car.RegNumber);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine();
        }
        
        /* 
         * Отмена или завершение выполнения ремонта 
         */
        private static void CompleteOrCancelRepair(string action)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(" Введите номер автомобиля");
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.White;
            string regNumber = Console.ReadLine();

            Mechanics master = null;
            Customers client = null;

            foreach (Mechanics mechanic in idMechanicsMap.Values)
            {
               foreach(Customers customer in mechanic.MapOrderIdCustomer.Values)
               {
                    if(customer.Car.RegNumber.Equals(regNumber))
                    {
                        client = customer;
                        master = mechanic;
                    }
                    if (client != null)
                        break;
               }
                if (client != null)
                    break;
            }
            if (master != null)
            {
                if (action.Equals("complete"))
                {
                    master.MapOrderIdCustomer.Remove(client.OrderId);
                    Repairs repair = new Repairs(master.Name, client.Name, regNumber, master.MechanicId, client.OrderId);
                    master.MapOrderIdRepair.Add(client.OrderId, repair);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" Автомобиль с номером {0} отремонтирован", regNumber);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Console.WriteLine();
                }
                else if (action.Equals("cancel"))
                {
                    master.MapOrderIdCustomer.Remove(client.OrderId);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" Автомобиль с номером {0} был исключен из очереди на ремонт", regNumber);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Автомобиль с номером {0} не найден в системе!", regNumber);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
