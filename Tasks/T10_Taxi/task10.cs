using System;
using System.Collections.Generic;

namespace TaxiApp
{
    public class Car
    {
        private int id;
        private string brand;
        private string model;
        private string number;

        public Car(int id, string brand, string model, string number)
        {
            this.id = id;
            this.brand = brand;
            this.model = model;
            this.number = number;
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
    }

    public class Driver
    {
        private int id;
        private string fullName;
        private Car car;

        public Driver(int id, string fullName, Car car)
        {
            this.id = id;
            this.fullName = fullName;
            this.car = car;
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

        public Car Car
        {
            get { return car; }
            set { car = value; }
        }
    }

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

    public class Custom
    {
        private int id;
        private Driver driver;
        private Customer customer;
        private string fromAddress;
        private string toAddress;
        private decimal price;
        private DateTime date;

        public Custom(int id, Driver driver, Customer customer, string fromAddress, string toAddress, decimal price, DateTime date)
        {
            this.id = id;
            this.driver = driver;
            this.customer = customer;
            this.fromAddress = fromAddress;
            this.toAddress = toAddress;
            this.price = price;
            this.date = date;
        }

        public int Id
        {
            get { return id; }
        }

        public Driver Driver
        {
            get { return driver; }
        }

        public Customer Customer
        {
            get { return customer; }
        }

        public string FromAddress
        {
            get { return fromAddress; }
            set { fromAddress = value; }
        }

        public string ToAddress
        {
            get { return toAddress; }
            set { toAddress = value; }
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
            set { date = value; }
        }
    }
}

namespace TaxiApp
{
    public class TaxiSystem
    {
        private Dictionary<int, Car> cars;
        private Dictionary<int, Driver> drivers;
        private Dictionary<int, Customer> customers;
        private List<Custom> customs;

        public TaxiSystem()
        {
            cars = new Dictionary<int, Car>();
            drivers = new Dictionary<int, Driver>();
            customers = new Dictionary<int, Customer>();
            customs = new List<Custom>();
        }

        public void AddCar(Car car)
        {
            if (!cars.ContainsKey(car.Id))
            {
                cars.Add(car.Id, car);
                Console.WriteLine("Автомобиль добавлен.");
            }
            else
            {
                Console.WriteLine("Автомобиль с таким номером уже существует.");
            }
        }

        public void AddDriver(int id, string fullName, int carId)
        {
            if (!cars.ContainsKey(carId))
            {
                Console.WriteLine("Автомобиль не найден.");
                return;
            }

            if (!drivers.ContainsKey(id))
            {
                Driver driver = new Driver(id, fullName, cars[carId]);
                drivers.Add(id, driver);
                Console.WriteLine("Водитель добавлен.");
            }
            else
            {
                Console.WriteLine("Водитель с таким номером уже существует.");
            }
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

        public void AddCustom(int id, int driverId, int customerId, string fromAddress, string toAddress, decimal price, DateTime date)
        {
            if (!drivers.ContainsKey(driverId))
            {
                Console.WriteLine("Водитель не найден.");
                return;
            }

            if (!customers.ContainsKey(customerId))
            {
                Console.WriteLine("Клиент не найден.");
                return;
            }

            Custom custom = new Custom(
                id,
                drivers[driverId],
                customers[customerId],
                fromAddress,
                toAddress,
                price,
                date
            );

            customs.Add(custom);
            Console.WriteLine("Заказ добавлен.");
        }

        public void EditCar(int carId, string newBrand, string newModel, string newNumber)
        {
            if (!cars.ContainsKey(carId))
            {
                Console.WriteLine("Автомобиль не найден.");
                return;
            }

            cars[carId].Brand = newBrand;
            cars[carId].Model = newModel;
            cars[carId].Number = newNumber;

            Console.WriteLine("Данные автомобиля изменены.");
        }

        public void EditDriver(int driverId, string newFullName, int newCarId)
        {
            if (!drivers.ContainsKey(driverId))
            {
                Console.WriteLine("Водитель не найден.");
                return;
            }

            if (!cars.ContainsKey(newCarId))
            {
                Console.WriteLine("Автомобиль не найден.");
                return;
            }

            drivers[driverId].FullName = newFullName;
            drivers[driverId].Car = cars[newCarId];

            Console.WriteLine("Данные водителя изменены.");
        }

        public void EditCustomer(int customerId, string newFullName, string newPhone)
        {
            if (!customers.ContainsKey(customerId))
            {
                Console.WriteLine("Клиент не найден.");
                return;
            }

            customers[customerId].FullName = newFullName;
            customers[customerId].Phone = newPhone;

            Console.WriteLine("Данные клиента изменены.");
        }

        public void ShowCustomInfo(int customId)
        {
            foreach (Custom custom in customs)
            {
                if (custom.Id == customId)
                {
                    Console.WriteLine("Информация о заказе:");
                    Console.WriteLine("Номер заказа: " + custom.Id);
                    Console.WriteLine("Водитель: " + custom.Driver.FullName);
                    Console.WriteLine("Автомобиль: " + custom.Driver.Car.Brand + " " + custom.Driver.Car.Model);
                    Console.WriteLine("Клиент: " + custom.Customer.FullName);
                    Console.WriteLine("Откуда: " + custom.FromAddress);
                    Console.WriteLine("Куда: " + custom.ToAddress);
                    Console.WriteLine("Стоимость: " + custom.Price);
                    Console.WriteLine("Дата: " + custom.Date.ToShortDateString());
                    return;
                }
            }

            Console.WriteLine("Заказ не найден.");
        }

        public void ShowOrdersCountByDate(DateTime date)
        {
            int count = 0;

            foreach (Custom custom in customs)
            {
                if (custom.Date.Date == date.Date)
                {
                    count++;
                }
            }

            Console.WriteLine("Количество заказов за " + date.ToShortDateString() + ": " + count);
        }

        public void ShowDriverInfo(int driverId)
        {
            if (!drivers.ContainsKey(driverId))
            {
                Console.WriteLine("Водитель не найден.");
                return;
            }

            Driver driver = drivers[driverId];

            Console.WriteLine("Информация о водителе:");
            Console.WriteLine("Номер: " + driver.Id);
            Console.WriteLine("ФИО: " + driver.FullName);
            Console.WriteLine("Автомобиль: " + driver.Car.Brand + " " + driver.Car.Model);
            Console.WriteLine("Госномер: " + driver.Car.Number);
        }
    }
}

namespace TaxiApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TaxiSystem system = new TaxiSystem();

            system.AddCar(new Car(1, "Hyundai", "Solaris", "А123ВС"));
            system.AddCar(new Car(2, "Kia", "Rio", "В456ОР"));

            system.AddDriver(1, "Иванов Иван Иванович", 1);
            system.AddDriver(2, "Петров Петр Петрович", 2);

            system.AddCustomer(new Customer(1, "Сидоров Сергей Сергеевич", "+79001234567"));
            system.AddCustomer(new Customer(2, "Орлова Анна Павловна", "+79007654321"));

            system.AddCustom(1, 1, 1, "Невский проспект, 10", "Пулково", 1200, DateTime.Today);
            system.AddCustom(2, 2, 2, "Московский вокзал", "Лиговский проспект, 50", 650, DateTime.Today);
            system.AddCustom(3, 1, 2, "Садовая, 5", "Петроградская, 15", 800, DateTime.Today.AddDays(-1));

            int menu;

            do
            {
                Console.WriteLine();
                Console.WriteLine("ГЛАВНОЕ МЕНЮ");
                Console.WriteLine("1 - добавить автомобиль");
                Console.WriteLine("2 - добавить водителя");
                Console.WriteLine("3 - добавить клиента");
                Console.WriteLine("4 - добавить заказ");
                Console.WriteLine("5 - редактировать автомобиль");
                Console.WriteLine("6 - редактировать водителя");
                Console.WriteLine("7 - редактировать клиента");
                Console.WriteLine("8 - показать информацию о заказе");
                Console.WriteLine("9 - показать информацию о водителе");
                Console.WriteLine("10 - количество заказов за день");
                Console.WriteLine("0 - выход");
                Console.Write("Выберите действие: ");

                menu = int.Parse(Console.ReadLine());

                switch (menu)
                {
                    case 1:
                        AddCarFromConsole(system);
                        break;

                    case 2:
                        AddDriverFromConsole(system);
                        break;

                    case 3:
                        AddCustomerFromConsole(system);
                        break;

                    case 4:
                        AddCustomFromConsole(system);
                        break;

                    case 5:
                        EditCarFromConsole(system);
                        break;

                    case 6:
                        EditDriverFromConsole(system);
                        break;

                    case 7:
                        EditCustomerFromConsole(system);
                        break;

                    case 8:
                        ShowCustomFromConsole(system);
                        break;

                    case 9:
                        ShowDriverFromConsole(system);
                        break;

                    case 10:
                        ShowOrdersCountFromConsole(system);
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

        static void AddCarFromConsole(TaxiSystem system)
        {
            Console.Write("Введите номер автомобиля: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите марку автомобиля: ");
            string brand = Console.ReadLine();

            Console.Write("Введите модель автомобиля: ");
            string model = Console.ReadLine();

            Console.Write("Введите госномер: ");
            string number = Console.ReadLine();

            system.AddCar(new Car(id, brand, model, number));
        }

        static void AddDriverFromConsole(TaxiSystem system)
        {
            Console.Write("Введите номер водителя: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите ФИО водителя: ");
            string fullName = Console.ReadLine();

            Console.Write("Введите номер автомобиля: ");
            int carId = int.Parse(Console.ReadLine());

            system.AddDriver(id, fullName, carId);
        }

        static void AddCustomerFromConsole(TaxiSystem system)
        {
            Console.Write("Введите номер клиента: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите ФИО клиента: ");
            string fullName = Console.ReadLine();

            Console.Write("Введите телефон клиента: ");
            string phone = Console.ReadLine();

            system.AddCustomer(new Customer(id, fullName, phone));
        }

        static void AddCustomFromConsole(TaxiSystem system)
        {
            Console.Write("Введите номер заказа: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите номер водителя: ");
            int driverId = int.Parse(Console.ReadLine());

            Console.Write("Введите номер клиента: ");
            int customerId = int.Parse(Console.ReadLine());

            Console.Write("Введите адрес отправления: ");
            string fromAddress = Console.ReadLine();

            Console.Write("Введите адрес назначения: ");
            string toAddress = Console.ReadLine();

            Console.Write("Введите стоимость заказа: ");
            decimal price = decimal.Parse(Console.ReadLine());

            system.AddCustom(id, driverId, customerId, fromAddress, toAddress, price, DateTime.Now);
        }

        static void EditCarFromConsole(TaxiSystem system)
        {
            Console.Write("Введите номер автомобиля: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите новую марку: ");
            string brand = Console.ReadLine();

            Console.Write("Введите новую модель: ");
            string model = Console.ReadLine();

            Console.Write("Введите новый госномер: ");
            string number = Console.ReadLine();

            system.EditCar(id, brand, model, number);
        }

        static void EditDriverFromConsole(TaxiSystem system)
        {
            Console.Write("Введите номер водителя: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите новое ФИО водителя: ");
            string fullName = Console.ReadLine();

            Console.Write("Введите новый номер автомобиля: ");
            int carId = int.Parse(Console.ReadLine());

            system.EditDriver(id, fullName, carId);
        }

        static void EditCustomerFromConsole(TaxiSystem system)
        {
            Console.Write("Введите номер клиента: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите новое ФИО клиента: ");
            string fullName = Console.ReadLine();

            Console.Write("Введите новый телефон клиента: ");
            string phone = Console.ReadLine();

            system.EditCustomer(id, fullName, phone);
        }

        static void ShowCustomFromConsole(TaxiSystem system)
        {
            Console.Write("Введите номер заказа: ");
            int id = int.Parse(Console.ReadLine());

            system.ShowCustomInfo(id);
        }

        static void ShowDriverFromConsole(TaxiSystem system)
        {
            Console.Write("Введите номер водителя: ");
            int id = int.Parse(Console.ReadLine());

            system.ShowDriverInfo(id);
        }

        static void ShowOrdersCountFromConsole(TaxiSystem system)
        {
            Console.Write("Введите год: ");
            int year = int.Parse(Console.ReadLine());

            Console.Write("Введите месяц: ");
            int month = int.Parse(Console.ReadLine());

            Console.Write("Введите день: ");
            int day = int.Parse(Console.ReadLine());

            DateTime date = new DateTime(year, month, day);
            system.ShowOrdersCountByDate(date);
        }
    }
}