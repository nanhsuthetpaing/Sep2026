using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sep2026.ConsoleApp2
{
    public class StudentService
    {
        public static List<StudentDto> Students = new List<StudentDto>();
        public void RegisterStudent(string no, string name, string classNo)
        {
            int maxId = Students.Count == 0 ? 0 : Students.Max(x => x.Id);
            StudentDto student = new StudentDto
            {
                Id = maxId + 1,
                No = no,
                Name = name,
                ClassNo = classNo
            };
            Students.Add(student);
        }

        public void ShowStudent()
        {
            foreach (var item in Students)
            {
                Console.WriteLine($"Id: {item.Id} | Name: {item.Name}`| ClassNo: {item.ClassNo}");
            }
        }
    }
}
