namespace PetrolStation
{
    // Done by: [Hamza koya]
    // SID: [1922493]

    internal class Vehicle
    {
        private string vehicleType;          //stores the type of vehicle

        private string fuelType;             //store the type of fuel
        private double fuelTime;             //stores the fuel time
        private double fuelNeed;             // stores how much fuel a vehicle requires
        private static int nextCarID = 0;    //
        private int carID;                   //ID for each car

        
        public static int NextCarID { get => nextCarID; set => nextCarID = value; }

        public int CarID { get => carID; set => carID = value; }
        public string VehicleType { get => vehicleType; set => vehicleType = value; }
        public string FuelType { get => fuelType; set => fuelType = value; }
        public double FuelTime { get => fuelTime; set => fuelTime = value; }
        public double FuelNeed { get => fuelNeed; set => fuelNeed = value; }

        /// <summary>
        /// Constuctor for each vehicle. Contains the fuel type, vehicle type, fuel time, and ID for each vehicle.
        /// </summary>
        /// <param name="ftp"></param>
        /// <param name="vtp"></param>
        /// <param name="ftm"></param>
        /// <param name="fn"></param>
        public Vehicle(string ftp, string vtp, double ftm, double fn)
        {
            VehicleType = vtp;
            FuelType = ftp;
            FuelTime = ftm;
            fuelNeed = fn;
            CarID = NextCarID++;
        }
    }
}