using System;

namespace PetrolStation
{
    // Done by: [Hamza koya]
    // SID: [1922493]

    internal class Display
    {
        /// <summary>
        /// Displays vehicles that enters the queue.
        /// </summary>
        internal static void DrawVehicles()
        {
            Vehicle v;

            Console.WriteLine("VEHICLE QUEUE:".PadLeft(50));

            for (int i = 0; i < Data.Vehicles.Count; i++)
            {
                v = Data.Vehicles[i];
                Console.Write("#{0} {1}- {2} |     ", v.CarID, v.VehicleType, v.FuelType);
            }
        }

        /// <summary>
        /// Displays the 9 Pumps when they are free or occupied.
        /// </summary>
        public static void DrawPumps()
        {
            Pump p;
            Console.WriteLine("      ");

            Console.WriteLine("PUMPS STATUS:".PadLeft(50));
            Console.WriteLine();

            for (int i = 0; i < 9; i++)
            {
                p = Data.Pumps[i];

                Console.Write("   Pump #{0} ", i + 1);
                if (p.IsAvailable()) { Console.Write("   FREE              "); }
                else { Console.Write("   BUSY (#{0} {1}-{2})", p.CurrentVehicle.CarID, p.CurrentVehicle.VehicleType, p.CurrentVehicle.FuelType); }
                Console.Write(" | ");

                if (i % 3 == 2) { Console.WriteLine("\n"); }
            }
        }

        /// <summary>
        /// Displays the informations of the simulation while it is running.
        /// Displays cost, fuel and vehicle
        /// </summary>
        public static void DrawInfo()
        {
            Console.WriteLine("       __________________________________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(" Fuel price (per liter): Diesel:${0:0.00}   LPG:${1:0.00}    Unleaded:${2:0.00}".PadLeft(90), Data.FuelPriceDiesel, Data.FuelPriceLpg, Data.FuelPriceUnleaded);
            Console.WriteLine();

            Console.WriteLine("Queue: {0}".PadLeft(50), Data.Vehicles.Count);
            Console.WriteLine("Number of Vehicles that Appeared: {0}".PadLeft(50), Vehicle.NextCarID);
            Console.WriteLine("Number of Vehicles Serviced:: {0}".PadLeft(50), Data.VehiclesServiced);
            Console.WriteLine("Number of Vehicles Missed: {0}".PadLeft(50), Data.MissedVehicles);
            Console.WriteLine();
            Console.WriteLine("Total Unleaded dispensed: {0}".PadLeft(50), Data.UnleadedDispensed);
            Console.WriteLine("Total Diesel dispensed: {0}".PadLeft(50), Data.DieselDispensed);
            Console.WriteLine("Total LPG dispensed: {0}".PadLeft(50), Data.LpgDispensed);
            Console.WriteLine("Total Fuel dispensed: {0}".PadLeft(50), Data.Totalfuel);

            Console.WriteLine();
            Console.WriteLine("Total Sales: ${0:0.00}".PadLeft(60), Data.TotalSales);
            Console.WriteLine("Commission earned: ${0:0.00}".PadLeft(60), Data.Commission);
            Console.WriteLine();
            Console.WriteLine("Press  X  to Stop and view Transactions".PadLeft(60));
            Console.WriteLine();
        }

        /// <summary>
        /// Displays all the transactions that took place.
        /// </summary>
        public static void DrawTransactions() 
        {
            Console.WriteLine("Transactions:");

            for (int i = 0; i < Data.TransactionList.Count; i++)
            {
                Console.WriteLine(Data.TransactionList[i]);
            }
        }
       
        /// <summary>
        /// Method that saves a copy of the displayed data and all the transactions to a file.
        /// </summary>
        public static void WriteToFile()
        {
            try
            {
                using (var file = new System.IO.StreamWriter("TransactionsData.txt", true))

                {
                    file.WriteLine("       __________________________________________________________________________________________");
                    file.WriteLine();
                    file.WriteLine(DateTime.Now);
                    file.WriteLine();
                    file.WriteLine(" Fuel price (per liter): Diesel:${0:0.00}   LPG:${1:0.00}    Unleaded:${2:0.00}", Data.FuelPriceDiesel, Data.FuelPriceLpg, Data.FuelPriceUnleaded);
                    file.WriteLine();

                    file.WriteLine("Vehicle in the Queue: {0}", Data.Vehicles.Count);
                    file.WriteLine("Number of Vehicles that Appeared: {0}", Vehicle.NextCarID);
                    file.WriteLine("Number of Vehicles Serviced:: {0}", Data.VehiclesServiced);
                    file.WriteLine("Number of Vehicles Missed: {0}", Data.MissedVehicles);
                    file.WriteLine();
                    file.WriteLine("Total Unleaded dispensed: {0}", Data.UnleadedDispensed);
                    file.WriteLine("Total Diesel dispensed: {0}", Data.DieselDispensed);
                    file.WriteLine("Total LPG dispensed: {0}", Data.LpgDispensed);
                    file.WriteLine("Total Fuel dispensed: {0}", Data.Totalfuel);
                    file.WriteLine();
                    file.WriteLine("Total Sales: ${0:0.00}", Data.TotalSales);
                    file.WriteLine("Commission earned: ${0:0.00}", Data.Commission);
                    file.WriteLine();

                    file.WriteLine("Transactions:");

                    for (int i = 0; i < Data.TransactionList.Count; i++)
                    {
                        file.WriteLine(Data.TransactionList[i]);
                    }
                }
            }
            catch (Exception e)

            {
                Console.WriteLine();
                Console.WriteLine("Exception: {0}", e.Message);
                Console.WriteLine();
            }
        }
    }
}