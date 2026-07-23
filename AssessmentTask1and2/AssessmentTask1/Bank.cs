using System;
using System.Collections.Generic;
using Functions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentTask1
{
    // Base class to define shared properties and behavior for all user types
    public abstract class User
    {
        // Common properties for all users
        public string Username { get; set; } // Stores username
        public string Password { get; set; } // Stores Password
        public decimal Balance { get; set; } = 0; // Store user balance

        // Method to be defined by the child class
        public abstract void Login(); // Forces child classes to define their own behavior
    }

    // Subclass of user for regular status users
    public class RegularAccount : User
    {
        // Override the absract login in base class 'User' for regular users.
        public override void Login()
        {
            Console.WriteLine("Regular user logging in...");
        }
    }

    // Subclass of user for premium status users
    public class PremiumAccount : User
    {
        // Override the absract login in base class 'User' for premium users.
        public override void Login()
        {
            // Unique welcome for premium users
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
__        __   _
\ \      / /__| | ___ ___  _ __ ___   ___
 \ \ /\ / / _ \ |/ __/ _ \| '_ ` _ \ / _ \
  \ V  V /  __/ | (_| (_) | | | | | |  __/
   \_/\_/ \___|_|\___\___/|_| |_| |_|\___|");
            Console.WriteLine("\nValued Premium Member!");
            Console.ResetColor();
        }

        // Unique benefits for premium users of the program 
        public void PremiumInterest()
        {
            decimal interestRate = 0.02m; // 2% interest for premium users.
            decimal earned = Balance * interestRate;
            Balance += earned;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nInterest bonus applied! You earned ${earned:F2}. New Balance: ${Balance:F2}");
            Console.ResetColor();
        }
    }

    public class Bank
    {
        // Initialize a dictionary that hold passwords and usernames
        private readonly Dictionary<string, User> usersLogin = new Dictionary<string, User>();

        // Constructor for Bank
        public Bank()
        {
            usersLogin.Add("Joe.Doe", new RegularAccount { Username = "Joe.Doe", Password = "Password123"});
        }

        // Starts the banking system
        public void Start()
        {
            // Initialize an infinite loop that only exits when the user is active
            while (true)
            {
                // Get user input for later use
                Console.ResetColor(); // Microsoft (N.d). -- and any other use of this command
                Console.WriteLine("\nWelcome to NSW Australia Bank!\n1: Login\n2: Sign-Up\n3: Quit\n");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Please choose 1, 2 or 3 to continue...");
                string userChoice = Console.ReadLine();

                // Input logic leading to Login, SignUp or Exiting
                switch (userChoice)
                {
                    case "1":
                        Login(); // Call the Login function
                        break;
                    case "2":
                        SignUp(); // Call the SignUp function
                        break;
                    case "3":
                        Console.ForegroundColor= ConsoleColor.Green;
                        Console.WriteLine("\nThanks for using our service!");
                        Console.ResetColor();
                        Console.ReadKey();
                        Environment.Exit(0); // Close out of the program if chosen
                        break;
                    default: // Handle edge case errors that do not include 1, 2 or 3
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nThat wasn't one of your options! Returning to main menu...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        // Task 1: Handles user login functionality
        private void Login()
        {
            // Initialize value that holds attempts
            int attempts = 0;

            // Create an instance of the Functions class to allow the use of the Functions.cs File
            Functions.Functions functions = new Functions.Functions();

            // Create a loop that only closes after attempts reach 3
            while (attempts < 3)
            {
                // Retreive Login credentials (username and password) from user
                string userNameAttempt = functions.GetUserInput("\nEnter your username: ");
                string passwordAttempt = functions.GetUserInput("\nEnter your password: ");

                // Check if the username exists is in the dictionary and if the password matches
                if (usersLogin.TryGetValue(userNameAttempt, out User users) && users.Password == passwordAttempt)  
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"\nWelcome {userNameAttempt}");
                    // Call the polymorphic login method which determines beaviour
                    // based on whther the user is a RegularAccount or PremiumAccount
                    users.Login();

                    while(true)
                    {
                        Console.ResetColor();
                        // Display menu options
                        Console.WriteLine("\nMenu:\n1: View Balance\n2: Deposit\n3: Withdraw\n4: Logout\n5: Quit\n");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Choose an option listed: ");
                        string choice = Console.ReadLine();

                        // Decision making logic
                        switch (choice)
                        {
                            case "1": 
                                if (users is PremiumAccount premiumUser) // Execute only if the user is a premium user
                                {   
                                    if (premiumUser.Balance > 0)
                                    {
                                        premiumUser.PremiumInterest(); // Apply bonus before showing balance
                                    }
                                    else
                                    {
                                        Console.ForegroundColor= ConsoleColor.Yellow;
                                        Console.WriteLine("\nYour balance is $0. No interest will be applied.");
                                        Console.ResetColor();
                                    }
                                }

                                ViewBalance(users);
                                break;
                            case "2":
                                Deposit(users); 
                                break;
                            case "3":
                                Withdraw(users);
                                break;
                            case "4": // Returning to main menu
                                Console.WriteLine("Logging out...");
                                Console.ReadKey();
                                Console.Clear();
                                return;
                            case "5": // Quitting the service
                                Console.ForegroundColor= ConsoleColor.Green;
                                Console.WriteLine("\nExiting the program! Thanks for using our service!\n");
                                Console.ResetColor();
                                Environment.Exit(0);
                                break;
                            default: // Handle edge-case errors outside of 1, 2, 3, 4 and 5.
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid choice. Please select an option from the main menu.");
                                break;
                        }
                    }
                }
                else
                {
                    // Increment attempts up 
                    attempts++;

                    // Display ammount of attempts left
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nInvalid Username or Password. Attempts left: {3 - attempts}\n");

                    // Logic block that occurs upon attempts equaling three
                    if (attempts == 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You've exceeded the maximum number of attempts. You will be locked out, please try again later!");
                        Console.ResetColor();
                        Console.ReadKey();
                        Environment.Exit(0); // Exit the program if they have used all attempts
                    }
                    else
                    {
                        HandleFailedLogin(); // Prompt again for retry or quit
                    }
                }
            }
        }

        // Task 2: Handles user sign-up functionality
        private void SignUp()
        {
            // Create an instance of the Functions class to help with user input
            Functions.Functions functions = new Functions.Functions();

            // Prompt user to enter a unique username
            string uniqueUser = functions.GetUserInput("\nEnter your username: ");

            // Validate username, ensuring it isn't empty or already exists
            if (!functions.IsValidUsername(uniqueUser) || usersLogin.ContainsKey(uniqueUser))
            {
                if (usersLogin.ContainsKey(uniqueUser))
                {
                    // If the username already exists in the dictionary
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nUsername is already taken. Please choose a different username");
                    Console.ReadKey();
                    Console.Clear();
                }
                return; // Stop further execution and return to main menu
            }
            // Prompt user to enter a password for their account
            string uniquePassword = functions.GetUserInput("\nEnter a password: ");

            // Check if the password is empty or contains only white space
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (string.IsNullOrWhiteSpace(uniquePassword))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nPassword cannot be empty. Please enter a valid password next time! Returning to main menu...");
                Console.ReadKey();
                Console.Clear();
                return;  // Stop further execution and return to the main menu
            }

            if (!functions.IsValidPassword(uniquePassword))
            {
                Console.ReadKey();
                Console.Clear();
                return;
            }

            // Proceed with email validation
            string userEmail = functions.GetValidEmail();
            if (userEmail != null)
            {
                // Proceed with age validation
                int validUserAge = functions.GetValidAge();
                if (validUserAge > 0)
                {
                    // Proceed with phone number validation
                    string phoneNumber = functions.GetValidPhoneNumber();
                    if (phoneNumber != null)
                    {
                        // Show previously entered information for confirmation before finalizing sign-in
                        functions.ShowUserDetails(uniqueUser, uniquePassword, userEmail, validUserAge, phoneNumber);

                        // Ask user to confirm or deny their entered details
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\nPlease confirm your details (C = Confirm or D = Deny): ");
                        string confirmDetails = Console.ReadLine().ToLower(); // ToLower for more consistency
                       
                        if (confirmDetails == "c" || confirmDetails == "confirm")
                        {
                            // Prompt user to chooose their desired account type
                            Console.Write("\nAre you signing up as a Regular or Premium user? (Type 'Regular' or 'Premium')\n");
                            string accountType = Console.ReadLine().ToLower(); // Convert input to lowercase for consistency

                            // Declare a User object to store new user data
                            User newUser;

                            // Check if user chose premium or regular
                            if (accountType == "premium")
                            {
                                Console.WriteLine("\nPremium Account Chosen.");
                                // Create a PremiumAccount object if user chooses 'premium'
                                newUser = new PremiumAccount();
                            }
                            else
                            {
                                Console.WriteLine("\nRegular Account Chosen.");
                                // Otherwise, create a RegularAccount
                                newUser = new RegularAccount();
                            }

                            // Assign the unique user and password to the new user
                            newUser.Username = uniqueUser; // Set the new user's username
                            newUser.Password = uniquePassword; // Set the new user's password

                            // Add user to the dictionary after confirmation
                            usersLogin.Add(uniqueUser, newUser);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Your details have been verified. Please Log in!");
                            Console.ReadKey();
                            Console.Clear();
                            Login(); // Redirect to login after sign-in
                        }
                        else if (confirmDetails == "d" || confirmDetails == "deny") 
                        {
                            // Upon dinial return user with unique output for sign-up re-trial
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nPlease re-try the signup process with your correct information.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                        else // Edge case error handling for unexpected outputs
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nThat wasn't one of your options! Returning to main menu...");
                            Console.ReadKey();
                            Console.Clear();
                            return;
                        }
                    }
                }
            }
        }

        // Handles failed login attempt and provides option to retry or quit
        public void HandleFailedLogin()
        {
            Console.ResetColor();
            // Prompt user to continue or exit the login process
            Console.WriteLine("1: Try Again\n2: Quit\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Please choose 1 to try again or 2 to quit...");
            string invalidLogin = Console.ReadLine();

            // Switch case to handle user's choice after failed login
            switch (invalidLogin)
            {
                case "1":
                    return; // Continue login process if the user wants to try again
                case "2":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nThanks for using our service!");
                    Console.ReadKey();
                    Console.ResetColor();
                    Environment.Exit(0); // Exit the program if user chooses to
                    break;
                default:
                    // Handle any edge case inputs and return to main menu
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice. Returning to main menu...");
                    ReturnToMainMenu();
                    break;
            }

        }

        // After failed login or incorrect input
        public void ReturnToMainMenu()
        {
            Console.ReadKey();
            Console.Clear(); // Clear screen / De-clutter
            Start(); // Restarts and send user back to outer main menu
        }

        // Method that handles deposit
        private void Deposit(User user)
        {
            Console.ForegroundColor = ConsoleColor.Cyan; 

            // Prompt the user for the amount they want to deposit and validate if it's a positive numerical number
            Console.WriteLine("\nEnter the amount you want to deposit: "); 
            string inputedDeposit = Console.ReadLine();
            if (decimal.TryParse(inputedDeposit, out decimal validAmount) && validAmount > 0)
            {
                user.Balance += validAmount; // Add the deposited value to their Balance

                user.Balance = Math.Round(user.Balance, 2); // Rounding

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nSuccessfully deposited ${validAmount:F2}. New balance: ${user.Balance}");
            }
            else // Handle edge-case errors that aren't numerical (1 - 9)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nInvalid amount. Please enter a positive numerical value.");
            }
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();

        }

        // Method that handles Withdrawal
        private void Withdraw(User user)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            // Prompt the user to withdraw a valid amount and parsing to ensure a valid input. numerical with decimal or not
            Console.WriteLine("\nEnter the amount you want to withdraw: ");
            string inputedWithdrawel = Console.ReadLine();
            if (decimal.TryParse(inputedWithdrawel, out decimal validAmount) && validAmount > 0)
            {
                if (user.Balance >= validAmount)
                {
                    user.Balance -= validAmount; // Remove the users input if valid from their balance.
                    Console.ForegroundColor = ConsoleColor.Green;
                    // Showcase their new balance and how much they've withdrawn
                    Console.WriteLine($"\nSuccessfully withdrew ${validAmount}. New balance: ${user.Balance:F2}");
                }
                else // Handle edge-case errors that are not in their current balance
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nNot sufficient funds available.");
                }
            }
            else // If the user had entered a letter or special character that doesn't build toward a numerical value
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nOnly positive numbers may be used for a valid withdrawal.");
            }
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }

        // Showcases the users balance as a whole without any further purpose.
        private void ViewBalance(User user)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nCurrent Balance: ${user.Balance:F2}");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }
    }
}
