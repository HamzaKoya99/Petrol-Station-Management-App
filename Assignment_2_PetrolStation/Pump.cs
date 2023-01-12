using System.Timers;

namespace PetrolStation
{
    // Done by: [Hamza koya]
    // SID: [1922493]

    internal class Pump
    {
        private Vehicle currentVehicle = null;
        internal Vehicle CurrentVehicle { get => currentVehicle; set => currentVehicle = value; }

        private int pumpID;
        public int PumpID { get => pumpID; set => pumpID = value; }
   
        /// <summary>
        /// Method to check if each pump is available
        /// </summary>
                public bool IsAvailable()
        {
            // returns TRUE if currentVehicle is NULL, meaning available
            // returns FALSE if currentVehicle is NOT NULL, meaning busy
            return currentVehicle == null;
        }

        /// <summary>
        /// Assign a vehicle to each pump.
        /// Each pump runs on a timer with the time limit set to the current vehicle's fuel time.
        /// After the fuel time has elapses the vehicle is released from the pump using the release vehicle method
        /// </summary>
        /// <param name="v"></param>
        public void AssignVehicle(Vehicle v)
        {
            currentVehicle = v;
            Data.Vehicles.Remove(v);
            Timer timer = new Timer
            {
                Interval = v.FuelTime,
                AutoReset = false // don't repeat
            };
            timer.Elapsed += ReleaseVehicle;

            timer.Enabled = true;
            timer.Start();
        }

        /// <summary>
        /// Release each vehicle from the pump and records the transaction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReleaseVehicle(object sender, ElapsedEventArgs e)
        {
            //Release vehicle and create copy for logging purposes

            Vehicle v = currentVehicle;
            currentVehicle = null;

            Data.UpdateServicedVehicles();

            //stores a transaction for each fueling

            // string file = "Transactions.Txt";
            //  if (!File.Exists(file)) File.WriteAllText(file, "");
            // string transactionLog = File.ReadAllText(file);

            Transaction t = new Transaction(pumpID + 1, v.VehicleType, v.FuelType, v.FuelNeed);

            t.UpdateTransaction();
            t.UpdateTotalSales();
            t.UpdateCommission();

            Data.TransactionList.Add(t); //Add the transaction to a list of transactions.

            //   transactionLog += t.ToString() + Environment.NewLine;
            //  File.WriteAllText(file, transactionLog);
        }
    }
}