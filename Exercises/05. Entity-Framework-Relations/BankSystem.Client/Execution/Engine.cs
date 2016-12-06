namespace BankSystem.Client.Execution
{
    using Data;
    using Models;
    using OI;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity.Validation;
    using Models.BankingAccounts;

    public class Engine : IEngine
    {
        private User currentUser;

        public Engine(BankSystemContext db, IConsoleReader reader, IConsoleWriter writer)
        {
            this.Context = db;
            this.Reader = reader;
            this.Writer = writer;
            this.currentUser = null;
        }

        public BankSystemContext Context { get; private set; }

        public IConsoleReader Reader { get; private set; }

        public IConsoleWriter Writer { get; private set; }

        public void Run()
        {
            string inputLine = this.Reader.ReadLine();

            while (!inputLine.Equals("Exit"))
            {

                try
                {
                    if (this.currentUser == null)
                    {
                        ExecuteNonLoggedInUserCommand(inputLine);
                    }
                    else
                    {
                        ExecuteLoggedInUserCommand(inputLine);
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    DbEntityValidationResult lastResult = ex.EntityValidationErrors.Last();
                    
                    foreach (DbValidationError error in lastResult.ValidationErrors)
                    {
                        this.Writer.WriteLine(error.ErrorMessage);
                    }

                    this.Context.Users.Local.Clear();
                    this.Context.BankingAccounts.Local.Clear();
                }
                catch (Exception ex)
                {
                    this.Writer.WriteLine(ex.Message);
                }

                inputLine = this.Reader.ReadLine();
            }
        }

        private void ExecuteLoggedInUserCommand(string inputLine)
        {
            string[] commandTokens = inputLine.Split();
            string command = commandTokens[0];

            switch (command)
            {
                case "Add":
                    AddAccountCommand(commandTokens);
                    break;
                case "ListAccounts":
                    ListAccountsCommand();
                    break;
                case "Deposit":
                    DepositCommand(commandTokens);
                    break;
                case "Withdraw":
                    WithdrawCommand(commandTokens);
                    break;
                case "DeductFee":
                    DeductFeeCommand(commandTokens);
                    break;
                case "AddInterest":
                    AddInterestCommand(commandTokens);
                    break;
                case "Logout":
                    this.Writer.WriteLine($"User {this.currentUser.Username} successfully logged out");
                    this.currentUser = null;
                    break;
                default:
                    throw new InvalidOperationException("Invalid command!");
            }
        }

        private void AddInterestCommand(string[] commandTokens)
        {
            string accountNumber = commandTokens[1];

            SavingsAccount savingsAccount = this.Context.BankingAccounts
                .OfType<SavingsAccount>()
                .FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (savingsAccount == null)
            {
                throw new InvalidOperationException("No such savings account found in the database!");
            }

            savingsAccount.AddInterest();
            this.Context.SaveChanges();

            this.Writer.WriteLine($"Added interest to {savingsAccount.AccountNumber}. Current balance: {savingsAccount.Balance:F2}");
        }

        private void DeductFeeCommand(string[] commandTokens)
        {
            string accountNumber = commandTokens[1];

            CheckingAccount checkingAccount = this.Context.BankingAccounts
                .OfType<CheckingAccount>()
                .FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (checkingAccount == null)
            {
                throw new InvalidOperationException("No such checking account exists in the database!");
            }

            checkingAccount.DeductFee();
            this.Context.SaveChanges();

            this.Writer.WriteLine($"Deducted fee of {checkingAccount.AccountNumber}. Current balance: {checkingAccount.Balance:F2}");
        }

        private void WithdrawCommand(string[] commandTokens)
        {
            string accountNumber = commandTokens[1];
            decimal withdrawAmount = decimal.Parse(commandTokens[2]);

            BankingAccount bankAccount = this.Context.BankingAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (bankAccount == null)
            {
                throw new InvalidOperationException("Invalid account number!");
            }

            bankAccount.WithdrawMoney(withdrawAmount);
            this.Context.SaveChanges();

            this.Writer.WriteLine($"Account {bankAccount.AccountNumber} has balance of {bankAccount.Balance}");
        }

        private void DepositCommand(string[] commandTokens)
        {
            string accountNumber = commandTokens[1];
            decimal depositAmount = decimal.Parse(commandTokens[2]);

            BankingAccount bankAccount = this.Context.BankingAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            if (bankAccount == null)
            {
                throw new InvalidOperationException("Invalid account number!");
            }

            bankAccount.DepositMoney(depositAmount);
            this.Context.SaveChanges();

            this.Writer.WriteLine($"Account {bankAccount.AccountNumber} has balance of {bankAccount.Balance}");
        }

        private void ListAccountsCommand()
        {
            IEnumerable<SavingsAccount> savingsAccounts = this.Context
                .BankingAccounts.OfType<SavingsAccount>()
                .Where(a => a.Owner.Username == this.currentUser.Username);
            IEnumerable<CheckingAccount> checkingAccounts = this.Context
                .BankingAccounts.OfType<CheckingAccount>()
                .Where(a => a.Owner.Username == this.currentUser.Username);

            this.Writer.WriteLine("Saving Accounts:");

            foreach (SavingsAccount savingAccount in savingsAccounts
                .OrderBy(a => a.AccountNumber))
            {
                this.Writer.WriteLine($"--{savingAccount.AccountNumber} {savingAccount.Balance:F2}");
            }

            this.Writer.WriteLine("Checking Accounts:");
            foreach (CheckingAccount checkingAccount in checkingAccounts
                .OrderBy(a => a.AccountNumber))
            {
                this.Writer.WriteLine($"--{checkingAccount.AccountNumber} {checkingAccount.Balance:F2}");
            }

        }

        private void AddAccountCommand(string[] commandTokens)
        {
            string accountType = commandTokens[1];

            switch (accountType)
            {
                case "SavingsAccount":
                    CreateSavingAccount(commandTokens);
                    break;
                case "CheckingAccount":
                    CreateCheckingAccount(commandTokens);
                    break;
                default:
                    throw new InvalidOperationException("Invalid command!");
            }
        }

        private void CreateCheckingAccount(string[] commandTokens)
        {
            string accountNumber = RandomStringGenerator.GenerateRandomAccountNumber();
            decimal balance = decimal.Parse(commandTokens[2]);
            decimal fee = decimal.Parse(commandTokens[3]);

            this.Context.BankingAccounts.Add(new CheckingAccount()
            {
                AccountNumber = accountNumber,
                Balance = balance,
                Fee = fee,
                Owner = this.currentUser
            });

            this.Context.SaveChanges();
            this.Writer.WriteLine($"Succesfully added account with number {accountNumber}");
        }

        private void CreateSavingAccount(string[] commandTokens)
        {
            string accountNumber = RandomStringGenerator.GenerateRandomAccountNumber();
            decimal balance = decimal.Parse(commandTokens[2]);
            decimal interest = decimal.Parse(commandTokens[3]);


            this.Context.BankingAccounts.Add(new SavingsAccount()
            {
                AccountNumber = accountNumber,
                Balance = balance,
                InterestRate = interest,
                Owner = this.currentUser
            });

            this.Context.SaveChanges();

            this.Writer.WriteLine($"Succesfully added account with number {accountNumber}");
        }

        private void ExecuteNonLoggedInUserCommand(string inputLine)
        {
            string[] commandTokens = inputLine.Split();
            string command = commandTokens[0];

            switch (command)
            {
                case "Register":
                    RegisterUser(commandTokens);
                    break;
                case "Login":
                    LoginUser(commandTokens);
                    break;
                case "Logout":
                    this.Writer.WriteLine("Cannot log out. No user was logged in.");
                    break;
                default:
                    throw new InvalidOperationException("Invalid command!");
            }
        }

        private void LoginUser(string[] commandTokens)
        {
            string username = commandTokens[1];
            string password = commandTokens[2];

            bool userExists = this.Context.Users
                .Any(u => u.Username.Equals(username) && u.Password.Equals(password));
            if (!userExists)
            {
                throw new InvalidOperationException("Incorrect username / password");
            }

            this.currentUser = this.Context.Users.First(u => u.Username == username && u.Password == password);
            this.Writer.WriteLine($"Succesfully logged in {username}");
        }

        private void RegisterUser(string[] commandTokens)
        {
            string username = commandTokens[1];
            string password = commandTokens[2];
            string email = commandTokens[3];

            //User user = (User)typeof(User)
            //    .GetConstructor(new[] { typeof(string), typeof(string), typeof(string) })
            //    .Invoke(new object[] { username, password, email });

            User existingUser = this.Context.Users.FirstOrDefault(u => u.Username == username && u.Password == password && u.Email == email);
            if (existingUser != null)
            {
                throw new InvalidOperationException($"User {username} already exists in the database!");
            }

            this.Context.Users.Add(new User()
            {
                Id = 0,
                Username = username,
                Password = password,
                Email = email
            });

            this.Context.SaveChanges();


            this.Writer.WriteLine($"{username} was registered in the system");
        }
    }
}
