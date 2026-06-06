using System;
using System.Collections.Generic;

namespace WarehouseApp
{
    public class Good
    {
        private int id;
        private string name;
        private decimal price;

        public Good(int id, string name, decimal price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
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
    }

    public class Store
    {
        private int id;
        private string address;

        public Store(int id, string address)
        {
            this.id = id;
            this.address = address;
        }

        public int Id
        {
            get { return id; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
    }

    public class Storekeeper
    {
        private int id;
        private string fullName;
        private Store store;

        public Storekeeper(int id, string fullName)
        {
            this.id = id;
            this.fullName = fullName;
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

        public Store Store
        {
            get { return store; }
            set { store = value; }
        }
    }

    public class GoodsInStore
    {
        private int id;
        private Good good;
        private Store store;
        private int amount;
        private DateTime date;

        public GoodsInStore(int id, Good good, Store store, int amount, DateTime date)
        {
            this.id = id;
            this.good = good;
            this.store = store;
            this.amount = amount;
            this.date = date;
        }

        public int Id
        {
            get { return id; }
        }

        public Good Good
        {
            get { return good; }
        }

        public Store Store
        {
            get { return store; }
        }

        public int Amount
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

        public DateTime Date
        {
            get { return date; }
        }
    }
}

namespace WarehouseApp
{
    public class WarehouseSystem
    {
        private Dictionary<int, Good> goods;
        private Dictionary<int, Store> stores;
        private Dictionary<int, Storekeeper> storekeepers;
        private List<GoodsInStore> goodsInStores;

        public WarehouseSystem()
        {
            goods = new Dictionary<int, Good>();
            stores = new Dictionary<int, Store>();
            storekeepers = new Dictionary<int, Storekeeper>();
            goodsInStores = new List<GoodsInStore>();
        }

        public void AddGood(Good good)
        {
            if (!goods.ContainsKey(good.Id))
            {
                goods.Add(good.Id, good);
                Console.WriteLine("Товар добавлен.");
            }
            else
            {
                Console.WriteLine("Товар с таким номером уже существует.");
            }
        }

        public void AddStore(Store store)
        {
            if (!stores.ContainsKey(store.Id))
            {
                stores.Add(store.Id, store);
                Console.WriteLine("Склад добавлен.");
            }
            else
            {
                Console.WriteLine("Склад с таким номером уже существует.");
            }
        }

        public void AddStorekeeper(Storekeeper storekeeper)
        {
            if (!storekeepers.ContainsKey(storekeeper.Id))
            {
                storekeepers.Add(storekeeper.Id, storekeeper);
                Console.WriteLine("Кладовщик добавлен.");
            }
            else
            {
                Console.WriteLine("Кладовщик с таким номером уже существует.");
            }
        }

        public void LinkStorekeeperToStore(int storekeeperId, int storeId)
        {
            if (!storekeepers.ContainsKey(storekeeperId))
            {
                Console.WriteLine("Кладовщик не найден.");
                return;
            }

            if (!stores.ContainsKey(storeId))
            {
                Console.WriteLine("Склад не найден.");
                return;
            }

            storekeepers[storekeeperId].Store = stores[storeId];
            Console.WriteLine("Кладовщик связан со складом.");
        }

        public void AddGoodsInStore(int id, int goodId, int storeId, int amount)
        {
            if (!goods.ContainsKey(goodId))
            {
                Console.WriteLine("Товар не найден.");
                return;
            }

            if (!stores.ContainsKey(storeId))
            {
                Console.WriteLine("Склад не найден.");
                return;
            }

            GoodsInStore record = new GoodsInStore(
                id,
                goods[goodId],
                stores[storeId],
                amount,
                DateTime.Now
            );

            goodsInStores.Add(record);
            Console.WriteLine("Поступление товара оформлено.");
        }

        public void ShowGoodInfo(int goodId)
        {
            if (!goods.ContainsKey(goodId))
            {
                Console.WriteLine("Товар не найден.");
                return;
            }

            Good good = goods[goodId];

            Console.WriteLine("Информация о товаре:");
            Console.WriteLine("Номер: " + good.Id);
            Console.WriteLine("Название: " + good.Name);
            Console.WriteLine("Цена: " + good.Price);

            int totalAmount = 0;

            foreach (GoodsInStore item in goodsInStores)
            {
                if (item.Good.Id == goodId)
                {
                    totalAmount += item.Amount;
                }
            }

            Console.WriteLine("Общее количество на складах: " + totalAmount);
        }

        public void EditGood(int goodId, string newName, decimal newPrice)
        {
            if (!goods.ContainsKey(goodId))
            {
                Console.WriteLine("Товар не найден.");
                return;
            }

            goods[goodId].Name = newName;
            goods[goodId].Price = newPrice;

            Console.WriteLine("Характеристики товара изменены.");
        }

        public void ShowStorekeeperInfo(int storekeeperId)
        {
            if (!storekeepers.ContainsKey(storekeeperId))
            {
                Console.WriteLine("Кладовщик не найден.");
                return;
            }

            Storekeeper storekeeper = storekeepers[storekeeperId];

            Console.WriteLine("Информация о кладовщике:");
            Console.WriteLine("Номер: " + storekeeper.Id);
            Console.WriteLine("ФИО: " + storekeeper.FullName);

            if (storekeeper.Store != null)
            {
                Console.WriteLine("Склад: " + storekeeper.Store.Address);
            }
            else
            {
                Console.WriteLine("Склад не назначен.");
            }
        }

        public void ShowStoreInfo(int storeId)
        {
            if (!stores.ContainsKey(storeId))
            {
                Console.WriteLine("Склад не найден.");
                return;
            }

            Store store = stores[storeId];

            Console.WriteLine("Информация о складе:");
            Console.WriteLine("Номер: " + store.Id);
            Console.WriteLine("Адрес: " + store.Address);

            Console.WriteLine("Товары на складе:");

            bool hasGoods = false;

            foreach (GoodsInStore item in goodsInStores)
            {
                if (item.Store.Id == storeId)
                {
                    Console.WriteLine("- " + item.Good.Name + ", количество: " + item.Amount);
                    hasGoods = true;
                }
            }

            if (!hasGoods)
            {
                Console.WriteLine("На складе нет товаров.");
            }
        }
    }
}

namespace WarehouseApp
{
    class Program
    {
        static void Main(string[] args)
        {
            WarehouseSystem system = new WarehouseSystem();

            system.AddGood(new Good(1, "Ноутбук", 75000));
            system.AddGood(new Good(2, "Монитор", 25000));
            system.AddGood(new Good(3, "Клавиатура", 3500));

            system.AddStore(new Store(1, "Склад №1, ул. Ленина, 10"));
            system.AddStore(new Store(2, "Склад №2, ул. Садовая, 5"));

            system.AddStorekeeper(new Storekeeper(1, "Иванов Иван Иванович"));
            system.AddStorekeeper(new Storekeeper(2, "Петров Петр Петрович"));

            int menu;

            do
            {
                Console.WriteLine();
                Console.WriteLine("ГЛАВНОЕ МЕНЮ");
                Console.WriteLine("1 - добавить товар");
                Console.WriteLine("2 - добавить склад");
                Console.WriteLine("3 - добавить кладовщика");
                Console.WriteLine("4 - связать кладовщика со складом");
                Console.WriteLine("5 - оформить поступление товара");
                Console.WriteLine("6 - показать информацию о товаре");
                Console.WriteLine("7 - редактировать товар");
                Console.WriteLine("8 - показать информацию о складе");
                Console.WriteLine("9 - показать информацию о кладовщике");
                Console.WriteLine("0 - выход");
                Console.Write("Выберите действие: ");

                menu = int.Parse(Console.ReadLine());

                switch (menu)
                {
                    case 1:
                        AddGoodFromConsole(system);
                        break;

                    case 2:
                        AddStoreFromConsole(system);
                        break;

                    case 3:
                        AddStorekeeperFromConsole(system);
                        break;

                    case 4:
                        LinkStorekeeperFromConsole(system);
                        break;

                    case 5:
                        AddGoodsInStoreFromConsole(system);
                        break;

                    case 6:
                        ShowGoodFromConsole(system);
                        break;

                    case 7:
                        EditGoodFromConsole(system);
                        break;

                    case 8:
                        ShowStoreFromConsole(system);
                        break;

                    case 9:
                        ShowStorekeeperFromConsole(system);
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

        static void AddGoodFromConsole(WarehouseSystem system)
        {
            Console.Write("Введите номер товара: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите название товара: ");
            string name = Console.ReadLine();

            Console.Write("Введите цену товара: ");
            decimal price = decimal.Parse(Console.ReadLine());

            system.AddGood(new Good(id, name, price));
        }

        static void AddStoreFromConsole(WarehouseSystem system)
        {
            Console.Write("Введите номер склада: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите адрес склада: ");
            string address = Console.ReadLine();

            system.AddStore(new Store(id, address));
        }

        static void AddStorekeeperFromConsole(WarehouseSystem system)
        {
            Console.Write("Введите номер кладовщика: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите ФИО кладовщика: ");
            string fullName = Console.ReadLine();

            system.AddStorekeeper(new Storekeeper(id, fullName));
        }

        static void LinkStorekeeperFromConsole(WarehouseSystem system)
        {
            Console.Write("Введите номер кладовщика: ");
            int storekeeperId = int.Parse(Console.ReadLine());

            Console.Write("Введите номер склада: ");
            int storeId = int.Parse(Console.ReadLine());

            system.LinkStorekeeperToStore(storekeeperId, storeId);
        }

        static void AddGoodsInStoreFromConsole(WarehouseSystem system)
        {
            Console.Write("Введите номер записи поступления: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите номер товара: ");
            int goodId = int.Parse(Console.ReadLine());

            Console.Write("Введите номер склада: ");
            int storeId = int.Parse(Console.ReadLine());

            Console.Write("Введите количество товара: ");
            int amount = int.Parse(Console.ReadLine());

            system.AddGoodsInStore(id, goodId, storeId, amount);
        }

        static void ShowGoodFromConsole(WarehouseSystem system)
        {
            Console.Write("Введите номер товара: ");
            int goodId = int.Parse(Console.ReadLine());

            system.ShowGoodInfo(goodId);
        }

        static void EditGoodFromConsole(WarehouseSystem system)
        {
            Console.Write("Введите номер товара: ");
            int goodId = int.Parse(Console.ReadLine());

            Console.Write("Введите новое название товара: ");
            string name = Console.ReadLine();

            Console.Write("Введите новую цену товара: ");
            decimal price = decimal.Parse(Console.ReadLine());

            system.EditGood(goodId, name, price);
        }

        static void ShowStoreFromConsole(WarehouseSystem system)
        {
            Console.Write("Введите номер склада: ");
            int storeId = int.Parse(Console.ReadLine());

            system.ShowStoreInfo(storeId);
        }

        static void ShowStorekeeperFromConsole(WarehouseSystem system)
        {
            Console.Write("Введите номер кладовщика: ");
            int storekeeperId = int.Parse(Console.ReadLine());

            system.ShowStorekeeperInfo(storekeeperId);
        }
    }
}