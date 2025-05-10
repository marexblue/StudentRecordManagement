using StudentRecordManagement;
using System;
using System.Collections;
using System.Collections.Generic;

namespace StudentAttendanceManagementSystem
{
    /*
    Data Structures Used:
    - LinkedList<Student>: Maintains a dynamic list of students (insertion/removal at ends efficient).
    - Dictionary<string, Student>: Hash table mapping student IDs to Student objects for O(1) lookup.
    - List<DateTime>: Dynamic array inside each Student to store attendance dates.

    Algorithms Implemented:
    - Hash Table lookup: O(1) retrieval of a student by ID via Dictionary.
    - LINQ OrderBy: Uses an efficient sorting algorithm (QuickSort/IntroSort under the hood) to sort students by name.
    - Iteration over LinkedList: O(n) operations to list or process all students.
   */
    class Program
    {
        private static readonly AttendanceManager manager = new AttendanceManager();

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n-- Student Attendance Management System --");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Record Attendance");
                Console.WriteLine("3. View Attendance for Student");
                Console.WriteLine("4. List All Students");
                Console.WriteLine("5. Sort Students by Name");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddStudent(); break;
                    case "2": RecordAttendance(); break;
                    case "3": ViewStudentAttendance(); break;
                    case "4": ListStudents(); break;
                    case "5": SortStudents(); break;
                    case "6": exit = true; break;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        private static void AddStudent()
        {
            Console.Write("Enter Student ID: ");
            var id = Console.ReadLine()?.Trim();
            Console.Write("Enter Student Name: ");
            var name = Console.ReadLine()?.Trim();

            try
            {
                manager.AddStudent(id, name);
                Console.WriteLine("Student added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void RecordAttendance()
        {
            Console.Write("Enter date (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out var date))
            {
                Console.WriteLine("Invalid date format.");
                return;
            }

            foreach (var student in manager.GetAllStudents())
            {
                Console.Write($"Is {student.Name} present? (y/n): ");
                var resp = Console.ReadLine()?.Trim().ToLower();
                if (resp == "y")
                {
                    student.AddAttendance(date); // List append, avoids duplicates
                }
            }

            Console.WriteLine($"Attendance recorded for {date:yyyy-MM-dd}.");
        }

        private static void ViewStudentAttendance()
        {
            Console.Write("Enter Student ID: ");
            var id = Console.ReadLine()?.Trim();
            var student = manager.GetStudentById(id); // O(1) lookup

            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.WriteLine($"\nAttendance records for {student.Name} ({student.Id}):");
            if (student.AttendanceDates.Count == 0)
                Console.WriteLine("No attendance records.");
            else
                foreach (var d in student.AttendanceDates)
                    Console.WriteLine(d.ToString("yyyy-MM-dd"));
        }

        private static void ListStudents()
        {
            Console.WriteLine("\nAll Students:");
            foreach (var s in manager.GetAllStudents()) // O(n) iteration
                Console.WriteLine($"{s.Id} - {s.Name}");
        }

        private static void SortStudents()
        {
            Console.WriteLine("\nStudents sorted by name:");
            foreach (var s in manager.GetStudentsSortedByName()) // O(n log n) sort + O(n) iteration
                Console.WriteLine($"{s.Id} - {s.Name}");
        }
    }
}
