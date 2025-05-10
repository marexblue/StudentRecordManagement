using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRecordManagement
{
    // Represents a student and their attendance records
    public class Student
    {
        public string Id { get; }
        public string Name { get; }

        // List<DateTime> is a dynamic array to store each date the student was present
        private readonly List<DateTime> attendanceDates = new List<DateTime>();

        public Student(string id, string name)
        {
            Id = id;
            Name = name;
        }

        // Record attendance for a given date (prevents duplicates)
        public void AddAttendance(DateTime date)
        {
            if (!attendanceDates.Contains(date))  // Linear search in List for duplicate check
                attendanceDates.Add(date);        // Amortized O(1) append
        }

        // Provides read-only access to attendance dates
        public IReadOnlyList<DateTime> AttendanceDates => attendanceDates.AsReadOnly();
    }
}
