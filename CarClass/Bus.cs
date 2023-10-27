namespace CarClass
{
    internal class Bus : Auto
    {
        private readonly int passengers; // Пассажиры

        public Bus(string number, float fuel, float flow, int mileage, float maxFuel, float dist, int maxSpeed,
            byte direction, int passengers)
            : base(number, fuel, flow, mileage, maxFuel, dist, maxSpeed, direction)
        {
            this.passengers = passengers;
        }

        public override void Out()
        {
            Console.WriteLine($"Пассажиров: {passengers} чел.");
            base.Out();
        }
        
    }
}