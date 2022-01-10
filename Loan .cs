using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LoanManagmentSystem
{
    class Loan
    {

        public string borrowerFirstName;
        public string borrowerLastName;
        public string borrowerMiddleName;
        public string borrowerSex;
        public string borrowerAddres;
        public string borrowerPhone;
        public string loanPlan;
        public double borrowerSalary;
        public string loanType;
        public string loanPurpose;
        public double loanAmount;
        public string LoanGrant;

        double TotalPayableAmount;
        double MonthlyPayableAmount;
        double MonthlyPenalty;
        string RejectionReason;

        public Loan()
        {
            viewLoanList();
        }

        public void requestLoan()
        {
            //username
            //job position
            //loan plan
            //loan type
            //purpose
            //provide salary and document

            //Enter Personal Information
            try {
                Console.WriteLine("|-----------------------------------------------------------------------|");
                Console.WriteLine("|\t\t\t\tRequest Loan \t\t\t\t|");
                Console.WriteLine("|-----------------------------------------------------------------------|");

                Console.Write("Enter Borrower's First Name: ");
                borrowerFirstName = Console.ReadLine();

                Console.Write("Enter Borrower's Last Name: ");
                borrowerLastName = Console.ReadLine();

                Console.Write("Enter Borrower's Middle Name: ");
                borrowerMiddleName = Console.ReadLine();

                Console.Write("Enter Sex: [ Male | Female ]:");
                borrowerSex = Console.ReadLine();

                Console.Write("Enter Address: ");
                borrowerAddres = Console.ReadLine();

                Console.Write("Enter Phone: ");
                borrowerPhone = Console.ReadLine();


                //Enter Loan Information

                Console.Write("Enter Monthly Salary: ");
                var borrowerSalaryTemp = Console.ReadLine();
                double.TryParse(borrowerSalaryTemp, out borrowerSalary);



                Console.WriteLine("Enter Loan Type [Notice: Use Their ID For Selection]: ");
                new LoanType().listLoanType();
                loanType = Console.ReadLine();

                Console.WriteLine("Enter Loan Plan [Notice: Use Their ID For Selection]: ");
                new LoanPlan().viewLoanPlan();
                loanPlan = Console.ReadLine();

                Console.Write("Enter Loan Amount: ");
                double.TryParse(Console.ReadLine(), out loanAmount);

                Console.Write("Enter Loan Purpose: ");
                loanPurpose = Console.ReadLine();

                //Console.Read();
                //Calculate for Grant

                //Console.WriteLine(new LoanPlan().getLoanPlan(int.Parse(loanPlan)));
                string response = new LoanPlan().getLoanPlan(int.Parse(loanPlan));

                Console.WriteLine("To Calculate Loan Details [HIT Enter Key]");
                Console.Read();
                calculateLoanGrant(loanAmount, Convert.ToDouble(response.Split(" ")[2]), Convert.ToInt32(response.Split(" ")[1]), Convert.ToDouble(response.Split(" ")[3]));
                policyAndConditions(loanAmount, Convert.ToDouble(response.Split(" ")[2]), Convert.ToInt32(response.Split(" ")[1]), Convert.ToDouble(response.Split(" ")[3]));


            }
            catch (Exception e)
            {
                Console.WriteLine("There Has Been An Error.Please Come back later...");
                Console.Read();
            }
            


        }

        public void calculateLoanGrant(double loanAmount, double interestRate, int totalMonth, double OverDuesPenaltyRate)
        {

            //Console.WriteLine("{0} {1} {2} {3}",interestRate,totalMonth,OverDuesPenaltyRate,loanAmount);
            //formula A = P (r (1+r)^n) / ( (1+r)^n -1 )
            double monthlyRate = interestRate / 12;
            MonthlyPayableAmount = loanAmount * (monthlyRate * (double)Math.Pow((1 + monthlyRate), totalMonth)) / ((double)Math.Pow((1 + monthlyRate), totalMonth) - 1);
            OverDuesPenaltyRate = OverDuesPenaltyRate / 12;
            OverDuesPenaltyRate = OverDuesPenaltyRate / 100;
            TotalPayableAmount = MonthlyPayableAmount * totalMonth;
            MonthlyPenalty = loanAmount * OverDuesPenaltyRate;
            
            Console.WriteLine("|-----------------------------------------------------------------------|");
            Console.WriteLine("\t\t\tLoan Detail");
            Console.WriteLine("|-----------------------------------------------------------------------|");
            Console.WriteLine("|\t Total Payable Amount : {0} BIRR",TotalPayableAmount);
            Console.WriteLine("|\t Monthly Payable Amount: {0} BIRR",MonthlyPayableAmount);
            Console.WriteLine("|\t Over Due's Penalty Amount: {0} BIRR",MonthlyPenalty);
            Console.WriteLine("|-----------------------------------------------------------------------|");
            Console.Read();
        }


        public void calculateRepaymentSchedule()
        {
            //depending on loan plan

        }

        public void policyAndConditions(double loanAmount, double interestRate, int totalMonth, double OverDuesPenaltyRate)
        {
            Console.WriteLine("|-----------------------------------------------------------------------|");
            Console.WriteLine("|\t\t\t\t Terms And Policy \t\t\t\t|");
            Console.WriteLine("|-----------------------------------------------------------------------|");
            Console.WriteLine("| According to the Terms and Policy Mr/Ms {0} requested a loan and he/she Must agree to the terms and policy to get this Loan.",borrowerFirstName+" "+borrowerLastName);
            Console.WriteLine("| 1.The Borrower Should Receive the Loan according to the date defined by the Loaner");
            Console.WriteLine("| 2.The Borrower Requested a loan to {0} BIRR",loanAmount);
            Console.WriteLine("| 3.The Borrower Should Agree to {0} % Interest Rate.",interestRate);
            Console.WriteLine("| 4.The Borrower Must repay the loan in {0} Month or Before otherwise There Would be Penalty's.",totalMonth);
            Console.WriteLine("| 5.The Borrower Should Pay Monthly Payment of {0} BIRR.",MonthlyPayableAmount);
            Console.WriteLine("| 6.If Borrower Didn't Pay his/her Monthly Payment in Time they Must pay an Penalty of {0} BIRR.", MonthlyPenalty);
            Console.WriteLine("| 7.By the End of the Term the Borrower Should pay Total Money of {0} BIRR", TotalPayableAmount);
            Console.WriteLine("|-----------------------------------------------------------------------|");

            Console.WriteLine("Do The Borrower Agree To The Terms And Conditions ?[Y/N]: ");
            var Choice = Console.ReadLine();


            switch (Choice)
            {
                case "Y":
                case "y":


                    Console.WriteLine("Do You[Administrator] Approve The Loan ?[Y/N]: ");

                    var LoanerChoice = Console.ReadLine();
                    switch (LoanerChoice)
                    {
                        case "Y":
                        case "y":
                            //save to file
                            LoanGrant = "GRANTED";
                            AddLoanInformation(null);
                            new Customers().AddCustomer(borrowerFirstName, borrowerLastName, borrowerMiddleName, borrowerSex, borrowerAddres, borrowerPhone);
                            Console.WriteLine("|-----------------------------------------------------------------------|");
                            Console.WriteLine("| Loan Has be GRANTED SUCCESSFULLY.Congradulations Mr/Ms {0}...!", borrowerFirstName + " " + borrowerLastName);
                            Console.WriteLine("|-----------------------------------------------------------------------|");

                            break;
                        default:
                            LoanGrant = "REJECTED";
                            Console.WriteLine("Please Provide a Loan REJECTION reason : ");
                            RejectionReason = Console.ReadLine();
                            AddLoanInformation(RejectionReason);
                            new Customers().AddCustomer(borrowerFirstName, borrowerLastName, borrowerMiddleName, borrowerSex, borrowerAddres, borrowerPhone);
                            Console.WriteLine("Rejection Reason Has been  Saved.");
                            break;
                    }
                    break;
                default:
                    LoanGrant = "REJECTED";
                    Console.WriteLine("Please Provide a Loan REJECTION reason : ");
                    RejectionReason = Console.ReadLine();
                    AddLoanInformation(RejectionReason);
                    new Customers().AddCustomer(borrowerFirstName, borrowerLastName, borrowerMiddleName, borrowerSex, borrowerAddres, borrowerPhone);
                    Console.WriteLine("The Loan Has Been REJECTED.");
                    break;
            }
            

        }
        
        public void AddLoanInformation(string RejectReason)
        {
            //Loan Information
            string line = "";
            if (RejectReason != null)
            {
                line = borrowerFirstName + "|" + borrowerLastName + "|" + borrowerMiddleName + "|" + borrowerPhone + "|" + borrowerSalary + "|" + loanType + "|" + loanPlan + "|" + loanPurpose + "|" + loanAmount + "|" + TotalPayableAmount + "|" + MonthlyPayableAmount + "|" + MonthlyPenalty + "|" + LoanGrant + "|" + RejectReason;
                File.AppendAllText(@"./active_loans.txt", line + Environment.NewLine);
            }
            else
            {
                line = borrowerFirstName + "|" + borrowerLastName + "|" + borrowerMiddleName + "|" + borrowerPhone + "|" + borrowerSalary + "|" + loanType + "|" + loanPlan + "|" + loanPurpose + "|" + loanAmount + "|" + TotalPayableAmount + "|" + MonthlyPayableAmount + "|" + MonthlyPenalty + "|" + LoanGrant;
                File.AppendAllText(@"./active_loans.txt", line + Environment.NewLine);

            }
        }
        public void viewLoanList()
        {
            //Loan granted date
            //loan type
            //loan plan
            //interest
            //monthly payment
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| \t\t\t List of Loans ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            List<string> rows = new List<string>();
            var tempoLine = "";
            foreach (char letter in File.ReadAllText("./active_loans.txt"))
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

                Console.WriteLine("----[ {0} ]-----------------------------------------------------------------------------------", eachLine[0]+" "+ eachLine[1]);
                Console.WriteLine("{0,-5}: {1} ", "ID", counter);
                Console.WriteLine("{0,-5}: {1} ", "First Name", eachLine[0]);
                Console.WriteLine("{0,-5}: {1} ", "Last Name", eachLine[1]);
                Console.WriteLine("{0,-5}: {1} ", "Middle Name", eachLine[2]);
                Console.WriteLine("{0,-5}: {1} ", "Phone", eachLine[3]);
                Console.WriteLine("{0,-5}: {1} ", "Salary", eachLine[4]);
                Console.WriteLine("{0,-5}: {1} ", "Loan Type", eachLine[5]);
                Console.WriteLine("{0,-5}: {1} ", "Loan Plan", eachLine[6]);
                Console.WriteLine("{0,-5}: {1} ", "Loan Purpose", eachLine[7]);
                Console.WriteLine("{0,-5}: {1} ", "Loan Amount", eachLine[8]);
                Console.WriteLine("{0,-5}: {1} ", "Total Loan Payment", eachLine[9]);
                Console.WriteLine("{0,-5}: {1} ", "Monthly Payment", eachLine[10]);
                Console.WriteLine("{0,-5}: {1} ", "Over Due's Penalty", eachLine[11]);
                Console.WriteLine("{0,-5}: {1} ", "Loan Status", eachLine[12]);



                Console.WriteLine("---------------------------------------------------------------------------------------------");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Press Enter To Continue...");
            Console.Read();


        }

        public void viewApprovedLoan()
        {

            //username
            //loan type
            //loan plan
            //interest
        }

        public void viewRejetedLoan()
        {
            //username
            //loan type
            //rejection reason

        }
    }
}
