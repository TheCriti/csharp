using System;
using System.Collections.Generic;

namespace CarServiceApp
{
    public class Customer
    {
        private int id;
        private string fullName;
        private string phone;

        public Customer(int id, string fullName, string phone)
        {
            this.id = id;
            this.fullName = fullName;
            this.phone = phone;
        }

        public int Id
        {
            get { return id; }
        }

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
    }

    public class Car
    {
        private int id;
        private string brand;
        private string model;
        private string number;
        private Customer owner;

        public Car(int id, string brand, string model, string number, Customer owner)
        {
            this.id = id;
            this.brand = brand;
            this.model = model;
            this.number = number;
            this.owner = owner;
        }

        public int Id
        {
            get { return id; }
        }

        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        public Customer Owner
        {
            get { return owner; }
            set { owner = value; }
        }
    }

    public class Mechanic
    {
        private int id;
        private string fullName;
        private string specialization;

        public Mechanic(int id, string fullName, string specialization)
        {
            this.id = id;
            this.fullName = fullName;
            this.specialization = specialization;
        }

        public int Id
        {
            get { return id; }
        }

        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        public string Specialization
        {
            get { return specialization; }
            set { specialization = value; }
        }
    }

    public class Repair
    {
        private int id;
        private Car car;
        private Mechanic mechanic;
        private string description;
        private decimal price;
        private DateTime date;

        public Repair(int id, Car car, Mechanic mechanic, string description, decimal price, DateTime date)
        {
            this.id = id;
            this.car = car;
            this.mechanic = mechanic;
            this.description = description;
            this.price = price;
            this.date = date;
        }

        public int Id
        {
            get { return id; }
        }

        public Car Car
        {
            get { return car; }
        }

        public Mechanic Mechanic
        {
            get { return mechanic; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                if (value >= 0)
                {
                    price = value;
                }
            }
        }

        public DateTime Date
        {
            get { return date; }
        }
    }
}

namespace CarServiceApp
{
    public class ServiceSystem
    {
        private Dictionary<int, Customer> customers;
        private Dictionary<int, Car> cars;
        private Dictionary<int, Mechanic> mechanics;
        private List<Repair> repairs;

        public ServiceSystem()
        {
            customers = new Dictionary<int, Customer>();
            cars = new Dictionary<int, Car>();
            mechanics = new Dictionary<int, Mechanic>();
            repairs = new List<Repair>();
        }

        public void AddCustomer(Customer customer)
        {
            if (!customers.ContainsKey(customer.Id))
            {
                customers.Add(customer.Id, customer);
                Console.WriteLine("Клиент добавлен.");
            }
            else
            {
                Console.WriteLine("Клиент с таким номером уже существует.");
            }
        }

        public void AddMechanic(Mechanic mechanic)
        {
            if (!mechanics.ContainsKey(mechanic.Id))
            {
                mechanics.Add(mechanic.Id, mechanic);
                Console.WriteLine("Мастер добавлен.");
            }
            else
            {
                Console.WriteLine("Мастер с таким номером уже существует.");
            }
        }

        public void AddCar(int id, string brand, string model, string number, int customerId)
        {
            if (!customers.ContainsKey(customerId))
            {
                Console.WriteLine("Клиент не найден.");
                return;
            }

            if (!cars.ContainsKey(id))
            {
                Car car = new Car(id, brand, model, number, customers[customerId]);
                cars.Add(id, car);
                Console.WriteLine("Автомобиль добавлен.");
            }
            else
            {
                Console.WriteLine("Автомобиль с таким номером уже существует.");
            }
        }

        public void AddRepair(int id, int carId, int mechanicId, string description, decimal price)
        {
            if (!cars.ContainsKey(carId))
            {
                Console.WriteLine("Автомобиль не найден.");
                return;
            }

            if (!mechanics.ContainsKey(mechanicId))
            {
                Console.WriteLine("Мастер не найден.");
                return;
            }

            Repair repair = new Repair(
                id,
                cars[carId],
                mechanics[mechanicId],
                description,
                price,
                DateTime.Now
            );

            repairs.Add(repair);
            Console.WriteLine("Ремонт добавлен.");
        }

        public void ShowCarInfo(int carId)
        {
            if (!cars.ContainsKey(carId))
            {
                Console.WriteLine("Автомобиль не найден.");
                return;
            }

            Car car = cars[carId];

            Console.WriteLine("Информация об автомобиле:");
            Console.WriteLine("Номер записи: " + car.Id);
            Console.WriteLine("Марка: " + car.Brand);
            Console.WriteLine("Модель: " + car.Model);
            Console.WriteLine("Госномер: " + car.Number);
            Console.WriteLine("Владелец: " + car.Owner.FullName);

            Console.WriteLine("Выполненные ремонты:");

            bool found = false;

            foreach (Repair repair in repairs)
            {
                if (repair.Car.Id == carId)
                {
                    Console.WriteLine("- " + repair.Description +
                                      ", мастер: " + repair.Mechanic.FullName +
                                      ", стоимость: " + repair.Price);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Ремонтов по автомобилю нет.");
            }
        }

        public void ShowCustomerInfo(int customerId)
        {
            if (!customers.ContainsKey(customerId))
            {
                Console.WriteLine("Клиент не найден.");
                return;
            }

            Customer customer = customers[customerId];

            Console.WriteLine("Информация о клиенте:");
            Console.WriteLine("Номер: " + customer.Id);
            Console.WriteLine("ФИО: " + customer.FullName);
            Console.WriteLine("Телефон: " + customer.Phone);

            Console.WriteLine("Автомобили клиента:");

            bool found = false;

            foreach (Car car in cars.Values)
            {
                if (car.Owner.Id == customerId)
                {
                    Console.WriteLine("- " + car.Brand + " " + car.Model +
                                      ", номер: " + car.Number);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Автомобилей у клиента нет.");
            }
        }

        public void ShowMechanicOrdersCount(int mechanicId)
        {
            if (!mechanics.ContainsKey(mechanicId))
            {
                Console.WriteLine("Мастер не найден.");
                return;
            }

            int count = 0;

            foreach (Repair repair in repairs)
            {
                if (repair.Mechanic.Id == mechanicId)
                {
                    count++;
                }
            }

            Console.WriteLine("Мастер: " + mechanics[mechanicId].FullName);
            Console.WriteLine("Количество обработанных заказов: " + count);
        }

        public void ShowAllMechanicsOrdersCount()
        {
            Console.WriteLine("Количество заказов по каждому мастеру:");

            foreach (Mechanic mechanic in mechanics.Values)
            {
                int count = 0;

                foreach (Repair repair in repairs)
                {
                    if (repair.Mechanic.Id == mechanic.Id)
                    {
                        count++;
                    }
                }

                Console.WriteLine("- " + mechanic.FullName + ": " + count);
            }
        }
    }
}

namespace CarServiceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceSystem system = new ServiceSystem();

            system.AddCustomer(new Customer(1, "Иванов Иван Иванович", "+79001234567"));
            system.AddCustomer(new Customer(2, "Петров Петр Петрович", "+79007654321"));

            system.AddMechanic(new Mechanic(1, "Сидоров Сергей Сергеевич", "Двигатель"));
            system.AddMechanic(new Mechanic(2, "Орлов Андрей Павлович", "Ходовая часть"));

            system.AddCar(1, "Toyota", "Camry", "А123ВС", 1);
            system.AddCar(2, "Kia", "Rio", "В456ОР", 2);

            system.AddRepair(1, 1, 1, "Замена масла", 4500);
            system.AddRepair(2, 1, 2, "Диагностика подвески", 2500);
            system.AddRepair(3, 2, 1, "Замена свечей", 3000);

            int menu;

            do
            {
                Console.WriteLine();
                Console.WriteLine("ГЛАВНОЕ МЕНЮ");
                Console.WriteLine("1 - добавить клиента");
                Console.WriteLine("2 - добавить мастера");
                Console.WriteLine("3 - добавить автомобиль");
                Console.WriteLine("4 - добавить выполненный ремонт");
                Console.WriteLine("5 - показать информацию об автомобиле");
                Console.WriteLine("6 - показать информацию о клиенте");
                Console.WriteLine("7 - количество заказов у мастера");
                Console.WriteLine("8 - количество заказов по всем мастерам");
                Console.WriteLine("0 - выход");
                Console.Write("Выберите действие: ");

                menu = int.Parse(Console.ReadLine());

                switch (menu)
                {
                    case 1:
                        AddCustomerFromConsole(system);
                        break;

                    case 2:
                        AddMechanicFromConsole(system);
                        break;

                    case 3:
                        AddCarFromConsole(system);
                        break;

                    case 4:
                        AddRepairFromConsole(system);
                        break;

                    case 5:
                        ShowCarFromConsole(system);
                        break;

                    case 6:
                        ShowCustomerFromConsole(system);
                        break;

                    case 7:
                        ShowMechanicCountFromConsole(system);
                        break;

                    case 8:
                        system.ShowAllMechanicsOrdersCount();
                        break;

                    case 0:
                        Console.WriteLine("Работа программы завершена.");
                        break;

                    default:
                        Console.WriteLine("Неверный пункт меню.");
                        break;
                }

            } while (menu != 0);
        }

        static void AddCustomerFromConsole(ServiceSystem system)
        {
            Console.Write("Введите номер клиента: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите ФИО клиента: ");
            string fullName = Console.ReadLine();

            Console.Write("Введите телефон клиента: ");
            string phone = Console.ReadLine();

            system.AddCustomer(new Customer(id, fullName, phone));
        }

        static void AddMechanicFromConsole(ServiceSystem system)
        {
            Console.Write("Введите номер мастера: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите ФИО мастера: ");
            string fullName = Console.ReadLine();

            Console.Write("Введите специализацию: ");
            string specialization = Console.ReadLine();

            system.AddMechanic(new Mechanic(id, fullName, specialization));
        }

        static void AddCarFromConsole(ServiceSystem system)
        {
            Console.Write("Введите номер автомобиля: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите марку автомобиля: ");
            string brand = Console.ReadLine();

            Console.Write("Введите модель автомобиля: ");
            string model = Console.ReadLine();

            Console.Write("Введите госномер автомобиля: ");
            string number = Console.ReadLine();

            Console.Write("Введите номер владельца: ");
            int customerId = int.Parse(Console.ReadLine());

            system.AddCar(id, brand, model, number, customerId);
        }

        static void AddRepairFromConsole(ServiceSystem system)
        {
            Console.Write("Введите номер ремонта: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите номер автомобиля: ");
            int carId = int.Parse(Console.ReadLine());

            Console.Write("Введите номер мастера: ");
            int mechanicId = int.Parse(Console.ReadLine());

            Console.Write("Введите описание ремонта: ");
            string description = Console.ReadLine();

            Console.Write("Введите стоимость ремонта: ");
            decimal price = decimal.Parse(Console.ReadLine());

            system.AddRepair(id, carId, mechanicId, description, price);
        }

        static void ShowCarFromConsole(ServiceSystem system)
        {
            Console.Write("Введите номер автомобиля: ");
            int carId = int.Parse(Console.ReadLine());

            system.ShowCarInfo(carId);
        }

        static void ShowCustomerFromConsole(ServiceSystem system)
        {
            Console.Write("Введите номер клиента: ");
            int customerId = int.Parse(Console.ReadLine());

            system.ShowCustomerInfo(customerId);
        }

        static void ShowMechanicCountFromConsole(ServiceSystem system)
        {
            Console.Write("Введите номер мастера: ");
            int mechanicId = int.Parse(Console.ReadLine());

            system.ShowMechanicOrdersCount(mechanicId);
        }
    }
}