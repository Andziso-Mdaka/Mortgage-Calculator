using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formative_1
{

    public class MortgageCalculator
    {
        private double LoanAmount;
        private double AnnualInterestRate;
        private int LoanTermYears;
        private DateTime LoanStartDate;

        public MortgageCalculator(double LoanAmount, double AnnualInterestRate, int LoanTermYears, DateTime LoanStartDate)
        {

            this.LoanAmount = LoanAmount;
            this.AnnualInterestRate = AnnualInterestRate;
            this.LoanTermYears = LoanTermYears;
        }


        public double GetLoanAmount()
        {
            Console.WriteLine("How much is your total loan amount?");
            LoanAmount = Convert.ToDouble(Console.ReadLine());
            return LoanAmount;
        }

        public double GetAnnualInterestRate()
        {
            Console.WriteLine("What is your annual interest rate?");
            AnnualInterestRate = Convert.ToDouble(Console.ReadLine());
            return AnnualInterestRate;
        }

        public int GetLoanTermYears()
        {
            Console.WriteLine("What is the term of the loan in years?");
            LoanTermYears = Convert.ToInt32(Console.ReadLine());
            return LoanTermYears;
        }

        public DateTime GetLoanStartDate()
        {
            Console.WriteLine("When do you want to start the loan?");
            Console.WriteLine("Use YYYY/MM/DD Format");
            LoanStartDate = DateTime.Parse(Console.ReadLine());
            return LoanStartDate;


        }


        public double CalculateMonthlyRepayment()
        {
            double monthlyInterestRate = AnnualInterestRate / (100 * 12);
            double numberOfPayments = LoanTermYears * 12;

            double monthlyRepayment = LoanAmount * monthlyInterestRate / (1 - Math.Pow(1 + monthlyInterestRate, -numberOfPayments));
            Console.WriteLine($"Your monthly repayment amount is: {monthlyRepayment:C}");
            Console.WriteLine(" ");
            return monthlyRepayment;
        }

        public double CalculateTotalInterestPaid()
        {

            double totalInterestPaid = CalculateMonthlyRepayment() * LoanTermYears * 12 - LoanAmount;
            Console.WriteLine($"Total interest paid over the loan term is: {totalInterestPaid:C}");
            Console.WriteLine(" ");

            return totalInterestPaid;

        }

        public double CalculateTotalAmountPaid(out DateTime endDate)
        {
            double totalAmountPaid = LoanAmount + CalculateTotalInterestPaid();

            endDate = LoanStartDate.AddYears(LoanTermYears);
            Console.WriteLine($"Total amount paid over the loan term is: {totalAmountPaid:C}");
            Console.WriteLine($"You will finish paying on: {endDate:yyyy/MM/dd}");
            Console.WriteLine(" ");
            return totalAmountPaid;
        }





        static void Main(string[] args)
        {
            MortgageCalculator user = new MortgageCalculator(1, 1, 1, DateTime.Now);

            Console.WriteLine("Mortgage Calculator");
            Console.WriteLine("1. Start");
            Console.WriteLine("2. Exit");
            String ans = Console.ReadLine();

            if (ans == "1")
            {
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
                System.Environment.Exit(0);
            }

            else
            {
                Console.WriteLine("Incorrect Input, Try again.");
                Main(args);
            }

            Section2();

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
                        Console.WriteLine("Amortization schedule feature is not implemented yet.");
                        Section2();
                        break;
                    case "5":
                        System.Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Incorrect Input, Try again.");
                        Section2();
                        break;
                }
            }

        



        }


    }
}
