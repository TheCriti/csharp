using System;
using System.Collections.Generic;

namespace UniversityApp
{
    public class Student
    {
        private int id;
        private string fullName;
        private Group group;

        public Student(int id, string fullName, Group group)
        {
            this.id = id;
            this.fullName = fullName;
            this.group = group;
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

        public Group Group
        {
            get { return group; }
            set { group = value; }
        }
    }

    public class Group
    {
        private int id;
        private string name;

        public Group(int id, string name)
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

    public class Course
    {
        private int id;
        private string title;

        public Course(int id, string title)
        {
            this.id = id;
            this.title = title;
        }

        public int Id
        {
            get { return id; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
    }

    public class Progress
    {
        private int id;
        private Student student;
        private Course course;
        private int grade;

        public Progress(int id, Student student, Course course, int grade)
        {
            this.id = id;
            this.student = student;
            this.course = course;
            this.grade = grade;
        }

        public int Id
        {
            get { return id; }
        }

        public Student Student
        {
            get { return student; }
        }

        public Course Course
        {
            get { return course; }
        }

        public int Grade
        {
            get { return grade; }
            set
            {
                if (value >= 2 && value <= 5)
                {
                    grade = value;
                }
            }
        }
    }
}

namespace UniversityApp
{
    public class UniversitySystem
    {
        private Dictionary<int, Student> students;
        private Dictionary<int, Group> groups;
        private Dictionary<int, Course> courses;
        private List<Progress> progressList;

        public UniversitySystem()
        {
            students = new Dictionary<int, Student>();
            groups = new Dictionary<int, Group>();
            courses = new Dictionary<int, Course>();
            progressList = new List<Progress>();
        }

        public void AddGroup(Group group)
        {
            if (!groups.ContainsKey(group.Id))
            {
                groups.Add(group.Id, group);
                Console.WriteLine("Группа добавлена.");
            }
            else
            {
                Console.WriteLine("Группа с таким номером уже существует.");
            }
        }

        public void AddStudent(Student student)
        {
            if (!students.ContainsKey(student.Id))
            {
                students.Add(student.Id, student);
                Console.WriteLine("Студент добавлен.");
            }
            else
            {
                Console.WriteLine("Студент с таким номером уже существует.");
            }
        }

        public void AddCourse(Course course)
        {
            if (!courses.ContainsKey(course.Id))
            {
                courses.Add(course.Id, course);
                Console.WriteLine("Предмет добавлен.");
            }
            else
            {
                Console.WriteLine("Предмет с таким номером уже существует.");
            }
        }

        public void AddProgress(int id, int studentId, int courseId, int grade)
        {
            if (!students.ContainsKey(studentId))
            {
                Console.WriteLine("Студент не найден.");
                return;
            }

            if (!courses.ContainsKey(courseId))
            {
                Console.WriteLine("Предмет не найден.");
                return;
            }

            Progress progress = new Progress(
                id,
                students[studentId],
                courses[courseId],
                grade
            );

            progressList.Add(progress);
            Console.WriteLine("Оценка добавлена.");
        }

        public void EditProgress(int progressId, int newGrade)
        {
            foreach (Progress item in progressList)
            {
                if (item.Id == progressId)
                {
                    item.Grade = newGrade;
                    Console.WriteLine("Оценка изменена.");
                    return;
                }
            }

            Console.WriteLine("Запись успеваемости не найдена.");
        }

        public void ShowStudentAverage(int studentId)
        {
            if (!students.ContainsKey(studentId))
            {
                Console.WriteLine("Студент не найден.");
                return;
            }

            int sum = 0;
            int count = 0;

            foreach (Progress item in progressList)
            {
                if (item.Student.Id == studentId)
                {
                    sum += item.Grade;
                    count++;
                }
            }

            Console.WriteLine("Студент: " + students[studentId].FullName);

            if (count == 0)
            {
                Console.WriteLine("Оценок нет.");
            }
            else
            {
                double average = (double)sum / count;
                Console.WriteLine("Средняя оценка студента: " + average.ToString("F2"));
            }
        }

        public void ShowGroupAverage(int groupId)
        {
            if (!groups.ContainsKey(groupId))
            {
                Console.WriteLine("Группа не найдена.");
                return;
            }

            int sum = 0;
            int count = 0;

            foreach (Progress item in progressList)
            {
                if (item.Student.Group.Id == groupId)
                {
                    sum += item.Grade;
                    count++;
                }
            }

            Console.WriteLine("Группа: " + groups[groupId].Name);

            if (count == 0)
            {
                Console.WriteLine("Оценок по группе нет.");
            }
            else
            {
                double average = (double)sum / count;
                Console.WriteLine("Средняя оценка по группе: " + average.ToString("F2"));
            }
        }

        public Group GetGroup(int groupId)
        {
            if (groups.ContainsKey(groupId))
            {
                return groups[groupId];
            }

            return null;
        }

        public void ShowStudentInfo(int studentId)
        {
            if (!students.ContainsKey(studentId))
            {
                Console.WriteLine("Студент не найден.");
                return;
            }

            Student student = students[studentId];

            Console.WriteLine("Информация о студенте:");
            Console.WriteLine("Номер: " + student.Id);
            Console.WriteLine("ФИО: " + student.FullName);
            Console.WriteLine("Группа: " + student.Group.Name);

            Console.WriteLine("Оценки:");

            bool hasGrades = false;

            foreach (Progress item in progressList)
            {
                if (item.Student.Id == studentId)
                {
                    Console.WriteLine("- " + item.Course.Title + ": " + item.Grade);
                    hasGrades = true;
                }
            }

            if (!hasGrades)
            {
                Console.WriteLine("Оценок нет.");
            }
        }
    }
}

namespace UniversityApp
{
    class Program
    {
        static void Main(string[] args)
        {
            UniversitySystem system = new UniversitySystem();

            system.AddGroup(new Group(1, "2-МД-1"));
            system.AddGroup(new Group(2, "2-МД-2"));

            system.AddCourse(new Course(1, "Программирование"));
            system.AddCourse(new Course(2, "Базы данных"));
            system.AddCourse(new Course(3, "Математика"));

            system.AddStudent(new Student(1, "Иванов Иван Иванович", system.GetGroup(1)));
            system.AddStudent(new Student(2, "Петров Петр Петрович", system.GetGroup(2)));
            system.AddStudent(new Student(3, "Сидоров Сергей Сергеевич", system.GetGroup(2)));

            system.AddProgress(1, 1, 1, 5);
            system.AddProgress(2, 1, 2, 4);
            system.AddProgress(3, 2, 1, 3);
            system.AddProgress(4, 2, 3, 5);
            system.AddProgress(5, 3, 2, 4);

            int menu;

            do
            {
                Console.WriteLine();
                Console.WriteLine("ГЛАВНОЕ МЕНЮ");
                Console.WriteLine("1 - добавить группу");
                Console.WriteLine("2 - добавить студента");
                Console.WriteLine("3 - добавить предмет");
                Console.WriteLine("4 - добавить оценку");
                Console.WriteLine("5 - редактировать оценку");
                Console.WriteLine("6 - показать информацию о студенте");
                Console.WriteLine("7 - показать среднюю оценку студента");
                Console.WriteLine("8 - показать среднюю оценку по группе");
                Console.WriteLine("0 - выход");
                Console.Write("Выберите действие: ");

                menu = int.Parse(Console.ReadLine());

                switch (menu)
                {
                    case 1:
                        AddGroupFromConsole(system);
                        break;

                    case 2:
                        AddStudentFromConsole(system);
                        break;

                    case 3:
                        AddCourseFromConsole(system);
                        break;

                    case 4:
                        AddProgressFromConsole(system);
                        break;

                    case 5:
                        EditProgressFromConsole(system);
                        break;

                    case 6:
                        ShowStudentFromConsole(system);
                        break;

                    case 7:
                        ShowStudentAverageFromConsole(system);
                        break;

                    case 8:
                        ShowGroupAverageFromConsole(system);
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

        static void AddGroupFromConsole(UniversitySystem system)
        {
            Console.Write("Введите номер группы: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите название группы: ");
            string name = Console.ReadLine();

            system.AddGroup(new Group(id, name));
        }

        static void AddStudentFromConsole(UniversitySystem system)
        {
            Console.Write("Введите номер студента: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите ФИО студента: ");
            string fullName = Console.ReadLine();

            Console.Write("Введите номер группы: ");
            int groupId = int.Parse(Console.ReadLine());

            Group group = system.GetGroup(groupId);

            if (group == null)
            {
                Console.WriteLine("Группа не найдена.");
                return;
            }

            system.AddStudent(new Student(id, fullName, group));
        }

        static void AddCourseFromConsole(UniversitySystem system)
        {
            Console.Write("Введите номер предмета: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите название предмета: ");
            string title = Console.ReadLine();

            system.AddCourse(new Course(id, title));
        }

        static void AddProgressFromConsole(UniversitySystem system)
        {
            Console.Write("Введите номер записи успеваемости: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите номер студента: ");
            int studentId = int.Parse(Console.ReadLine());

            Console.Write("Введите номер предмета: ");
            int courseId = int.Parse(Console.ReadLine());

            Console.Write("Введите оценку от 2 до 5: ");
            int grade = int.Parse(Console.ReadLine());

            system.AddProgress(id, studentId, courseId, grade);
        }

        static void EditProgressFromConsole(UniversitySystem system)
        {
            Console.Write("Введите номер записи успеваемости: ");
            int progressId = int.Parse(Console.ReadLine());

            Console.Write("Введите новую оценку от 2 до 5: ");
            int grade = int.Parse(Console.ReadLine());

            system.EditProgress(progressId, grade);
        }

        static void ShowStudentFromConsole(UniversitySystem system)
        {
            Console.Write("Введите номер студента: ");
            int studentId = int.Parse(Console.ReadLine());

            system.ShowStudentInfo(studentId);
        }

        static void ShowStudentAverageFromConsole(UniversitySystem system)
        {
            Console.Write("Введите номер студента: ");
            int studentId = int.Parse(Console.ReadLine());

            system.ShowStudentAverage(studentId);
        }

        static void ShowGroupAverageFromConsole(UniversitySystem system)
        {
            Console.Write("Введите номер группы: ");
            int groupId = int.Parse(Console.ReadLine());

            system.ShowGroupAverage(groupId);
        }
    }
}