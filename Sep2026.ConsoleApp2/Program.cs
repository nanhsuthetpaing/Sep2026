using Sep2026.ConsoleApp2;

Console.WriteLine("Student Management");

Console.Write("Please enter student no: ");
string no = Console.ReadLine();

Console.Write("Please enter student name: ");
string name = Console.ReadLine();   

Console.Write("Please enter student ClassNo: ");
string classNo = Console.ReadLine();

StudentService studentService = new StudentService();

Console.ReadLine();
