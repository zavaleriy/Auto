namespace CarClass
{
    internal class Truck : Auto
    {
        private readonly double weight; // Груз

        public Truck(string number, float fuel, float flow, int mileage, float maxFuel, float dist, int maxSpeed,
            byte direction, double weight)
            : base(number, fuel, flow, mileage, maxFuel, dist, maxSpeed, direction)
        {
            this.weight = weight;
        }
        
        public override void Out()
        {
            Console.WriteLine($"Груз: {weight} т.");
            base.Out();
        }
        
    }

}