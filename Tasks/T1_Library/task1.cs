using System;
using System.Collections.Generic;

namespace LibraryApp
{
    public class Book
    {
        private int id;
        private string title;
        private string author;
        private int year;

        public Book(int id, string title, string author, int year)
        {
            this.id = id;
            this.title = title;
            this.author = author;
            this.year = year;
        }

        public int Id
        {
            get { return id; }
        }

        public string Title
        {
            get { return title; }
        }

        public string Author
        {
            get { return author; }
        }

        public int Year
        {
            get { return year; }
        }
    }

    public class Reader
    {
        private int id;
        private string fullName;

        public Reader(int id, string fullName)
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
        }
    }

    public class IssBook
    {
        private Book book;
        private Reader reader;
        private DateTime issueDate;

        public IssBook(Book book, Reader reader, DateTime issueDate)
        {
            this.book = book;
            this.reader = reader;
            this.issueDate = issueDate;
        }

        public Book Book
        {
            get { return book; }
        }

        public Reader Reader
        {
            get { return reader; }
        }

        public DateTime IssueDate
        {
            get { return issueDate; }
        }
    }

    public class RetBook
    {
        private Book book;
        private Reader reader;
        private DateTime returnDate;

        public RetBook(Book book, Reader reader, DateTime returnDate)
        {
            this.book = book;
            this.reader = reader;
            this.returnDate = returnDate;
        }

        public Book Book
        {
            get { return book; }
        }

        public Reader Reader
        {
            get { return reader; }
        }

        public DateTime ReturnDate
        {
            get { return returnDate; }
        }
    }
}

namespace LibraryApp
{
    public class Library
    {
        private Dictionary<int, Book> books;
        private Dictionary<int, Reader> readers;
        private List<IssBook> issuedBooks;
        private List<RetBook> returnedBooks;

        public Library()
        {
            books = new Dictionary<int, Book>();
            readers = new Dictionary<int, Reader>();
            issuedBooks = new List<IssBook>();
            returnedBooks = new List<RetBook>();
        }

        public void AddBook(Book book)
        {
            if (!books.ContainsKey(book.Id))
            {
                books.Add(book.Id, book);
                Console.WriteLine("Книга добавлена.");
            }
            else
            {
                Console.WriteLine("Книга с таким номером уже существует.");
            }
        }

        public void AddReader(Reader reader)
        {
            if (!readers.ContainsKey(reader.Id))
            {
                readers.Add(reader.Id, reader);
                Console.WriteLine("Читатель добавлен.");
            }
            else
            {
                Console.WriteLine("Читатель с таким номером уже существует.");
            }
        }

        public void IssueBook(int bookId, int readerId)
        {
            if (!books.ContainsKey(bookId))
            {
                Console.WriteLine("Книга не найдена.");
                return;
            }

            if (!readers.ContainsKey(readerId))
            {
                Console.WriteLine("Читатель не найден.");
                return;
            }

            foreach (IssBook item in issuedBooks)
            {
                if (item.Book.Id == bookId)
                {
                    Console.WriteLine("Эта книга уже выдана.");
                    return;
                }
            }

            Book book = books[bookId];
            Reader reader = readers[readerId];

            IssBook issuedBook = new IssBook(book, reader, DateTime.Now);
            issuedBooks.Add(issuedBook);

            Console.WriteLine("Книга успешно выдана.");
        }

        public void ReturnBook(int bookId)
        {
            IssBook foundIssue = null;

            foreach (IssBook item in issuedBooks)
            {
                if (item.Book.Id == bookId)
                {
                    foundIssue = item;
                    break;
                }
            }

            if (foundIssue == null)
            {
                Console.WriteLine("Данная книга не числится среди выданных.");
                return;
            }

            RetBook returnedBook = new RetBook(
                foundIssue.Book,
                foundIssue.Reader,
                DateTime.Now
            );

            returnedBooks.Add(returnedBook);
            issuedBooks.Remove(foundIssue);

            Console.WriteLine("Возврат книги успешно оформлен.");
        }

        public void ShowBookInfo(int bookId)
        {
            if (!books.ContainsKey(bookId))
            {
                Console.WriteLine("Книга не найдена.");
                return;
            }

            Book book = books[bookId];

            Console.WriteLine("Информация о книге:");
            Console.WriteLine("Номер: " + book.Id);
            Console.WriteLine("Название: " + book.Title);
            Console.WriteLine("Автор: " + book.Author);
            Console.WriteLine("Год издания: " + book.Year);

            bool isIssued = false;

            foreach (IssBook item in issuedBooks)
            {
                if (item.Book.Id == bookId)
                {
                    Console.WriteLine("Статус: книга выдана читателю " + item.Reader.FullName);
                    Console.WriteLine("Дата выдачи: " + item.IssueDate.ToShortDateString());
                    isIssued = true;
                    break;
                }
            }

            if (!isIssued)
            {
                Console.WriteLine("Статус: книга находится в библиотеке.");
            }
        }

        public void ShowReaderInfo(int readerId)
        {
            if (!readers.ContainsKey(readerId))
            {
                Console.WriteLine("Читатель не найден.");
                return;
            }

            Reader reader = readers[readerId];

            Console.WriteLine("Информация о читателе:");
            Console.WriteLine("Номер: " + reader.Id);
            Console.WriteLine("ФИО: " + reader.FullName);

            Console.WriteLine("Выданные книги:");

            bool hasBooks = false;

            foreach (IssBook item in issuedBooks)
            {
                if (item.Reader.Id == readerId)
                {
                    Console.WriteLine("- " + item.Book.Title + ", дата выдачи: " +
                                      item.IssueDate.ToShortDateString());
                    hasBooks = true;
                }
            }

            if (!hasBooks)
            {
                Console.WriteLine("У читателя нет выданных книг.");
            }
        }
    }
}

namespace LibraryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();

            library.AddBook(new Book(1, "Война и мир", "Л. Н. Толстой", 1869));
            library.AddBook(new Book(2, "Преступление и наказание", "Ф. М. Достоевский", 1866));
            library.AddBook(new Book(3, "Мастер и Маргарита", "М. А. Булгаков", 1967));

            library.AddReader(new Reader(1, "Иванов Иван Иванович"));
            library.AddReader(new Reader(2, "Петров Петр Петрович"));

            int menu;

            do
            {
                Console.WriteLine();
                Console.WriteLine("ГЛАВНОЕ МЕНЮ");
                Console.WriteLine("1 - добавить книгу");
                Console.WriteLine("2 - добавить читателя");
                Console.WriteLine("3 - выдать книгу");
                Console.WriteLine("4 - вернуть книгу");
                Console.WriteLine("5 - показать информацию о книге");
                Console.WriteLine("6 - показать информацию о читателе");
                Console.WriteLine("0 - выход");
                Console.Write("Выберите действие: ");

                menu = int.Parse(Console.ReadLine());

                switch (menu)
                {
                    case 1:
                        AddBookFromConsole(library);
                        break;

                    case 2:
                        AddReaderFromConsole(library);
                        break;

                    case 3:
                        IssueBookFromConsole(library);
                        break;

                    case 4:
                        ReturnBookFromConsole(library);
                        break;

                    case 5:
                        ShowBookFromConsole(library);
                        break;

                    case 6:
                        ShowReaderFromConsole(library);
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

        static void AddBookFromConsole(Library library)
        {
            Console.Write("Введите номер книги: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите название книги: ");
            string title = Console.ReadLine();

            Console.Write("Введите автора книги: ");
            string author = Console.ReadLine();

            Console.Write("Введите год издания: ");
            int year = int.Parse(Console.ReadLine());

            Book book = new Book(id, title, author, year);
            library.AddBook(book);
        }

        static void AddReaderFromConsole(Library library)
        {
            Console.Write("Введите номер читателя: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите ФИО читателя: ");
            string fullName = Console.ReadLine();

            Reader reader = new Reader(id, fullName);
            library.AddReader(reader);
        }

        static void IssueBookFromConsole(Library library)
        {
            Console.Write("Введите номер книги: ");
            int bookId = int.Parse(Console.ReadLine());

            Console.Write("Введите номер читателя: ");
            int readerId = int.Parse(Console.ReadLine());

            library.IssueBook(bookId, readerId);
        }

        static void ReturnBookFromConsole(Library library)
        {
            Console.Write("Введите номер книги: ");
            int bookId = int.Parse(Console.ReadLine());

            library.ReturnBook(bookId);
        }

        static void ShowBookFromConsole(Library library)
        {
            Console.Write("Введите номер книги: ");
            int bookId = int.Parse(Console.ReadLine());

            library.ShowBookInfo(bookId);
        }

        static void ShowReaderFromConsole(Library library)
        {
            Console.Write("Введите номер читателя: ");
            int readerId = int.Parse(Console.ReadLine());

            library.ShowReaderInfo(readerId);
        }
    }
}