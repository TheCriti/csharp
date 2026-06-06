using System;
using System.Collections.Generic;

namespace TicketApp
{
    public class Flight
    {
        private int id;
        private string number;
        private Airport departureAirport;
        private Airport arrivalAirport;

        public Flight(int id, string number, Airport departureAirport, Airport arrivalAirport)
        {
            this.id = id;
            this.number = number;
            this.departureAirport = departureAirport;
            this.arrivalAirport = arrivalAirport;
        }

        public int Id
        {
            get { return id; }
        }

        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        public Airport DepartureAirport
        {
            get { return departureAirport; }
            set { departureAirport = value; }
        }

        public Airport ArrivalAirport
        {
            get { return arrivalAirport; }
            set { arrivalAirport = value; }
        }
    }

    public class Airport
    {
        private int id;
        private string name;
        private string city;

        public Airport(int id, string name, string city)
        {
            this.id = id;
            this.name = name;
            this.city = city;
        }

        public int Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }
    }

    public class Customer
    {
        private int id;
        private string fullName;
        private string passport;

        public Customer(int id, string fullName, string passport)
        {
            this.id = id;
            this.fullName = fullName;
            this.passport = passport;
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

        public string Passport
        {
            get { return passport; }
            set { passport = value; }
        }
    }

    public class Ticket
    {
        private int id;
        private Flight flight;
        private Customer customer;
        private decimal price;
        private DateTime saleDate;

        public Ticket(int id, Flight flight, Customer customer, decimal price, DateTime saleDate)
        {
            this.id = id;
            this.flight = flight;
            this.customer = customer;
            this.price = price;
            this.saleDate = saleDate;
        }

        public int Id
        {
            get { return id; }
        }

        public Flight Flight
        {
            get { return flight; }
        }

        public Customer Customer
        {
            get { return customer; }
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

        public DateTime SaleDate
        {
            get { return saleDate; }
        }
    }
}

namespace TicketApp
{
    public class TicketSystem
    {
        private Dictionary<int, Flight> flights;
        private Dictionary<int, Airport> airports;
        private Dictionary<int, Customer> customers;
        private List<Ticket> tickets;

        public TicketSystem()
        {
            flights = new Dictionary<int, Flight>();
            airports = new Dictionary<int, Airport>();
            customers = new Dictionary<int, Customer>();
            tickets = new List<Ticket>();
        }

        public void AddAirport(Airport airport)
        {
            if (!airports.ContainsKey(airport.Id))
            {
                airports.Add(airport.Id, airport);
                Console.WriteLine("Аэропорт добавлен.");
            }
            else
            {
                Console.WriteLine("Аэропорт с таким номером уже существует.");
            }
        }

        public void AddFlight(int id, string number, int departureAirportId, int arrivalAirportId)
        {
            if (!airports.ContainsKey(departureAirportId))
            {
                Console.WriteLine("Аэропорт отправления не найден.");
                return;
            }

            if (!airports.ContainsKey(arrivalAirportId))
            {
                Console.WriteLine("Аэропорт прибытия не найден.");
                return;
            }

            if (!flights.ContainsKey(id))
            {
                Flight flight = new Flight(
                    id,
                    number,
                    airports[departureAirportId],
                    airports[arrivalAirportId]
                );

                flights.Add(id, flight);
                Console.WriteLine("Рейс добавлен.");
            }
            else
            {
                Console.WriteLine("Рейс с таким номером уже существует.");
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

        public void SellTicket(int ticketId, int flightId, int customerId, decimal price)
        {
            if (!flights.ContainsKey(flightId))
            {
                Console.WriteLine("Рейс не найден.");
                return;
            }

            if (!customers.ContainsKey(customerId))
            {
                Console.WriteLine("Клиент не найден.");
                return;
            }

            Ticket ticket = new Ticket(
                ticketId,
                flights[flightId],
                customers[customerId],
                price,
                DateTime.Now
            );

            tickets.Add(ticket);
            Console.WriteLine("Билет продан.");
        }

        public void ShowTicketInfo(int ticketId)
        {
            foreach (Ticket ticket in tickets)
            {
                if (ticket.Id == ticketId)
                {
                    Console.WriteLine("Информация о билете:");
                    Console.WriteLine("Номер билета: " + ticket.Id);
                    Console.WriteLine("Рейс: " + ticket.Flight.Number);
                    Console.WriteLine("Откуда: " + ticket.Flight.DepartureAirport.City);
                    Console.WriteLine("Куда: " + ticket.Flight.ArrivalAirport.City);
                    Console.WriteLine("Клиент: " + ticket.Customer.FullName);
                    Console.WriteLine("Цена: " + ticket.Price);
                    Console.WriteLine("Дата продажи: " + ticket.SaleDate.ToShortDateString());
                    return;
                }
            }

            Console.WriteLine("Билет не найден.");
        }

        public void ShowRevenue()
        {
            decimal total = 0;

            foreach (Ticket ticket in tickets)
            {
                total += ticket.Price;
            }

            Console.WriteLine("Общая сумма выручки: " + total);
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
            Console.WriteLine("Паспорт: " + customer.Passport);

            Console.WriteLine("Купленные билеты:");

            bool hasTickets = false;

            foreach (Ticket ticket in tickets)
            {
                if (ticket.Customer.Id == customerId)
                {
                    Console.WriteLine("- билет №" + ticket.Id + ", рейс " +
                                      ticket.Flight.Number + ", цена: " + ticket.Price);
                    hasTickets = true;
                }
            }

            if (!hasTickets)
            {
                Console.WriteLine("У клиента нет купленных билетов.");
            }
        }

        public void ShowFlightInfo(int flightId)
        {
            if (!flights.ContainsKey(flightId))
            {
                Console.WriteLine("Рейс не найден.");
                return;
            }

            Flight flight = flights[flightId];

            Console.WriteLine("Информация о рейсе:");
            Console.WriteLine("Номер записи: " + flight.Id);
            Console.WriteLine("Номер рейса: " + flight.Number);
            Console.WriteLine("Аэропорт отправления: " + flight.DepartureAirport.Name);
            Console.WriteLine("Аэропорт прибытия: " + flight.ArrivalAirport.Name);
        }
    }
}

namespace TicketApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TicketSystem system = new TicketSystem();

            system.AddAirport(new Airport(1, "Пулково", "Санкт-Петербург"));
            system.AddAirport(new Airport(2, "Шереметьево", "Москва"));
            system.AddAirport(new Airport(3, "Адлер", "Сочи"));

            system.AddFlight(1, "SU-101", 1, 2);
            system.AddFlight(2, "DP-320", 1, 3);

            system.AddCustomer(new Customer(1, "Иванов Иван Иванович", "4012 123456"));
            system.AddCustomer(new Customer(2, "Петров Петр Петрович", "4013 654321"));

            system.SellTicket(1, 1, 1, 8500);
            system.SellTicket(2, 2, 2, 12500);

            int menu;

            do
            {
                Console.WriteLine();
                Console.WriteLine("ГЛАВНОЕ МЕНЮ");
                Console.WriteLine("1 - добавить аэропорт");
                Console.WriteLine("2 - добавить рейс");
                Console.WriteLine("3 - добавить клиента");
                Console.WriteLine("4 - оформить продажу билета");
                Console.WriteLine("5 - показать информацию о билете");
                Console.WriteLine("6 - показать информацию о клиенте");
                Console.WriteLine("7 - показать информацию о рейсе");
                Console.WriteLine("8 - показать сумму выручки");
                Console.WriteLine("0 - выход");
                Console.Write("Выберите действие: ");

                menu = int.Parse(Console.ReadLine());

                switch (menu)
                {
                    case 1:
                        AddAirportFromConsole(system);
                        break;

                    case 2:
                        AddFlightFromConsole(system);
                        break;

                    case 3:
                        AddCustomerFromConsole(system);
                        break;

                    case 4:
                        SellTicketFromConsole(system);
                        break;

                    case 5:
                        ShowTicketFromConsole(system);
                        break;

                    case 6:
                        ShowCustomerFromConsole(system);
                        break;

                    case 7:
                        ShowFlightFromConsole(system);
                        break;

                    case 8:
                        system.ShowRevenue();
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

        static void AddAirportFromConsole(TicketSystem system)
        {
            Console.Write("Введите номер аэропорта: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите название аэропорта: ");
            string name = Console.ReadLine();

            Console.Write("Введите город: ");
            string city = Console.ReadLine();

            system.AddAirport(new Airport(id, name, city));
        }

        static void AddFlightFromConsole(TicketSystem system)
        {
            Console.Write("Введите номер записи рейса: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите номер рейса: ");
            string number = Console.ReadLine();

            Console.Write("Введите номер аэропорта отправления: ");
            int departureAirportId = int.Parse(Console.ReadLine());

            Console.Write("Введите номер аэропорта прибытия: ");
            int arrivalAirportId = int.Parse(Console.ReadLine());

            system.AddFlight(id, number, departureAirportId, arrivalAirportId);
        }

        static void AddCustomerFromConsole(TicketSystem system)
        {
            Console.Write("Введите номер клиента: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите ФИО клиента: ");
            string fullName = Console.ReadLine();

            Console.Write("Введите паспортные данные: ");
            string passport = Console.ReadLine();

            system.AddCustomer(new Customer(id, fullName, passport));
        }

        static void SellTicketFromConsole(TicketSystem system)
        {
            Console.Write("Введите номер билета: ");
            int ticketId = int.Parse(Console.ReadLine());

            Console.Write("Введите номер рейса: ");
            int flightId = int.Parse(Console.ReadLine());

            Console.Write("Введите номер клиента: ");
            int customerId = int.Parse(Console.ReadLine());

            Console.Write("Введите цену билета: ");
            decimal price = decimal.Parse(Console.ReadLine());

            system.SellTicket(ticketId, flightId, customerId, price);
        }

        static void ShowTicketFromConsole(TicketSystem system)
        {
            Console.Write("Введите номер билета: ");
            int ticketId = int.Parse(Console.ReadLine());

            system.ShowTicketInfo(ticketId);
        }

        static void ShowCustomerFromConsole(TicketSystem system)
        {
            Console.Write("Введите номер клиента: ");
            int customerId = int.Parse(Console.ReadLine());

            system.ShowCustomerInfo(customerId);
        }

        static void ShowFlightFromConsole(TicketSystem system)
        {
            Console.Write("Введите номер рейса: ");
            int flightId = int.Parse(Console.ReadLine());

            system.ShowFlightInfo(flightId);
        }
    }
}