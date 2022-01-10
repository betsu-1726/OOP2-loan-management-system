using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LoanManagmentSystem
{
    class LoanType
    {
        public string loanType;
        public string loanDescription;
        public LoanType() {
        
        }

        public void createLoanType()
        {
            //type
            //description


            Console.WriteLine("==================Create Loan Type============================");
            Console.Write("Enter Name of Plan Type: ");
            var planType = Console.ReadLine();
            loanType = planType.ToString();
            Console.WriteLine();
            Console.Write("Enter Loan Description: ");
            var description = Console.ReadLine();
            loanDescription = description.ToString();
            Console.WriteLine();
            
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("\t\t\t Loan Type ");
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Plan Type: {0} ", loanType);
            Console.WriteLine("Description: {0} ", loanDescription);
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
                    var line = loanType + "|" + loanDescription;
                    File.AppendAllText(@"./loan_type.txt", line + Environment.NewLine);
                    Console.WriteLine("Saved");
                    break;
                default:
                    Console.WriteLine("Not Saved");
                    break;
            }



        }

        public void listLoanType()
        {
            //#
            //loan type
            //description

            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("| ID |\tLoan Type\t\t|\t\t\t Loan Description");
            Console.WriteLine("---------------------------------------------------------------------------------");
            List<string> rows = new List<string>();
            var tempoLine = "";
            foreach (char letter in File.ReadAllText("./loan_type.txt"))
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
                var eachLine = line.Split("|", StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine("| {0} | \t {1} \t\t\t {2}", counter, eachLine[0], eachLine[1]);
            }
            Console.WriteLine("---------------------------------------------------------------------------------");





        }

    }
}
