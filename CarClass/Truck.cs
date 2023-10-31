namespace CarClass
{
    internal class Truck : Auto
    {
        private readonly double weight; // Груз
        private readonly double maxWeight = 38; // максимальная грузоподъемность

        public Truck(string number, float fuel, float flow, int mileage, float maxFuel, float dist, int maxSpeed,
            byte direction, double weight)
            : base(number, fuel, flow, mileage, maxFuel, dist, maxSpeed, direction)
        {
            this.weight = weight > maxWeight ? maxWeight : weight;
        }
        
        public override void Out()
        {
            Console.WriteLine($"Груз: {weight}/{maxWeight} т.");
            base.Out();
        }
        
        
        
    }

}