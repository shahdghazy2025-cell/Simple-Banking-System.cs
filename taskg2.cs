using System;

class Program
{

    static int transactionCount = 0;
    static decimal totalDeposits = 0;
    static decimal totalWithdrawals = 0;

    static int ShowMenu()
    {
        Console.WriteLine("========== BANK MENU ==========");
        Console.WriteLine("1. Check Balance");
        Console.WriteLine("2. Deposit Money");
        Console.WriteLine("3. Withdraw Money");
        Console.WriteLine("4. View Transaction Summary");
        Console.WriteLine("5. Exit");

        int option;
        Console.Write("Choose an option: ");
        string input = Console.ReadLine();

         while (!int.TryParse(input, out option) || option < 1 || option > 5)
        {
            Console.WriteLine("Invalid choice!");
            Console.Write("Choose an option: ");
            input = Console.ReadLine();
        }

        return option;
    }


    static void DisplayBalance(decimal balance)
{
        Console.WriteLine($"Current Balance: {balance} EGP");
        
}


    static void Deposit(ref decimal balance, decimal amount)
{
    if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException("amount", "Amount must be positive.");
        }

        balance += amount;

        totalDeposits += amount;
        transactionCount++;

}
    static void Withdraw(ref decimal balance, decimal amount)
{
         if (amount <= 0)
          {
            throw new ArgumentOutOfRangeException(
                "amount", "Amount must be positive.");
          }

         if (amount > balance)
          {
            throw new InvalidOperationException("Insufficient balance.");
         }

         balance -= amount;

          totalWithdrawals += amount;
          transactionCount++;
}


 static bool TryReadAmount(string input, out decimal amount)
    {
        if (decimal.TryParse(input, out amount) && amount > 0)
        {
            return true;
        }
        else
        {
            amount = 0;
            return false;
        }
    }
    static void DisplayTransactionSummary()
    {
        Console.WriteLine("\n===== Transaction Summary =====");
        Console.WriteLine($"Total Transactions: {transactionCount}");
        Console.WriteLine($"Total Deposits: {totalDeposits} EGP");
        Console.WriteLine($"Total Withdrawals: {totalWithdrawals} EGP");
    }

    static void Main()
    {
        string name;
        decimal balance;

        Console.Write("Enter your name: ");
        name = Console.ReadLine();

        while (true)
        {
            Console.Write("Enter initial balance: ");
            string balanceInput = Console.ReadLine();

            if (decimal.TryParse(balanceInput, out balance))
            {
                if (balance < 0)
                {
                    Console.WriteLine("Balance cannot be negative.");
                }
                else
                {
                    Console.WriteLine($"Account created successfully! Balance: {balance} EGP");
                    break;
                }
            }
            else
            {
                Console.WriteLine("Invalid balance. Please enter a valid number.");
            }
        }
         
         while (true){
        int choice = ShowMenu();
        switch (choice)
        {
             case 1:
                    DisplayBalance(balance);
                    break;
             case 2:
                    Console.Write("Enter deposit amount: ");
                    string depositInput = Console.ReadLine();
                    decimal depositAmount;
                     while (!TryReadAmount(depositInput, out depositAmount))
                    {
                        Console.WriteLine("Invalid amount!");
                        Console.Write("Enter deposit amount: ");
                        depositInput = Console.ReadLine();
                    }

                    try
                    {
                        Deposit(ref balance, depositAmount);

                        Console.WriteLine("Deposit successful!");
                        Console.WriteLine(
                            $"Current Balance: {balance} EGP");
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    finally
                    {
                        Console.WriteLine(
                            "Transaction attempt completed.");
                    }
                      break;

            
             case 3:
                    Console.Write("Enter withdrawal amount: ");
                    string withdrawInput = Console.ReadLine();

                    decimal withdrawAmount;
                  while (!TryReadAmount(withdrawInput, out withdrawAmount))
                    {
                        Console.WriteLine("Invalid amount!");
                        Console.Write("Enter withdrawal amount: ");
                        withdrawInput = Console.ReadLine();
                    }

                    try
                    {
                        Withdraw(ref balance, withdrawAmount);

                        Console.WriteLine("Withdrawal successful!");
                        Console.WriteLine(
                            $"Current Balance: {balance} EGP");
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
            
                    catch (InvalidOperationException ex)
                     when (ex.Message == "Insufficient balance.")
                      {
                       Console.WriteLine("Withdrawal rejected: Insufficient funds.");
                    }
                       catch (InvalidOperationException ex)
                    {
                     Console.WriteLine($"Invalid operation: {ex.Message}");
                     }
                    
                    finally
                    {
                        Console.WriteLine("Transaction attempt completed.");
                        Console.WriteLine("Returning to main menu...");
                    }

                    break;

             case 4:
                    DisplayTransactionSummary();
                    break;

             case 5:
                     Console.WriteLine("thank you for useing our machine Bank ");             
                     return;

        
    }
}
}
}
