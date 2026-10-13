using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sep2026.ExpenseTrackerConsoleApp
{
    public class ReportService
    {
        private readonly ExpenseService _expenseService;

        public ReportService(ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        public void ShowMonthlyReport(int year, int month)
        {
            List<ExpenseDto> expenses = _expenseService.GetByMonth(year, month);

            Console.WriteLine("--- Report for " + year + "-" + month.ToString("00") + " ---");

            if (expenses.Count == 0)
            {
                Console.WriteLine("No expenses this month.");
                return;
            }

            Console.WriteLine("Total spent: " + expenses.Sum(x => x.Amount).ToString("N2"));

            Console.WriteLine("By category:");
            var groups = expenses
                .GroupBy(x => x.Category, StringComparer.OrdinalIgnoreCase)
                .OrderByDescending(g => g.Sum(x => x.Amount));

            foreach (var group in groups)
            {
                Console.WriteLine("  " + group.Key.PadRight(12) + group.Sum(x => x.Amount).ToString("N2"));
            }

            ExpenseDto highest = expenses.OrderByDescending(x => x.Amount).First();
            Console.WriteLine("Highest expense: " + highest.Description + " (" + highest.Amount.ToString("N2") + ")");
        }

        public void ShowBudget(int year, int month, decimal budget)
        {
            decimal total = _expenseService.GetByMonth(year, month).Sum(x => x.Amount);
            decimal remaining = budget - total;

            Console.WriteLine("Spent: " + total.ToString("N2") + " / Budget: " + budget.ToString("N2"));

            if (remaining < 0)
            {
                Console.WriteLine("WARNING: You are over budget by " + Math.Abs(remaining).ToString("N2") + "!");
            }
            else
            {
                Console.WriteLine("Good! You still have " + remaining.ToString("N2") + " left.");
            }
        }
    }
}