using System;
using System.Collections.Generic;

namespace HospitalApp
{
    public class Patient
    {
        private int id;
        private string fullName;
        private int age;
        private Room room;
        private Doctor doctor;

        public Patient(int id, string fullName, int age, Room room, Doctor doctor)
        {
            this.id = id;
            this.fullName = fullName;
            this.age = age;
            this.room = room;
            this.doctor = doctor;
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

        public Room Room
        {
            get { return room; }
            set { room = value; }
        }

        public Doctor Doctor
        {
            get { return doctor; }
            set { doctor = value; }
        }
    }

    public class Room
    {
        private int id;
        private int number;
        private int floor;

        public Room(int id, int number, int floor)
        {
            this.id = id;
            this.number = number;
            this.floor = floor;
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

        public int Floor
        {
            get { return floor; }
            set { floor = value; }
        }
    }

    public class Doctor
    {
        private int id;
        private string fullName;
        private string specialization;

        public Doctor(int id, string fullName, string specialization)
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

    public class PatState
    {
        private int id;
        private Patient patient;
        private string diagnosis;
        private double temperature;
        private string condition;
        private DateTime date;

        public PatState(int id, Patient patient, string diagnosis, double temperature, string condition, DateTime date)
        {
            this.id = id;
            this.patient = patient;
            this.diagnosis = diagnosis;
            this.temperature = temperature;
            this.condition = condition;
            this.date = date;
        }

        public int Id
        {
            get { return id; }
        }

        public Patient Patient
        {
            get { return patient; }
        }

        public string Diagnosis
        {
            get { return diagnosis; }
            set { diagnosis = value; }
        }

        public double Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }

        public string Condition
        {
            get { return condition; }
            set { condition = value; }
        }

        public DateTime Date
        {
            get { return date; }
        }
    }
}

namespace HospitalApp
{
    public class HospitalSystem
    {
        private Dictionary<int, Patient> patients;
        private Dictionary<int, Room> rooms;
        private Dictionary<int, Doctor> doctors;
        private List<PatState> states;

        public HospitalSystem()
        {
            patients = new Dictionary<int, Patient>();
            rooms = new Dictionary<int, Room>();
            doctors = new Dictionary<int, Doctor>();
            states = new List<PatState>();
        }

        public void AddRoom(Room room)
        {
            if (!rooms.ContainsKey(room.Id))
            {
                rooms.Add(room.Id, room);
                Console.WriteLine("Палата добавлена.");
            }
            else
            {
                Console.WriteLine("Палата с таким номером уже существует.");
            }
        }

        public void AddDoctor(Doctor doctor)
        {
            if (!doctors.ContainsKey(doctor.Id))
            {
                doctors.Add(doctor.Id, doctor);
                Console.WriteLine("Врач добавлен.");
            }
            else
            {
                Console.WriteLine("Врач с таким номером уже существует.");
            }
        }

        public void AddPatient(int id, string fullName, int age, int roomId, int doctorId)
        {
            if (!rooms.ContainsKey(roomId))
            {
                Console.WriteLine("Палата не найдена.");
                return;
            }

            if (!doctors.ContainsKey(doctorId))
            {
                Console.WriteLine("Врач не найден.");
                return;
            }

            if (!patients.ContainsKey(id))
            {
                Patient patient = new Patient(
                    id,
                    fullName,
                    age,
                    rooms[roomId],
                    doctors[doctorId]
                );

                patients.Add(id, patient);
                Console.WriteLine("Больной добавлен.");
            }
            else
            {
                Console.WriteLine("Больной с таким номером уже существует.");
            }
        }

        public void AddPatientState(int id, int patientId, string diagnosis, double temperature, string condition)
        {
            if (!patients.ContainsKey(patientId))
            {
                Console.WriteLine("Больной не найден.");
                return;
            }

            PatState state = new PatState(
                id,
                patients[patientId],
                diagnosis,
                temperature,
                condition,
                DateTime.Now
            );

            states.Add(state);
            Console.WriteLine("Состояние больного добавлено.");
        }

        public void ShowPatientState(int patientId)
        {
            if (!patients.ContainsKey(patientId))
            {
                Console.WriteLine("Больной не найден.");
                return;
            }

            Console.WriteLine("Состояние больного: " + patients[patientId].FullName);

            bool found = false;

            foreach (PatState state in states)
            {
                if (state.Patient.Id == patientId)
                {
                    Console.WriteLine("Диагноз: " + state.Diagnosis);
                    Console.WriteLine("Температура: " + state.Temperature);
                    Console.WriteLine("Состояние: " + state.Condition);
                    Console.WriteLine("Дата записи: " + state.Date.ToShortDateString());
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Записей о состоянии нет.");
            }
        }

        public void ShowPatientsCountInRoom(int roomId)
        {
            if (!rooms.ContainsKey(roomId))
            {
                Console.WriteLine("Палата не найдена.");
                return;
            }

            int count = 0;

            foreach (Patient patient in patients.Values)
            {
                if (patient.Room.Id == roomId)
                {
                    count++;
                }
            }

            Console.WriteLine("Палата №" + rooms[roomId].Number);
            Console.WriteLine("Количество больных в палате: " + count);
        }

        public void ShowPatientsCountByDoctor(int doctorId)
        {
            if (!doctors.ContainsKey(doctorId))
            {
                Console.WriteLine("Врач не найден.");
                return;
            }

            int count = 0;

            foreach (Patient patient in patients.Values)
            {
                if (patient.Doctor.Id == doctorId)
                {
                    count++;
                }
            }

            Console.WriteLine("Врач: " + doctors[doctorId].FullName);
            Console.WriteLine("Количество лечащихся больных: " + count);
        }

        public void ShowPatientInfo(int patientId)
        {
            if (!patients.ContainsKey(patientId))
            {
                Console.WriteLine("Больной не найден.");
                return;
            }

            Patient patient = patients[patientId];

            Console.WriteLine("Информация о больном:");
            Console.WriteLine("Номер: " + patient.Id);
            Console.WriteLine("ФИО: " + patient.FullName);
            Console.WriteLine("Возраст: " + patient.Age);
            Console.WriteLine("Палата: " + patient.Room.Number);
            Console.WriteLine("Врач: " + patient.Doctor.FullName);
        }
    }
}

namespace HospitalApp
{
    class Program
    {
        static void Main(string[] args)
        {
            HospitalSystem system = new HospitalSystem();

            system.AddRoom(new Room(1, 101, 1));
            system.AddRoom(new Room(2, 205, 2));

            system.AddDoctor(new Doctor(1, "Иванов Иван Иванович", "Терапевт"));
            system.AddDoctor(new Doctor(2, "Петров Петр Петрович", "Хирург"));

            system.AddPatient(1, "Сидоров Сергей Сергеевич", 45, 1, 1);
            system.AddPatient(2, "Алексеева Анна Олеговна", 31, 1, 2);
            system.AddPatient(3, "Орлов Дмитрий Павлович", 58, 2, 1);

            system.AddPatientState(1, 1, "ОРВИ", 37.8, "Среднее");
            system.AddPatientState(2, 2, "Перелом", 36.6, "Стабильное");

            int menu;

            do
            {
                Console.WriteLine();
                Console.WriteLine("ГЛАВНОЕ МЕНЮ");
                Console.WriteLine("1 - добавить палату");
                Console.WriteLine("2 - добавить врача");
                Console.WriteLine("3 - добавить больного");
                Console.WriteLine("4 - добавить состояние больного");
                Console.WriteLine("5 - показать информацию о больном");
                Console.WriteLine("6 - показать состояние больного");
                Console.WriteLine("7 - количество больных в палате");
                Console.WriteLine("8 - количество больных у врача");
                Console.WriteLine("0 - выход");
                Console.Write("Выберите действие: ");

                menu = int.Parse(Console.ReadLine());

                switch (menu)
                {
                    case 1:
                        AddRoomFromConsole(system);
                        break;

                    case 2:
                        AddDoctorFromConsole(system);
                        break;

                    case 3:
                        AddPatientFromConsole(system);
                        break;

                    case 4:
                        AddStateFromConsole(system);
                        break;

                    case 5:
                        ShowPatientFromConsole(system);
                        break;

                    case 6:
                        ShowStateFromConsole(system);
                        break;

                    case 7:
                        ShowRoomCountFromConsole(system);
                        break;

                    case 8:
                        ShowDoctorCountFromConsole(system);
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

        static void AddRoomFromConsole(HospitalSystem system)
        {
            Console.Write("Введите номер записи палаты: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите номер палаты: ");
            int number = int.Parse(Console.ReadLine());

            Console.Write("Введите этаж: ");
            int floor = int.Parse(Console.ReadLine());

            system.AddRoom(new Room(id, number, floor));
        }

        static void AddDoctorFromConsole(HospitalSystem system)
        {
            Console.Write("Введите номер врача: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите ФИО врача: ");
            string fullName = Console.ReadLine();

            Console.Write("Введите специализацию: ");
            string specialization = Console.ReadLine();

            system.AddDoctor(new Doctor(id, fullName, specialization));
        }

        static void AddPatientFromConsole(HospitalSystem system)
        {
            Console.Write("Введите номер больного: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите ФИО больного: ");
            string fullName = Console.ReadLine();

            Console.Write("Введите возраст: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Введите номер записи палаты: ");
            int roomId = int.Parse(Console.ReadLine());

            Console.Write("Введите номер врача: ");
            int doctorId = int.Parse(Console.ReadLine());

            system.AddPatient(id, fullName, age, roomId, doctorId);
        }

        static void AddStateFromConsole(HospitalSystem system)
        {
            Console.Write("Введите номер записи состояния: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите номер больного: ");
            int patientId = int.Parse(Console.ReadLine());

            Console.Write("Введите диагноз: ");
            string diagnosis = Console.ReadLine();

            Console.Write("Введите температуру: ");
            double temperature = double.Parse(Console.ReadLine());

            Console.Write("Введите состояние больного: ");
            string condition = Console.ReadLine();

            system.AddPatientState(id, patientId, diagnosis, temperature, condition);
        }

        static void ShowPatientFromConsole(HospitalSystem system)
        {
            Console.Write("Введите номер больного: ");
            int patientId = int.Parse(Console.ReadLine());

            system.ShowPatientInfo(patientId);
        }

        static void ShowStateFromConsole(HospitalSystem system)
        {
            Console.Write("Введите номер больного: ");
            int patientId = int.Parse(Console.ReadLine());

            system.ShowPatientState(patientId);
        }

        static void ShowRoomCountFromConsole(HospitalSystem system)
        {
            Console.Write("Введите номер записи палаты: ");
            int roomId = int.Parse(Console.ReadLine());

            system.ShowPatientsCountInRoom(roomId);
        }

        static void ShowDoctorCountFromConsole(HospitalSystem system)
        {
            Console.Write("Введите номер врача: ");
            int doctorId = int.Parse(Console.ReadLine());

            system.ShowPatientsCountByDoctor(doctorId);
        }
    }
}