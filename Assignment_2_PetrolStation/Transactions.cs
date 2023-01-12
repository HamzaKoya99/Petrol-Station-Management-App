namespace PetrolStation
{
    // Done by: [Hamza koya]
    // SID: [1922493]

    internal class Transaction
    {
        private int pumpNumber;

        private double fuelDispensed = 0;
        private double fuelingCost = 0.00;
        private string vehicleType, vehicleFuelType;
        private static int nextTransID = 1;
        private int transID;

        //Accessors for the private attributes
        public static int NextTransID { get => nextTransID; set => nextTransID = value; }

        public int TransID { get => transID; set => transID = value; }
        public int PumpNumber { get => pumpNumber; set => pumpNumber = value; }
        public string VehicleType { get => vehicleType; set => vehicleType = value; }
        public string VehicleFuelType { get => vehicleFuelType; set => vehicleFuelType = value; }
        public double FuelDispensed { get => fuelDispensed; set => fuelDispensed = value; }
        public double FuelingCost { get => fuelingCost; set => fuelingCost = value; }

        /// <summary>
        /// Constuctor for each transaction which contains the transaction number, the vehicle type, the the fuel type, amount of fuel base
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="vtp"></param>
        /// <param name="ftp"></param>
        /// <param name="fd"></param>
        public Transaction(int pn, string vtp, string ftp, double fd)
        {
            PumpNumber = pn;
            FuelDispensed = fd;
            VehicleType = vtp;
            VehicleFuelType = ftp;
            TransID = NextTransID++;
        }

        /// <summary>
        /// Calculates the fuel cost for each transaction.
        /// Updates the total of that fuel type
        /// updates the combine total of all the fuel types
        /// </summary>
        /// <returns></returns>
        public double UpdateTransaction()
        {
            Data.Totalfuel += FuelDispensed;

            switch (VehicleFuelType)
            {
                case "Unleaded":
                    Data.UnleadedDispensed += FuelDispensed;
                    FuelingCost = FuelDispensed * Data.FuelPriceUnleaded;
                    break;

                case "Diesel":
                    FuelingCost = FuelDispensed * Data.FuelPriceDiesel;
                    Data.DieselDispensed += FuelDispensed;
                    break;

                case "LPG":
                    FuelingCost = FuelDispensed * Data.FuelPriceLpg;
                    Data.LpgDispensed += FuelDispensed;
                    break;
            }

            return fuelingCost;
        }

        /// <summary>
        /// method to calculate and update the total fuel sales
        /// </summary>
        public void UpdateTotalSales()
        {
            Data.TotalSales += FuelingCost;
        }

        /// <summary>
        /// Method to calculate and update the commission earned
        /// </summary>
        public void UpdateCommission()
        {
            Data.Commission += FuelingCost * Data.CommissionRate;
        }

        /// <summary>
        /// Method to return each transaction
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{TransID}    Pump#{PumpNumber} {VehicleType} -  ${fuelingCost}  {FuelDispensed}L {VehicleFuelType}   dispensed";
        }
    }
}