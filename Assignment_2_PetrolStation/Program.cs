using System;
using System.Timers;

namespace PetrolStation
{
    // Done by: [Hamza koya]
    // SID: [1922493]

    internal class Program
    {
        private static void Main(string[] args)
        {
            char exit;
            
            do
            {
                Data.Initialise();
                Random numberGenerator = new Random();
                Timer timer = new Timer
                {
                    Interval = 2050,
                    AutoReset = true // repeats every 2 seconds
                };
                timer.Elapsed += RunProgramLoop;
                timer.Enabled = true;
                timer.Start();
                exit = Console.ReadKey().KeyChar.ToString().ToLower()[0];
                Console.Clear();

                if (exit == 'x')

                {
                    timer.Enabled = false;                 
                    Display.DrawTransactions();
                    Console.WriteLine();
                    Display.WriteToFile();
                    Console.ReadKey();
                }
            } while (exit != 'x');
        }

        /// <summary>
        /// Assigns the methods to run in the main timer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void RunProgramLoop(object sender, ElapsedEventArgs e)
        {
            Data.AssignVehicleToPump();
            Console.Clear();
            Display.DrawVehicles();
            Console.WriteLine();
            Console.WriteLine();
            Display.DrawPumps();
            Console.WriteLine();
            Display.DrawInfo();
        }
    }
}