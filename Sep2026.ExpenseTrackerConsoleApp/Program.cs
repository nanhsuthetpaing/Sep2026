using Sep2026.ExpenseTrackerConsoleApp;

ExpenseService expenseService = new ExpenseService();
ReportService reportService = new ReportService(expenseService);
bool running = true;


while (running)
{
    Console.WriteLine("Expense Tracker");
    Console.WriteLine("1. Add expense");
    Console.WriteLine("2. Show all expenses");
    Console.WriteLine("3. Edit expense");
    Console.WriteLine("4. Delete expense");
    Console.WriteLine("5. Monthly report");
    Console.WriteLine("6. Check budget");
    Console.WriteLine("7. Exit");
    Console.Write("Choose an option: ");
    string? option = Console.ReadLine();

    if (option == null)
    {
        break;
    }

    switch (option)
    {
        case "1":
            {
                string description = ReadText("Description: ");
                decimal amount = ReadDecimal("Amount: ");
                string category = ReadText("Category (Food, Transport, Bills...): ");
                DateTime date = ReadDate("Date (yyyy-MM-dd): ");
                expenseService.Add(description, amount, category, date);
                break;
            }
        case "2":
            {
                expenseService.ShowExpenses();
                break;
            }
        case "3":
            {
                expenseService.ShowExpenses();
                int id = ReadNumber("Expense Id to update: ");
                string description = ReadText("New description: ");
                decimal amount = ReadDecimal("New amount: ");
                string category = ReadText("New category: ");
                expenseService.Update(id, description, amount, category);
                break;
            }
        case "4":
            {
                expenseService.ShowExpenses();
                int id = ReadNumber("Expense Id to delete: ");
                expenseService.Delete(id);
                break;
            }
        case "5":
            {
                int year = ReadNumber("Year (example 2026): ");
                int month = ReadMonth();
                reportService.ShowMonthlyReport(year, month);
                break;
            }
        case "6":
            {
                int year = ReadNumber("Year (example 2026): ");
                int month = ReadMonth();
                decimal budget = ReadDecimal("Monthly budget: ");
                reportService.ShowBudget(year, month, budget);
                break;
            }
        case "7":
            {
                running = false;
                break;
            }
        default:
            {
                Console.WriteLine("Please choose an option from 1 to 7.");
                break;
            }
    }

    Console.WriteLine();
}

Console.WriteLine("Exiting app...");

static string ReadText(string message)
{
    while (true)
    {
        Console.Write(message);
        string? input = Console.ReadLine();
        if (input == null)
        {
            throw new EndOfStreamException();
        }
        if (!string.IsNullOrWhiteSpace(input))
        {
            return input.Trim();
        }
        Console.WriteLine("Please enter a value.");
    }
}

static int ReadNumber(string message)
{
    while (true)
    {
        string input = ReadText(message);
        int number;
        bool isNumber = int.TryParse(input, out number);
        if (isNumber)
        {
            return number;
        }
        Console.WriteLine("Please enter a whole number.");
    }
}

static decimal ReadDecimal(string message)
{
    while (true)
    {
        string input = ReadText(message);
        decimal number;
        bool isNumber = decimal.TryParse(input, out number);
        if (isNumber)
        {
            return number;
        }
        Console.WriteLine("Please enter a valid number.");
    }
}

static DateTime ReadDate(string message)
{
    while (true)
    {
        string input = ReadText(message);
        DateTime date;
        bool isDate = DateTime.TryParse(input, out date);
        if (isDate)
        {
            return date;
        }
        Console.WriteLine("Invalid date. Example: 2026-10-13");
    }
}

static int ReadMonth()
{
    while (true)
    {
        int month = ReadNumber("Month (1-12): ");
        if (month >= 1 && month <= 12)
        {
            return month;
        }
        Console.WriteLine("Month must be from 1 to 12.");
    }
}


