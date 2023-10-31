namespace CarClass
{
    internal class Bus : Auto
    {
        private readonly int passengers; // Пассажиры
        private readonly byte maxPassengers = 45; // Максимальное кол-во пассажиров

        public Bus(string number, float fuel, float flow, int mileage, float maxFuel, float dist, int maxSpeed,
            byte direction, int passengers)
            : base(number, fuel, flow, mileage, maxFuel, dist, maxSpeed, direction)
        {
            this.passengers = passengers > maxPassengers ? maxPassengers : passengers;
        }

        public override void Out()
        {
            Console.WriteLine($"Пассажиров: {passengers}/{maxPassengers} чел.");
            base.Out();
        }
        
        
        
    }
}