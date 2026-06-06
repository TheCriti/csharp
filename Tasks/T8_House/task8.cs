using System;
using System.Collections.Generic;

namespace HouseApp
{
    public class Person
    {
        private int id;
        private string fullName;
        private int age;

        public Person(int id, string fullName, int age)
        {
            this.id = id;
            this.fullName = fullName;
            this.age = age;
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

        public int Age
        {
            get { return age; }
            set
            {
                if (value > 0)
                {
                    age = value;
                }
            }
        }
    }

    public class Flat
    {
        private int id;
        private int number;
        private double area;

        public Flat(int id, int number, double area)
        {
            this.id = id;
            this.number = number;
            this.area = area;
        }

        public int Id
        {
            get { return id; }
        }

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        public double Area
        {
            get { return area; }
            set
            {
                if (value > 0)
                {
                    area = value;
                }
            }
        }
    }

    public class Setting
    {
        private int id;
        private Person person;
        private Flat flat;
        private DateTime moveInDate;
        private DateTime? moveOutDate;

        public Setting(int id, Person person, Flat flat, DateTime moveInDate)
        {
            this.id = id;
            this.person = person;
            this.flat = flat;
            this.moveInDate = moveInDate;
            this.moveOutDate = null;
        }

        public int Id
        {
            get { return id; }
        }

        public Person Person
        {
            get { return person; }
        }

        public Flat Flat
        {
            get { return flat; }
        }

        public DateTime MoveInDate
        {
            get { return moveInDate; }
        }

        public DateTime? MoveOutDate
        {
            get { return moveOutDate; }
            set { moveOutDate = value; }
        }

        public bool IsActive
        {
            get { return moveOutDate == null; }
        }
    }

    public class Bill
    {
        private int id;
        private Flat flat;
        private string month;
        private decimal amount;

        public Bill(int id, Flat flat, string month, decimal amount)
        {
            this.id = id;
            this.flat = flat;
            this.month = month;
            this.amount = amount;
        }

        public int Id
        {
            get { return id; }
        }

        public Flat Flat
        {
            get { return flat; }
        }

        public string Month
        {
            get { return month; }
            set { month = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set
            {
                if (value >= 0)
                {
                    amount = value;
                }
            }
        }
    }
}

namespace HouseApp
{
    public class HouseSystem
    {
        private Dictionary<int, Person> people;
        private Dictionary<int, Flat> flats;
        private List<Setting> settings;
        private List<Bill> bills;

        public HouseSystem()
        {
            people = new Dictionary<int, Person>();
            flats = new Dictionary<int, Flat>();
            settings = new List<Setting>();
            bills = new List<Bill>();
        }

        public void AddPerson(Person person)
        {
            if (!people.ContainsKey(person.Id))
            {
                people.Add(person.Id, person);
                Console.WriteLine("Человек добавлен.");
            }
            else
            {
                Console.WriteLine("Человек с таким номером уже существует.");
            }
        }

        public void AddFlat(Flat flat)
        {
            if (!flats.ContainsKey(flat.Id))
            {
                flats.Add(flat.Id, flat);
                Console.WriteLine("Квартира добавлена.");
            }
            else
            {
                Console.WriteLine("Квартира с таким номером уже существует.");
            }
        }

        public void AddBill(int id, int flatId, string month, decimal amount)
        {
            if (!flats.ContainsKey(flatId))
            {
                Console.WriteLine("Квартира не найдена.");
                return;
            }

            Bill bill = new Bill(id, flats[flatId], month, amount);
            bills.Add(bill);

            Console.WriteLine("Коммунальный платеж добавлен.");
        }

        public void EditPerson(int personId, string newFullName, int newAge)
        {
            if (!people.ContainsKey(personId))
            {
                Console.WriteLine("Человек не найден.");
                return;
            }

            people[personId].FullName = newFullName;
            people[personId].Age = newAge;

            Console.WriteLine("Данные человека изменены.");
        }

        public void EditFlat(int flatId, int newNumber, double newArea)
        {
            if (!flats.ContainsKey(flatId))
            {
                Console.WriteLine("Квартира не найдена.");
                return;
            }

            flats[flatId].Number = newNumber;
            flats[flatId].Area = newArea;

            Console.WriteLine("Данные квартиры изменены.");
        }

        public void MoveIn(int settingId, int personId, int flatId)
        {
            if (!people.ContainsKey(personId))
            {
                Console.WriteLine("Человек не найден.");
                return;
            }

            if (!flats.ContainsKey(flatId))
            {
                Console.WriteLine("Квартира не найдена.");
                return;
            }

            foreach (Setting setting in settings)
            {
                if (setting.Person.Id == personId && setting.IsActive)
                {
                    Console.WriteLine("Этот человек уже проживает в квартире.");
                    return;
                }
            }

            Setting newSetting = new Setting(
                settingId,
                people[personId],
                flats[flatId],
                DateTime.Now
            );

            settings.Add(newSetting);
            Console.WriteLine("Человек вселен в квартиру.");
        }

        public void MoveOut(int personId)
        {
            foreach (Setting setting in settings)
            {
                if (setting.Person.Id == personId && setting.IsActive)
                {
                    setting.MoveOutDate = DateTime.Now;
                    Console.WriteLine("Человек выселен из квартиры.");
                    return;
                }
            }

            Console.WriteLine("Активное проживание данного человека не найдено.");
        }

        public void ShowFlatResidents(int flatId)
        {
            if (!flats.ContainsKey(flatId))
            {
                Console.WriteLine("Квартира не найдена.");
                return;
            }

            Console.WriteLine("Жильцы квартиры №" + flats[flatId].Number + ":");

            bool found = false;

            foreach (Setting setting in settings)
            {
                if (setting.Flat.Id == flatId && setting.IsActive)
                {
                    Console.WriteLine("- " + setting.Person.FullName +
                                      ", дата вселения: " + setting.MoveInDate.ToShortDateString());
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("В квартире нет жильцов.");
            }
        }

        public void ShowPersonInfo(int personId)
        {
            if (!people.ContainsKey(personId))
            {
                Console.WriteLine("Человек не найден.");
                return;
            }

            Person person = people[personId];

            Console.WriteLine("Информация о человеке:");
            Console.WriteLine("Номер: " + person.Id);
            Console.WriteLine("ФИО: " + person.FullName);
            Console.WriteLine("Возраст: " + person.Age);

            foreach (Setting setting in settings)
            {
                if (setting.Person.Id == personId && setting.IsActive)
                {
                    Console.WriteLine("Проживает в квартире №" + setting.Flat.Number);
                    return;
                }
            }

            Console.WriteLine("Сейчас не проживает ни в одной квартире.");
        }

        public void ShowTotalBills()
        {
            decimal total = 0;

            foreach (Bill bill in bills)
            {
                total += bill.Amount;
            }

            Console.WriteLine("Суммарные коммунальные платежи по дому: " + total);
        }
    }
}

namespace HouseApp
{
    class Program
    {
        static void Main(string[] args)
        {
            HouseSystem system = new HouseSystem();

            system.AddFlat(new Flat(1, 12, 45.5));
            system.AddFlat(new Flat(2, 28, 60.0));

            system.AddPerson(new Person(1, "Иванов Иван Иванович", 35));
            system.AddPerson(new Person(2, "Петров Петр Петрович", 42));
            system.AddPerson(new Person(3, "Сидорова Анна Олеговна", 29));

            system.MoveIn(1, 1, 1);
            system.MoveIn(2, 2, 1);
            system.MoveIn(3, 3, 2);

            system.AddBill(1, 1, "Май", 5400);
            system.AddBill(2, 2, "Май", 7300);

            int menu;

            do
            {
                Console.WriteLine();
                Console.WriteLine("ГЛАВНОЕ МЕНЮ");
                Console.WriteLine("1 - добавить человека");
                Console.WriteLine("2 - добавить квартиру");
                Console.WriteLine("3 - добавить коммунальный платеж");
                Console.WriteLine("4 - редактировать человека");
                Console.WriteLine("5 - редактировать квартиру");
                Console.WriteLine("6 - вселить человека в квартиру");
                Console.WriteLine("7 - выселить человека из квартиры");
                Console.WriteLine("8 - показать жильцов квартиры");
                Console.WriteLine("9 - показать информацию о человеке");
                Console.WriteLine("10 - сумма коммунальных платежей по дому");
                Console.WriteLine("0 - выход");
                Console.Write("Выберите действие: ");

                menu = int.Parse(Console.ReadLine());

                switch (menu)
                {
                    case 1:
                        AddPersonFromConsole(system);
                        break;

                    case 2:
                        AddFlatFromConsole(system);
                        break;

                    case 3:
                        AddBillFromConsole(system);
                        break;

                    case 4:
                        EditPersonFromConsole(system);
                        break;

                    case 5:
                        EditFlatFromConsole(system);
                        break;

                    case 6:
                        MoveInFromConsole(system);
                        break;

                    case 7:
                        MoveOutFromConsole(system);
                        break;

                    case 8:
                        ShowFlatResidentsFromConsole(system);
                        break;

                    case 9:
                        ShowPersonFromConsole(system);
                        break;

                    case 10:
                        system.ShowTotalBills();
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

        static void AddPersonFromConsole(HouseSystem system)
        {
            Console.Write("Введите номер человека: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите ФИО: ");
            string fullName = Console.ReadLine();

            Console.Write("Введите возраст: ");
            int age = int.Parse(Console.ReadLine());

            system.AddPerson(new Person(id, fullName, age));
        }

        static void AddFlatFromConsole(HouseSystem system)
        {
            Console.Write("Введите номер записи квартиры: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите номер квартиры: ");
            int number = int.Parse(Console.ReadLine());

            Console.Write("Введите площадь квартиры: ");
            double area = double.Parse(Console.ReadLine());

            system.AddFlat(new Flat(id, number, area));
        }

        static void AddBillFromConsole(HouseSystem system)
        {
            Console.Write("Введите номер платежа: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите номер записи квартиры: ");
            int flatId = int.Parse(Console.ReadLine());

            Console.Write("Введите месяц: ");
            string month = Console.ReadLine();

            Console.Write("Введите сумму платежа: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            system.AddBill(id, flatId, month, amount);
        }

        static void EditPersonFromConsole(HouseSystem system)
        {
            Console.Write("Введите номер человека: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите новое ФИО: ");
            string fullName = Console.ReadLine();

            Console.Write("Введите новый возраст: ");
            int age = int.Parse(Console.ReadLine());

            system.EditPerson(id, fullName, age);
        }

        static void EditFlatFromConsole(HouseSystem system)
        {
            Console.Write("Введите номер записи квартиры: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите новый номер квартиры: ");
            int number = int.Parse(Console.ReadLine());

            Console.Write("Введите новую площадь: ");
            double area = double.Parse(Console.ReadLine());

            system.EditFlat(id, number, area);
        }

        static void MoveInFromConsole(HouseSystem system)
        {
            Console.Write("Введите номер записи вселения: ");
            int settingId = int.Parse(Console.ReadLine());

            Console.Write("Введите номер человека: ");
            int personId = int.Parse(Console.ReadLine());

            Console.Write("Введите номер записи квартиры: ");
            int flatId = int.Parse(Console.ReadLine());

            system.MoveIn(settingId, personId, flatId);
        }

        static void MoveOutFromConsole(HouseSystem system)
        {
            Console.Write("Введите номер человека: ");
            int personId = int.Parse(Console.ReadLine());

            system.MoveOut(personId);
        }

        static void ShowFlatResidentsFromConsole(HouseSystem system)
        {
            Console.Write("Введите номер записи квартиры: ");
            int flatId = int.Parse(Console.ReadLine());

            system.ShowFlatResidents(flatId);
        }

        static void ShowPersonFromConsole(HouseSystem system)
        {
            Console.Write("Введите номер человека: ");
            int personId = int.Parse(Console.ReadLine());

            system.ShowPersonInfo(personId);
        }
    }
}