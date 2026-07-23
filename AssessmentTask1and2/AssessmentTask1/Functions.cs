using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Functions
{
    public class Functions
    {
        // Method to display message and retreieve user input
        public string GetUserInput(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.Cyan; // Microsoft (N.d). -- and any other use of this command
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        // Check if username is not empty or contains any white spaces
        public bool IsValidUsername(string username)
        {
            // Check if username is empty or has only excessive amount of spaces
            if (string.IsNullOrWhiteSpace(username))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                // If invalid. Inform the user of the issue
                Console.WriteLine("Username can not be empty. Returning to main menu...");
                Console.ReadKey();
                Console.Clear();
                return false; // Indicates the username is not valid
            }
            else if (username.Any(char.IsDigit))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nUsername cannot contain numbers. Returning to main menu...");
                Console.ReadKey();
                Console.Clear();
                return false;
            }
                return true;
        }

        // Validates if the email ends with a valid domain
        public string GetValidEmail()
        {
            // Retreive user email and convert it to lowercase for consistency
            string email = GetUserInput("\nEnter your Email: ").ToLower();

            int atSymbolIndex = email.IndexOf("@");
            if (atSymbolIndex <= 0 || atSymbolIndex == email.Length - 1)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("\nInvalid email. The email cannot start with '@' and must have a word before it");
                Console.ReadKey();
                Console.Clear();
                Console.ResetColor();
                return null;
            }

            // Logic block checking for valid domains (hotmail, gmail, or outlook)
            if (email.EndsWith("@hotmail.com") || email.EndsWith("@gmail.com") || email.EndsWith("@outlook.com"))
            {
                // Prompt to confirm the email
                string emailConfirmation = GetUserInput("Re-enter your Email: ").ToLower();
                // Compare previous email to confirmation input
                if (email == emailConfirmation) return email; // If matching, then proceed the process
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    // If not matching then output an error message and return to main menu
                    Console.WriteLine("\nEmail confirmation failed. The previous email needs to match. Returning to main menu...");
                    Console.ReadKey();
                    Console.Clear();
                    return null; // Indicate an invalid email
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                // If they do not match, show an error message and return to main menu
                Console.WriteLine("\nInvalid email. Please use a valid email address from Gmail. Hotmail or Outlook. Returning to main menu...");
                Console.ReadKey();
                Console.Clear();
                return null; // Indicate an invalid email
            }
        }

        // Check if user input is valid, positive age
        public int GetValidAge()
        {
            // Retreive user input for age
            string ageInput = GetUserInput("\nEnter your age: ");

            // Try to convert previous input to an integer and check if positive
            if (int.TryParse(ageInput, out int age) && age > 1)
            {
                // If valid, return age
                return age;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                // If invalid, return an error message and return to main menu
                Console.WriteLine("\nInvalid age. Age must be a positive number. Returning to main menu...");
                Console.ReadKey();
                Console.Clear();
                return -1; // Return -1 to indicate invalid age
            }
        }

        // Check if users phone number is ten numbers long and starts with "04"
        public string GetValidPhoneNumber()
        {
            // Retreive user input for phone number
            string phone = GetUserInput("\nPhone Number: ");

            // Check if phone number contains only numerics, starts woth "04", and has a length of 10
            if (long.TryParse(phone, out long _) && phone.StartsWith("04") && phone.Length == 10)
            {
                // If valid, return the phone number
                return phone;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                // If invalid, show an error message and return to main menu
                Console.WriteLine("\nPhone numbers must include ten digits and start with '04'. Returning to main menu...");
                Console.ReadKey();
                Console.Clear();
                return null; // Return null indicating and invalid number
            }
        }

        // Show the user details during signup confirmation
        public void ShowUserDetails(string username, string password, string email, int age, string phone)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            // Formatted output of their details
            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine($"Username:\t{username}");
            Console.WriteLine($"Password:\t{password}");
            Console.WriteLine($"Email:\t\t{email}");
            Console.WriteLine($"Age:\t\t{age}");
            Console.WriteLine($"Phone Number:\t{phone}");
            Console.WriteLine("----------------------------------------");
        }

        public bool IsValidPassword(string password)
        {
            // Regular expression for strong password:
            // - At least one lowercase letter
            // - At least one uppercase letter
            // - At least one number
            // - At least one special character
            // - Minimum length of 8 characters
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*(),.?""':{}|<>])[A-Za-z\d!@#$%^&*(),.?""':{}|<>]{8,}$";


            // Match password with the regex pattern
            if (Regex.IsMatch(password, passwordPattern))
            {
                return true; // Password is strong
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nPassword must contain at least one uppercase letter, one lowercase letter, one number, one special character, and be at least 8 characters long.");
                Console.ResetColor();
                return false; // Password is not strong
            }
        }

    }
}
