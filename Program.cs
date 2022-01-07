using System;
using System.IO;

namespace LoanManagmentSystem
{
    class Program
    {
        public static Boolean LOGGED_IN = false;
        public static Boolean isAdminCreated = false;
        static void Main(string[] args)
        {
            Menu Menus = new Menu();
            Home home;
            Loan loan;
            LoanType loanType;
            LoanPlan loanPlan;
            Payment payment;
            Customers customers;
            Report report;
            

            Auth Administartor = new Auth();

                
            if (LOGGED_IN)
            {
                
                MenuselectionPoint:      //used for looping back to this point from the goto point
                Menus.menu1();
                Console.Write("Select Service : _\b");
                string choice = Console.ReadLine();
                
                try
                {
                    switch (int.Parse(choice))
                    {
                        case 1:
                            home = new Home();
                            break;
                        case 2:
                            loan = new Loan();
                            break;
                        case 3:
                            payment = new Payment();
                            break;
                        case 4:
                            loanPlan = new LoanPlan();
                            break;
                        case 5:
                            loanType = new LoanType();
                            break;
                        case 6:
                            customers = new Customers();
                            break;
                        default:
                            Console.WriteLine("No Service For This Selection.Please Use Numbers Between 1 - 6 only!");
                            goto MenuselectionPoint;

                    }
                }
                catch (System.FormatException e) {
                    Console.WriteLine("Wrong Input..Please Use Numbers only.Press Any Key To continue.For Exit Press 0");
                    var back = Console.ReadLine();
                    if (int.Parse(back) != 0) {
                        goto MenuselectionPoint;
                    }
                    
                
                }
                
            }
            else
            {
                
                    Console.Clear();
                    Administartor.LoginTrial();
            
            }
           
            Console.Read();
        }

    }
    
    
}
