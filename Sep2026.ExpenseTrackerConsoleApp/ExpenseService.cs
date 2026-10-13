using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sep2026.ExpenseTrackerConsoleApp
{
    public class ExpenseService
    {
        public static List<ExpenseDto> Expenses = new List<ExpenseDto>();

        public void Add(string description, decimal amount, string category, DateTime date)
        {
            if (amount <= 0)
            {
                Console.WriteLine("Amount must be greater than 0.");
                return;
            }
            int maxId = Expenses.Count == 0 ? 0 : Expenses.Max(x => x.Id);
            ExpenseDto expense = new ExpenseDto
            {
                Id = maxId + 1,
                Description = description.Trim(),
                Amount = amount,
                Category = category.Trim(),
                Date = date
            };
            Expenses.Add(expense);
            Console.WriteLine("Expense added successfully.");
        }

        public void ShowExpenses()
        {
            List<ExpenseDto> expenses = GetAll();

            if (expenses.Count == 0)
            {
                Console.WriteLine("No expenses yet.");
                return;
            }

            decimal total = 0;
            foreach (ExpenseDto expense in expenses)
            {
                Console.WriteLine(expense.Id + " | " + expense.Date.ToString("yyyy-MM-dd") + " | "
                    + expense.Category + " | " + expense.Amount.ToString("N2") + " | " + expense.Description);
                total += expense.Amount;
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Total: " + total.ToString("N2"));
        }

        public void Update(int id, string description, decimal amount, string category)
        {
            ExpenseDto? expense = GetById(id);

            if (expense == null)
            {
                Console.WriteLine("Expense not found.");
                return;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Amount must be greater than 0.");
                return;
            }

            expense.Description = description.Trim();
            expense.Amount = amount;
            expense.Category = category.Trim();
            Console.WriteLine("Expense updated successfully.");
        }

        public void Delete(int id)
        {
            int removed = Expenses.RemoveAll(x => x.Id == id);

            if (removed == 0)
            {
                Console.WriteLine("Expense not found.");
            }
            else
            {
                Console.WriteLine("Expense deleted successfully.");
            }
        }

        public ExpenseDto? GetById(int id)
        {
            return Expenses.FirstOrDefault(x => x.Id == id);
        }

        public List<ExpenseDto> GetAll()
        {
            return Expenses.OrderBy(x => x.Date).ThenBy(x => x.Id).ToList();
        }

        public List<ExpenseDto> GetByMonth(int year, int month)
        {
            return Expenses
                .Where(x => x.Date.Year == year && x.Date.Month == month)
                .OrderBy(x => x.Date)
                .ToList();
        }
    }
}
