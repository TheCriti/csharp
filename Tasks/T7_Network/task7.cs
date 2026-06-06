using System;
using System.Collections.Generic;

namespace NetworkApp
{
    public class Room
    {
        private int id;
        private string number;
        private string department;

        public Room(int id, string number, string department)
        {
            this.id = id;
            this.number = number;
            this.department = department;
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

        public string Department
        {
            get { return department; }
            set { department = value; }
        }
    }

    public class AccessoryType
    {
        private int id;
        private string name;

        public AccessoryType(int id, string name)
        {
            this.id = id;
            this.name = name;
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
    }

    public class Computer
    {
        private int id;
        private string name;
        private string ipAddress;
        private Room room;

        public Computer(int id, string name, string ipAddress, Room room)
        {
            this.id = id;
            this.name = name;
            this.ipAddress = ipAddress;
            this.room = room;
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

        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        public Room Room
        {
            get { return room; }
            set { room = value; }
        }
    }

    public class Accessory
    {
        private int id;
        private string name;
        private AccessoryType type;
        private Computer computer;
        private int value;

        public Accessory(int id, string name, AccessoryType type, Computer computer, int value)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.computer = computer;
            this.value = value;
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

        public AccessoryType Type
        {
            get { return type; }
            set { type = value; }
        }

        public Computer Computer
        {
            get { return computer; }
            set { computer = value; }
        }

        public int Value
        {
            get { return value; }
            set
            {
                if (value >= 0)
                {
                    this.value = value;
                }
            }
        }
    }
}

namespace NetworkApp
{
    public class NetworkSystem
    {
        private Dictionary<int, Computer> computers;
        private Dictionary<int, Room> rooms;
        private Dictionary<int, AccessoryType> types;
        private List<Accessory> accessories;

        public NetworkSystem()
        {
            computers = new Dictionary<int, Computer>();
            rooms = new Dictionary<int, Room>();
            types = new Dictionary<int, AccessoryType>();
            accessories = new List<Accessory>();
        }

        public void AddRoom(Room room)
        {
            if (!rooms.ContainsKey(room.Id))
            {
                rooms.Add(room.Id, room);
                Console.WriteLine("Комната добавлена.");
            }
            else
            {
                Console.WriteLine("Комната с таким номером уже существует.");
            }
        }

        public void AddAccessoryType(AccessoryType type)
        {
            if (!types.ContainsKey(type.Id))
            {
                types.Add(type.Id, type);
                Console.WriteLine("Тип комплектующего добавлен.");
            }
            else
            {
                Console.WriteLine("Тип комплектующего с таким номером уже существует.");
            }
        }

        public void AddComputer(int id, string name, string ipAddress, int roomId)
        {
            if (!rooms.ContainsKey(roomId))
            {
                Console.WriteLine("Комната не найдена.");
                return;
            }

            if (!computers.ContainsKey(id))
            {
                Computer computer = new Computer(id, name, ipAddress, rooms[roomId]);
                computers.Add(id, computer);
                Console.WriteLine("Компьютер добавлен.");
            }
            else
            {
                Console.WriteLine("Компьютер с таким номером уже существует.");
            }
        }

        public void AddAccessory(int id, string name, int typeId, int computerId, int value)
        {
            if (!types.ContainsKey(typeId))
            {
                Console.WriteLine("Тип комплектующего не найден.");
                return;
            }

            if (!computers.ContainsKey(computerId))
            {
                Console.WriteLine("Компьютер не найден.");
                return;
            }

            Accessory accessory = new Accessory(
                id,
                name,
                types[typeId],
                computers[computerId],
                value
            );

            accessories.Add(accessory);
            Console.WriteLine("Комплектующее добавлено.");
        }

        public void EditComputer(int computerId, string newName, string newIpAddress, int roomId)
        {
            if (!computers.ContainsKey(computerId))
            {
                Console.WriteLine("Компьютер не найден.");
                return;
            }

            if (!rooms.ContainsKey(roomId))
            {
                Console.WriteLine("Комната не найдена.");
                return;
            }

            computers[computerId].Name = newName;
            computers[computerId].IpAddress = newIpAddress;
            computers[computerId].Room = rooms[roomId];

            Console.WriteLine("Данные компьютера изменены.");
        }

        public void EditAccessory(int accessoryId, string newName, int newValue)
        {
            foreach (Accessory accessory in accessories)
            {
                if (accessory.Id == accessoryId)
                {
                    accessory.Name = newName;
                    accessory.Value = newValue;
                    Console.WriteLine("Данные комплектующего изменены.");
                    return;
                }
            }

            Console.WriteLine("Комплектующее не найдено.");
        }

        public void ShowComputerInfo(int computerId)
        {
            if (!computers.ContainsKey(computerId))
            {
                Console.WriteLine("Компьютер не найден.");
                return;
            }

            Computer computer = computers[computerId];

            Console.WriteLine("Информация о компьютере:");
            Console.WriteLine("Номер: " + computer.Id);
            Console.WriteLine("Название: " + computer.Name);
            Console.WriteLine("IP-адрес: " + computer.IpAddress);
            Console.WriteLine("Комната: " + computer.Room.Number);
            Console.WriteLine("Отдел: " + computer.Room.Department);

            Console.WriteLine("Комплектующие:");

            bool found = false;

            foreach (Accessory accessory in accessories)
            {
                if (accessory.Computer.Id == computerId)
                {
                    Console.WriteLine("- " + accessory.Type.Name + ": " +
                                      accessory.Name + ", значение: " + accessory.Value);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Комплектующие не добавлены.");
            }
        }

        public void ShowTotalRam()
        {
            int totalRam = 0;

            foreach (Accessory accessory in accessories)
            {
                if (accessory.Type.Name.ToLower() == "оперативная память")
                {
                    totalRam += accessory.Value;
                }
            }

            Console.WriteLine("Суммарный объем оперативной памяти: " + totalRam + " ГБ");
        }

        public void ShowComputersInRoom(int roomId)
        {
            if (!rooms.ContainsKey(roomId))
            {
                Console.WriteLine("Комната не найдена.");
                return;
            }

            Console.WriteLine("Компьютеры в комнате " + rooms[roomId].Number + ":");

            bool found = false;

            foreach (Computer computer in computers.Values)
            {
                if (computer.Room.Id == roomId)
                {
                    Console.WriteLine("- " + computer.Name + ", IP: " + computer.IpAddress);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("В комнате нет компьютеров.");
            }
        }
    }
}

namespace NetworkApp
{
    class Program
    {
        static void Main(string[] args)
        {
            NetworkSystem system = new NetworkSystem();

            system.AddRoom(new Room(1, "201", "Бухгалтерия"));
            system.AddRoom(new Room(2, "305", "Отдел продаж"));

            system.AddAccessoryType(new AccessoryType(1, "Оперативная память"));
            system.AddAccessoryType(new AccessoryType(2, "Процессор"));
            system.AddAccessoryType(new AccessoryType(3, "Жесткий диск"));

            system.AddComputer(1, "PC-01", "192.168.1.10", 1);
            system.AddComputer(2, "PC-02", "192.168.1.11", 1);
            system.AddComputer(3, "PC-03", "192.168.1.12", 2);

            system.AddAccessory(1, "Kingston DDR4", 1, 1, 16);
            system.AddAccessory(2, "Intel Core i5", 2, 1, 0);
            system.AddAccessory(3, "Kingston DDR4", 1, 2, 8);
            system.AddAccessory(4, "AMD Ryzen 5", 2, 2, 0);
            system.AddAccessory(5, "Samsung SSD", 3, 3, 512);
            system.AddAccessory(6, "Kingston DDR4", 1, 3, 32);

            int menu;

            do
            {
                Console.WriteLine();
                Console.WriteLine("ГЛАВНОЕ МЕНЮ");
                Console.WriteLine("1 - добавить комнату");
                Console.WriteLine("2 - добавить тип комплектующего");
                Console.WriteLine("3 - добавить компьютер");
                Console.WriteLine("4 - добавить комплектующее");
                Console.WriteLine("5 - редактировать компьютер");
                Console.WriteLine("6 - редактировать комплектующее");
                Console.WriteLine("7 - показать характеристики компьютера");
                Console.WriteLine("8 - показать суммарный объем оперативной памяти");
                Console.WriteLine("9 - показать компьютеры в комнате");
                Console.WriteLine("0 - выход");
                Console.Write("Выберите действие: ");

                menu = int.Parse(Console.ReadLine());

                switch (menu)
                {
                    case 1:
                        AddRoomFromConsole(system);
                        break;

                    case 2:
                        AddTypeFromConsole(system);
                        break;

                    case 3:
                        AddComputerFromConsole(system);
                        break;

                    case 4:
                        AddAccessoryFromConsole(system);
                        break;

                    case 5:
                        EditComputerFromConsole(system);
                        break;

                    case 6:
                        EditAccessoryFromConsole(system);
                        break;

                    case 7:
                        ShowComputerFromConsole(system);
                        break;

                    case 8:
                        system.ShowTotalRam();
                        break;

                    case 9:
                        ShowComputersInRoomFromConsole(system);
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

        static void AddRoomFromConsole(NetworkSystem system)
        {
            Console.Write("Введите номер записи комнаты: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите номер комнаты: ");
            string number = Console.ReadLine();

            Console.Write("Введите название отдела: ");
            string department = Console.ReadLine();

            system.AddRoom(new Room(id, number, department));
        }

        static void AddTypeFromConsole(NetworkSystem system)
        {
            Console.Write("Введите номер типа комплектующего: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите название типа: ");
            string name = Console.ReadLine();

            system.AddAccessoryType(new AccessoryType(id, name));
        }

        static void AddComputerFromConsole(NetworkSystem system)
        {
            Console.Write("Введите номер компьютера: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите название компьютера: ");
            string name = Console.ReadLine();

            Console.Write("Введите IP-адрес: ");
            string ip = Console.ReadLine();

            Console.Write("Введите номер комнаты: ");
            int roomId = int.Parse(Console.ReadLine());

            system.AddComputer(id, name, ip, roomId);
        }

        static void AddAccessoryFromConsole(NetworkSystem system)
        {
            Console.Write("Введите номер комплектующего: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите название комплектующего: ");
            string name = Console.ReadLine();

            Console.Write("Введите номер типа комплектующего: ");
            int typeId = int.Parse(Console.ReadLine());

            Console.Write("Введите номер компьютера: ");
            int computerId = int.Parse(Console.ReadLine());

            Console.Write("Введите значение характеристики: ");
            int value = int.Parse(Console.ReadLine());

            system.AddAccessory(id, name, typeId, computerId, value);
        }

        static void EditComputerFromConsole(NetworkSystem system)
        {
            Console.Write("Введите номер компьютера: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите новое название компьютера: ");
            string name = Console.ReadLine();

            Console.Write("Введите новый IP-адрес: ");
            string ip = Console.ReadLine();

            Console.Write("Введите новый номер комнаты: ");
            int roomId = int.Parse(Console.ReadLine());

            system.EditComputer(id, name, ip, roomId);
        }

        static void EditAccessoryFromConsole(NetworkSystem system)
        {
            Console.Write("Введите номер комплектующего: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите новое название комплектующего: ");
            string name = Console.ReadLine();

            Console.Write("Введите новое значение характеристики: ");
            int value = int.Parse(Console.ReadLine());

            system.EditAccessory(id, name, value);
        }

        static void ShowComputerFromConsole(NetworkSystem system)
        {
            Console.Write("Введите номер компьютера: ");
            int id = int.Parse(Console.ReadLine());

            system.ShowComputerInfo(id);
        }

        static void ShowComputersInRoomFromConsole(NetworkSystem system)
        {
            Console.Write("Введите номер комнаты: ");
            int roomId = int.Parse(Console.ReadLine());

            system.ShowComputersInRoom(roomId);
        }
    }
}