using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace PetrolStation
{
    // Done by: [Hamza koya]
    // SID: [1922493]
    

    internal class Data
    {
        private static List<Vehicle> vehicles;                                       //A list to store the vehicles in a queue
        private static List<Pump> pumps = new List<Pump>();                          //List of all the pumps in the station
        private static List<Transaction> transactionList = new List<Transaction>();  //A list of all transactions

        private static readonly string[] typeV = { "Car", "Van", "HGV" };              //the types of vehicles
        private static readonly string[] fuels = { "LPG", "Diesel", "Unleaded" };     //the types of fuels

        private static int missedVehicles;
        private static int vehiclesServiced;

        private static double unleadedDispensed = 0;
        private static double dieselDispensed = 0;
        private static double lpgDispensed = 0;
        private static double totalFuel = 0;

        private static double totalSales = 0.00;
        private static double commission = 0.00;

        //Private constants
        private const double fuelPriceLpg = 0.70;

        private const double fuelPriceDiesel = 1.18;
        private const double fuelPriceUnleaded = 1.10;
        private const double commissionRate = 0.01;

        public static List<Vehicle> Vehicles
        {
            get
            {
                return vehicles;
            }
            set
            {
                vehicles = value;
            }
        }

        public static List<Pump> Pumps
        {
            get
            {
                return pumps;
            }
            set
            {
                pumps = value;
            }
        }

        public static List<Transaction> TransactionList
        {
            get
            {
                return transactionList;
            }
            set
            {
                transactionList = value;
            }
        }

        public static int MissedVehicles { get => missedVehicles; set => missedVehicles = value; }
        public static int VehiclesServiced { get => vehiclesServiced; set => vehiclesServiced = value; }

        public static double TotalSales { get => totalSales; set => totalSales = value; }
        public static double Commission { get => commission; set => commission = value; }

        public static double UnleadedDispensed { get => unleadedDispensed; set => unleadedDispensed = value; }
        public static double DieselDispensed { get => dieselDispensed; set => dieselDispensed = value; }
        public static double LpgDispensed { get => lpgDispensed; set => lpgDispensed = value; }

        public static double Totalfuel { get => totalFuel; set => totalFuel = value; }

        public static double FuelPriceLpg => fuelPriceLpg;
        public static double FuelPriceDiesel => fuelPriceDiesel;
        public static double FuelPriceUnleaded => fuelPriceUnleaded;
        public static double CommissionRate => commissionRate;

        private static Timer timer;
        private static Random rng = new Random();

        /// <summary>
        /// Method that starts both, vehicle and pump initialisation
        /// </summary>
        public static void Initialise()
        {
            Initialisepumps();
            InitialiseVehicles();
        }

        /// <summary>
        /// Initialise the vehicle creation in a timer
        /// </summary>
        private static void InitialiseVehicles()
        {
            Vehicles = new List<Vehicle>();

            // https://msdn.microsoft.com/en-us/library/system.timers.timer(v=vs.71).aspx

            timer = new Timer
            {
                Interval = 1500,
                AutoReset = true // keeps repeating every 1.5 seconds
            };
            timer.Elapsed += CreateVehicle;
            timer.Enabled = true;
            timer.Start();
        }

        /// <summary>
        /// Creates a random vehicle based on a type of vehicles
        /// Creates a random fuel based on what fuel each vehicle can use
        /// Creates a random amount of fuel that the vehicle will have before it starts fueling.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CreateVehicle(object sender, ElapsedEventArgs e)
        {
            const double RATE = 1.5; //dispense rate
            double maxFuel;
            double initFuel;
            double fuelNeed;
            double fuelTime;

            //random vehicle selection
            int typesNum = rng.Next(0, 3);
            string type = typeV.ElementAt(typesNum);

            //Random fuel selection
            int fuelsNum = rng.Next(0, 3);
            string fuel = fuels.ElementAt(typesNum);

            //selects the tank size and ininitial fuel amount based on vehicle type
            if (type == "Car")
            {
                initFuel = rng.Next(0, 11); //initial fuel < 25%
                maxFuel = 40;
                typesNum = rng.Next(0, 3);
                fuel = fuels.ElementAt(typesNum);
            }
            else if (type == "Van")
            {
                initFuel = rng.Next(0, 21); //initial fuel < 25%
                maxFuel = 80;
                typesNum = rng.Next(0, 2);
                fuel = fuels.ElementAt(typesNum);
            }
            else
            {
                initFuel = rng.Next(0, 38); //initial fuel < 25%
                maxFuel = 150;

                fuel = "Diesel";
            }

            fuelNeed = maxFuel - initFuel; // calculates how much fuel is needed

            fuelTime = Math.Truncate((fuelNeed / RATE) * 1000);  //the time taken to fuel in milliseconds

            Vehicle v = new Vehicle(fuel, type, fuelTime, fuelNeed);

            if (vehicles.Count < 5)
                vehicles.Add(v);
            else
                UpdateMissedVehicles();

            short runNextAfter = (short)rng.Next(1500, 2500);

            timer.Interval = runNextAfter;
        }

        /// <summary>
        /// Initialises the 9 pumps.
        /// </summary>
        private static void Initialisepumps()
        {
            Pump p;

            for (int i = 0; i < 9; i++)
            {
                p = new Pump();
                pumps.Add(p);
            }
        }

        /// <summary>
        /// Assigns a vehicle to each pump once the pump is free.
        /// Each Lane is in a row with three column as pumps.
        /// It will select the last pump in each lane first.
        /// if the second pump in each lane is occupied then the last pump is blocked.
        /// the same goes for the second and last pump if the first pump is occupied.
        /// </summary>
        public static void AssignVehicleToPump()
        {
            Pump p = null;

            if (vehicles.Count == 0) { return; }

            //Assign pumps using queue
            if (pumps[8].IsAvailable() && pumps[7].IsAvailable() && pumps[6].IsAvailable()) p = pumps[8];
            else if (pumps[7].IsAvailable() && pumps[6].IsAvailable()) p = pumps[7];
            else if (pumps[6].IsAvailable()) p = pumps[6];
            else if (pumps[5].IsAvailable() && pumps[4].IsAvailable() && pumps[3].IsAvailable()) p = pumps[5];
            else if (pumps[4].IsAvailable() && pumps[3].IsAvailable()) p = pumps[4];
            else if (pumps[3].IsAvailable()) p = pumps[3];
            else if (pumps[2].IsAvailable() && pumps[1].IsAvailable() && pumps[0].IsAvailable()) p = pumps[2];
            else if (pumps[1].IsAvailable() && pumps[0].IsAvailable()) p = pumps[1];
            else if (pumps[0].IsAvailable()) p = pumps[0];

            if (p == null) return;

            Vehicle v = vehicles[0];

            p?.AssignVehicle(v); // assign vehicle to the pump

            p.PumpID = pumps.IndexOf(p);
        }

        /// <summary>
        /// Updates the number of vehicles serviced
        /// </summary>
        public static void UpdateServicedVehicles()
        {
            VehiclesServiced++;
        }

        /// <summary>
        /// Updates the number of vehicles that did not get serviced and went away
        /// </summary>
        public static void UpdateMissedVehicles()
        {
            MissedVehicles++;
        }
    }
}