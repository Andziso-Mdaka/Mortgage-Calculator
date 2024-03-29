using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formative_1
{

    public class MortgageCalculator
    {
        // Private member variables to store loan details
        private double LoanAmount;
        private double AnnualInterestRate;
        private int LoanTermYears;
        private DateTime LoanStartDate;

        // Constructor to initialize the loan details
        public MortgageCalculator(double LoanAmount, double AnnualInterestRate, int LoanTermYears, DateTime LoanStartDate)
        {

            this.LoanAmount = LoanAmount;
            this.AnnualInterestRate = AnnualInterestRate;
            this.LoanTermYears = LoanTermYears;
        }


        // Method to get the loan amount from the user
        public double GetLoanAmount()
        {


            Console.WriteLine("How much is your total loan amount?");
            LoanAmount = Convert.ToDouble(Console.ReadLine());
            if (LoanAmount <= 0) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("loan amount cannot be 0 or less");
                Console.ResetColor();
                GetLoanAmount();
            }
            return LoanAmount;
        }

        // Method to get the annual interest rate from the user
        public double GetAnnualInterestRate()
        {
            Console.WriteLine("What is your annual interest rate?");
            AnnualInterestRate = Convert.ToDouble(Console.ReadLine());
            if (AnnualInterestRate <= 0 ) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Interest rate cannot be 0 or less");
                Console.ResetColor();
                GetAnnualInterestRate();
            }
            return AnnualInterestRate;
        }

        // Method to get the loan term in years from the user
        public int GetLoanTermYears()
        {
            Console.WriteLine("What is the term of the loan in years?");
            LoanTermYears = Convert.ToInt32(Console.ReadLine());
            if (LoanTermYears <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Loan term years cannot be 0 or less");
                Console.ResetColor();
                GetLoanTermYears();
            }
            return LoanTermYears;
        }

        // Method to get the loan start date from the user
        public DateTime GetLoanStartDate()
        {
            Console.WriteLine("When do you want to start the loan?");
            Console.WriteLine("Use YYYY/MM/DD Format");
            LoanStartDate = DateTime.Parse(Console.ReadLine());
            return LoanStartDate;


        }

        // Method to calculate the monthly repayment amount
        public double CalculateMonthlyRepayment()
        {
            double monthlyInterestRate = AnnualInterestRate / (100 * 12);
            double numberOfPayments = LoanTermYears * 12;

            double monthlyRepayment = LoanAmount * monthlyInterestRate / (1 - Math.Pow(1 + monthlyInterestRate, -numberOfPayments));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Your monthly repayment amount is: {monthlyRepayment:C}");
            Console.ResetColor();
            Console.WriteLine(" ");
            return monthlyRepayment;
        }

        // Method to calculate the total interest paid over the loan term
        public double CalculateTotalInterestPaid()
        {

            double totalInterestPaid = CalculateMonthlyRepayment() * LoanTermYears * 12 - LoanAmount;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Total interest paid over the loan term is: {totalInterestPaid:C}");
            Console.ResetColor();
            Console.WriteLine(" ");

            return totalInterestPaid;

        }

        // Method to calculate the total amount paid over the loan term
        public double CalculateTotalAmountPaid(out DateTime endDate)
        {
            double totalAmountPaid = LoanAmount + CalculateTotalInterestPaid();

            endDate = LoanStartDate.AddYears(LoanTermYears);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Total amount paid over the loan term is: {totalAmountPaid:C}");
            Console.WriteLine($"You will finish paying on: {endDate:yyyy/MM/dd}");
            Console.ResetColor();
            Console.WriteLine(" ");
            return totalAmountPaid;
        }

        // Class to represent each entry in the amortization schedule
        public class AmortizationEntry
        {
            public int PaymentNumber { get; set; }
            public double PaymentAmount { get; set; }
            public double InterestPaid { get; set; }
            public double PrincipalPaid { get; set; }
            public double RemainingBalance { get; set; }
        }

        // Method to generate the amortization schedule
        public List<AmortizationEntry> GenerateAmortizationSchedule()
        {
            var schedule = new List<AmortizationEntry>();
            double monthlyInterestRate = AnnualInterestRate / (100 * 12);
            double numberOfPayments = LoanTermYears * 12;
            double monthlyRepayment = CalculateMonthlyRepayment();
            double remainingBalance = LoanAmount;

            for (int i = 1; i <= numberOfPayments; i++)
            {
                double interestPaid = remainingBalance * monthlyInterestRate;
                double principalPaid = monthlyRepayment - interestPaid;
                remainingBalance -= principalPaid;

                schedule.Add(new AmortizationEntry
                {
                    PaymentNumber = i,
                    PaymentAmount = monthlyRepayment,
                    InterestPaid = interestPaid,
                    PrincipalPaid = principalPaid,
                    RemainingBalance = remainingBalance
                });
            }

            return schedule;
        }


        // Method to print the amortization schedule
        public static void PrintAmortizationSchedule(List<AmortizationEntry> schedule)
        {
            Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}", "Payment #", "Payment", "Interest", "Principal", "Balance");

            foreach (var entry in schedule)
            {
                Console.WriteLine("{0,-15}{1,-15:C}{2,-15:C}{3,-15:C}{4,-15:C}", entry.PaymentNumber, entry.PaymentAmount, entry.InterestPaid, entry.PrincipalPaid, entry.RemainingBalance);
            }
        }

        // Main method to initiate the program
        static void Main(string[] args)
        {
            // Create an instance of MortgageCalculator
            MortgageCalculator user = new MortgageCalculator(1, 1, 1, DateTime.Now);

            Console.WriteLine("Mortgage Calculator");
            Console.WriteLine("1. Start");
            Console.WriteLine("2. Exit");
            String ans = Console.ReadLine();

            if (ans == "1")
            {
                // Get loan details from the user
                user.GetLoanAmount();
                Console.WriteLine(" ");
                user.GetAnnualInterestRate();
                Console.WriteLine(" ");
                user.GetLoanTermYears();
                Console.WriteLine(" ");
                user.GetLoanStartDate();
                Console.WriteLine(" ");

            }

            else if (ans == "2")
            {
                // Exit the program
                System.Environment.Exit(0);
            }

            else
            {
                // Prompt for correct input
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect Input, Try again.");
                Console.ResetColor();
                Main(args);
            }

            // Call the Section2 method to provide further options to the user
            Section2();

            // Method to handle the user's choice after entering loan details
            void Section2()
            {
                Console.WriteLine("What would you like to do next?");
                Console.WriteLine("1. Check Monthly Repayment");
                Console.WriteLine("2. Check Total Interest Paid");
                Console.WriteLine("3. Check Total Amount Paid");
                Console.WriteLine("4. Generate Amortization Schedule");
                Console.WriteLine("5. Exit");

                String ans1 = Console.ReadLine();

                switch (ans1)
                {
                    case "1":
                        user.CalculateMonthlyRepayment();
                        Section2();
                        break;
                    case "2":
                        user.CalculateTotalInterestPaid();
                        Section2();
                        break;
                    case "3":
                        user.CalculateTotalAmountPaid(out DateTime endDate);
                        Section2();
                        break;
                    case "4":
                        var schedule = user.GenerateAmortizationSchedule();
                        PrintAmortizationSchedule(schedule);
                        Section2();
                        break;
                    case "5":
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Incorrect Input, Try again.");
                        Console.ResetColor();
                        Section2();
                        break;
                }
            }

       
        }

    }
}


