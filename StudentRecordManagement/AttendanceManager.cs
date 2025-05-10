using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordManagement
{// Manages the collection of students and their attendance records
    public class AttendanceManager
    {
        // LinkedList for dynamic student list operations
        private readonly LinkedList<Student> students = new LinkedList<Student>();

        // Dictionary<string, Student> serves as a hash table for fast student lookup
        private readonly Dictionary<string, Student> studentMap = new Dictionary<string, Student>();

        // Add a new student (throws if ID already exists)
        public void AddStudent(string id, string name)
        {
            if (studentMap.ContainsKey(id))
                throw new ArgumentException("A student with this ID already exists.");

            var student = new Student(id, name);
            students.AddLast(student);         // O(1) insertion at tail of LinkedList
            studentMap[id] = student;         // O(1) hash table insert
        }

        // Retrieve all students (iteration over LinkedList)
        public IEnumerable<Student> GetAllStudents() => students;

        // Lookup a student by ID using hash table (O(1))
        public Student GetStudentById(string id) => studentMap.TryGetValue(id, out var s) ? s : null;

        // Sort students by Name using LINQ OrderBy (underlying efficient sort algorithm)
        public List<Student> GetStudentsSortedByName()
        {
            return students
                .OrderBy(s => s.Name, StringComparer.OrdinalIgnoreCase) // O(n log n) sort
                .ToList();                                            // Allocate new list
        }
    }

}
