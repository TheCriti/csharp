using System;
using System.Collections.Generic;

namespace AirlineApp
{
    public class Flight
    {
        private int id;
        private string number;
        private string destination;

        public Flight(int id, string number, string destination)
        {
            this.id = id;
            this.number = number;
            this.destination = destination;
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

        public string Destination
        {
            get { return destination; }
            set { destination = value; }
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

    public class Departure
    {
        private int id;
        private Flight flight;
        private Airport airport;
        private DateTime departureTime;

        public Departure(int id, Flight flight, Airport airport, DateTime departureTime)
        {
            this.id = id;
            this.flight = flight;
            this.airport = airport;
            this.departureTime = departureTime;
        }

        public int Id
        {
            get { return id; }
        }

        public Flight Flight
        {
            get { return flight; }
        }

        public Airport Airport
        {
            get { return airport; }
        }

        public DateTime DepartureTime
        {
            get { return departureTime; }
            set { departureTime = value; }
        }
    }

    public class Arrival
    {
        private int id;
        private Flight flight;
        private Airport airport;
        private DateTime arrivalTime;

        public Arrival(int id, Flight flight, Airport airport, DateTime arrivalTime)
        {
            this.id = id;
            this.flight = flight;
            this.airport = airport;
            this.arrivalTime = arrivalTime;
        }

        public int Id
        {
            get { return id; }
        }

        public Flight Flight
        {
            get { return flight; }
        }

        public Airport Airport
        {
            get { return airport; }
        }

        public DateTime ArrivalTime
        {
            get { return arrivalTime; }
            set { arrivalTime = value; }
        }
    }
}

namespace AirlineApp
{
    public class AirlineSystem
    {
        private Dictionary<int, Flight> flights;
        private Dictionary<int, Airport> airports;
        private List<Departure> departures;
        private List<Arrival> arrivals;

        public AirlineSystem()
        {
            flights = new Dictionary<int, Flight>();
            airports = new Dictionary<int, Airport>();
            departures = new List<Departure>();
            arrivals = new List<Arrival>();
        }

        public void AddFlight(Flight flight)
        {
            if (!flights.ContainsKey(flight.Id))
            {
                flights.Add(flight.Id, flight);
                Console.WriteLine("Рейс добавлен.");
            }
            else
            {
                Console.WriteLine("Рейс с таким номером уже существует.");
            }
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

        public void AddDeparture(int id, int flightId, int airportId, DateTime departureTime)
        {
            if (!flights.ContainsKey(flightId))
            {
                Console.WriteLine("Рейс не найден.");
                return;
            }

            if (!airports.ContainsKey(airportId))
            {
                Console.WriteLine("Аэропорт не найден.");
                return;
            }

            Departure departure = new Departure(
                id,
                flights[flightId],
                airports[airportId],
                departureTime
            );

            departures.Add(departure);
            Console.WriteLine("Отправление добавлено.");
        }

        public void AddArrival(int id, int flightId, int airportId, DateTime arrivalTime)
        {
            if (!flights.ContainsKey(flightId))
            {
                Console.WriteLine("Рейс не найден.");
                return;
            }

            if (!airports.ContainsKey(airportId))
            {
                Console.WriteLine("Аэропорт не найден.");
                return;
            }

            Arrival arrival = new Arrival(
                id,
                flights[flightId],
                airports[airportId],
                arrivalTime
            );

            arrivals.Add(arrival);
            Console.WriteLine("Прибытие добавлено.");
        }

        public void EditFlight(int flightId, string newNumber, string newDestination)
        {
            if (!flights.ContainsKey(flightId))
            {
                Console.WriteLine("Рейс не найден.");
                return;
            }

            flights[flightId].Number = newNumber;
            flights[flightId].Destination = newDestination;

            Console.WriteLine("Данные рейса изменены.");
        }

        public void EditAirport(int airportId, string newName, string newCity)
        {
            if (!airports.ContainsKey(airportId))
            {
                Console.WriteLine("Аэропорт не найден.");
                return;
            }

            airports[airportId].Name = newName;
            airports[airportId].City = newCity;

            Console.WriteLine("Данные аэропорта изменены.");
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
            Console.WriteLine("Направление: " + flight.Destination);
        }

        public void ShowDepartedNotArrivedFlights()
        {
            Console.WriteLine("Вылетевшие, но не прибывшие рейсы:");

            bool found = false;

            foreach (Departure departure in departures)
            {
                bool arrived = false;

                foreach (Arrival arrival in arrivals)
                {
                    if (arrival.Flight.Id == departure.Flight.Id)
                    {
                        arrived = true;
                        break;
                    }
                }

                if (!arrived)
                {
                    Console.WriteLine(
                        "- рейс " + departure.Flight.Number +
                        ", направление: " + departure.Flight.Destination +
                        ", аэропорт отправления: " + departure.Airport.Name +
                        ", время отправления: " + departure.DepartureTime
                    );

                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Таких рейсов нет.");
            }
        }
    }
}

namespace AirlineApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AirlineSystem system = new AirlineSystem();

            system.AddAirport(new Airport(1, "Пулково", "Санкт-Петербург"));
            system.AddAirport(new Airport(2, "Шереметьево", "Москва"));
            system.AddAirport(new Airport(3, "Сочи", "Сочи"));

            system.AddFlight(new Flight(1, "SU-101", "Санкт-Петербург — Москва"));
            system.AddFlight(new Flight(2, "DP-320", "Санкт-Петербург — Сочи"));
            system.AddFlight(new Flight(3, "U6-777", "Москва — Санкт-Петербург"));

            system.AddDeparture(1, 1, 1, DateTime.Now.AddHours(-2));
            system.AddDeparture(2, 2, 1, DateTime.Now.AddHours(-1));
            system.AddArrival(1, 1, 2, DateTime.Now.AddMinutes(-30));

            int menu;

            do
            {
                Console.WriteLine();
                Console.WriteLine("ГЛАВНОЕ МЕНЮ");
                Console.WriteLine("1 - добавить рейс");
                Console.WriteLine("2 - добавить аэропорт");
                Console.WriteLine("3 - добавить отправление");
                Console.WriteLine("4 - добавить прибытие");
                Console.WriteLine("5 - редактировать рейс");
                Console.WriteLine("6 - редактировать аэропорт");
                Console.WriteLine("7 - показать информацию о рейсе");
                Console.WriteLine("8 - показать вылетевшие, но не прибывшие рейсы");
                Console.WriteLine("0 - выход");
                Console.Write("Выберите действие: ");

                menu = int.Parse(Console.ReadLine());

                switch (menu)
                {
                    case 1:
                        AddFlightFromConsole(system);
                        break;

                    case 2:
                        AddAirportFromConsole(system);
                        break;

                    case 3:
                        AddDepartureFromConsole(system);
                        break;

                    case 4:
                        AddArrivalFromConsole(system);
                        break;

                    case 5:
                        EditFlightFromConsole(system);
                        break;

                    case 6:
                        EditAirportFromConsole(system);
                        break;

                    case 7:
                        ShowFlightFromConsole(system);
                        break;

                    case 8:
                        system.ShowDepartedNotArrivedFlights();
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

        static void AddFlightFromConsole(AirlineSystem system)
        {
            Console.Write("Введите номер записи рейса: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите номер рейса: ");
            string number = Console.ReadLine();

            Console.Write("Введите направление рейса: ");
            string destination = Console.ReadLine();

            system.AddFlight(new Flight(id, number, destination));
        }

        static void AddAirportFromConsole(AirlineSystem system)
        {
            Console.Write("Введите номер аэропорта: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите название аэропорта: ");
            string name = Console.ReadLine();

            Console.Write("Введите город: ");
            string city = Console.ReadLine();

            system.AddAirport(new Airport(id, name, city));
        }

        static void AddDepartureFromConsole(AirlineSystem system)
        {
            Console.Write("Введите номер записи отправления: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите номер рейса: ");
            int flightId = int.Parse(Console.ReadLine());

            Console.Write("Введите номер аэропорта отправления: ");
            int airportId = int.Parse(Console.ReadLine());

            system.AddDeparture(id, flightId, airportId, DateTime.Now);
        }

        static void AddArrivalFromConsole(AirlineSystem system)
        {
            Console.Write("Введите номер записи прибытия: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите номер рейса: ");
            int flightId = int.Parse(Console.ReadLine());

            Console.Write("Введите номер аэропорта прибытия: ");
            int airportId = int.Parse(Console.ReadLine());

            system.AddArrival(id, flightId, airportId, DateTime.Now);
        }

        static void EditFlightFromConsole(AirlineSystem system)
        {
            Console.Write("Введите номер записи рейса: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите новый номер рейса: ");
            string number = Console.ReadLine();

            Console.Write("Введите новое направление: ");
            string destination = Console.ReadLine();

            system.EditFlight(id, number, destination);
        }

        static void EditAirportFromConsole(AirlineSystem system)
        {
            Console.Write("Введите номер аэропорта: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите новое название аэропорта: ");
            string name = Console.ReadLine();

            Console.Write("Введите новый город: ");
            string city = Console.ReadLine();

            system.EditAirport(id, name, city);
        }

        static void ShowFlightFromConsole(AirlineSystem system)
        {
            Console.Write("Введите номер записи рейса: ");
            int id = int.Parse(Console.ReadLine());

            system.ShowFlightInfo(id);
        }
    }
}