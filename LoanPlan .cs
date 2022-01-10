using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LoanManagmentSystem
{
    class LoanPlan
    {
        public int loanPlanInMonth;
        public double loanInterestInPercent;
        public double loanOverDuePenalty;
        public LoanPlan()
        {

        }

        public void createLoanPlans() {

            Console.WriteLine("==================Create Loan Plan============================");
            Console.Write("Enter Plan in Month :    Month\b\b\b\b\b\b\b\b\b");
            var plan = Console.ReadLine();
            loanPlanInMonth = int.Parse(plan);
            Console.WriteLine();
            Console.Write("Enter Loan Interset Percent(%):      %\b\b\b\b\b\b");
            var interest = Console.ReadLine();
            loanInterestInPercent = double.Parse(interest);
            Console.WriteLine();
            Console.Write("Enter Monthly Over Due's Penalty:      %\b\b\b\b\b\b");
            var OverDuePenalty = Console.ReadLine();
            loanOverDuePenalty = double.Parse(OverDuePenalty);

            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("\t\t\t Loan Plan ");
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Plan: {0} Month",plan);
            Console.WriteLine("Interest: {0} %",interest);
            Console.WriteLine("Monthly Over Due's Penalty: {0} %",OverDuePenalty);
            Console.WriteLine("---------------------------------------------------------");
            Console.Write("Are You Sure You Want To Save?[Y/N] : ");
            string Save = Console.ReadLine();
            Console.WriteLine(Save);

            switch (Save)
            {
                case "Y":
                case "y":

                    //create a file named loanPlans
                    //save data as a row
                    var line = loanPlanInMonth + " " + loanInterestInPercent + " " + loanOverDuePenalty;
                    File.AppendAllText(@"./loan_plan.txt", line + Environment.NewLine);


                    Console.WriteLine("Saved");
                    break;
                default:
                    Console.WriteLine("Not Saved");
                    break;
            }

            Console.Read();
        }

        public void viewLoanPlan()
        {
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("| ID |\tPlan\t|\t\tInterest\t|\tOver Due's Penalty\t|");
            Console.WriteLine("---------------------------------------------------------------------------------");
            List<string> rows = new List<string>();
            var tempoLine = "";
            foreach (char letter in File.ReadAllText("./loan_plan.txt"))
            {
                
                if (letter.Equals('\n'))
                {
                    rows.Add(tempoLine);
                    tempoLine = "";
                    //Console.WriteLine();
                }
                else
                {
                    tempoLine += letter;
                    //Console.Write(letter);
                }
                
            }

            var counter = 0;
            foreach(string line in rows)
            {
                counter++;
                var eachLine = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine("| {0} | \t {1} \t\t\t {2} \t\t\t {3}",counter, eachLine[0], eachLine[1], eachLine[2]);
            }
            Console.WriteLine("---------------------------------------------------------------------------------");

        }

        public string getLoanPlan(int ID)
        {
            string result = "";
            List<string> rows = new List<string>();
            var tempoLine = "";
            foreach (char letter in File.ReadAllText("./loan_plan.txt"))
            {

                if (letter.Equals('\n'))
                {
                    rows.Add(tempoLine);
                    tempoLine = "";
                    //Console.WriteLine();
                }
                else
                {
                    tempoLine += letter;
                    //Console.Write(letter);
                }

            }

            var counter = 0;
            foreach (string line in rows)
            {
                counter++;
                var eachLine = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (counter == ID)
                {
                  result = counter+" "+eachLine[0]+" "+eachLine[1]+" "+eachLine[2];

                }
               
            }
            

            return result;

        }
    }
}
