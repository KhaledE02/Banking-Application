using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentTask1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the Bank class and start the program, while also inputting dummyUser
            Bank bank = new Bank();
            bank.Start();
        }
    }
}

/* Reference List:
 *     - Microsoft. (2025, March 19). Explore object oriented programming with classes and objects. Microsoft. https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/tutorials/classes
 *     // Modularization/Segmentation of work learnt from above. Date accessed. 29/03/2025
 *     - Microsoft. (N.d). Console.ForegroundColor Property. Microsoft. https://learn.microsoft.com/en-us/dotnet/api/system.console.foregroundcolor?view=net-9.0
 *     // Date accessed. 31/03/2025
 */