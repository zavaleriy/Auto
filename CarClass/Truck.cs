namespace CarClass
{
    internal class Truck : Auto
    {
        private double weight; // Груз
        private readonly double maxWeight = 38; // Максимальная грузоподъемность

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

        public void AddLoad(double load)
        {
            if (weight == maxWeight)
                WarningAlert("Грузовик полон");
            else if (weight + load > maxWeight)
            {
                WarningAlert($"{Math.Abs(maxWeight - weight)} т. загружено");
                weight += maxWeight - weight;
            }
            else
                weight += load;
        }

        public void RemoveLoad(double load)
        {
            if (weight - load < 0)
            {
                ErrorAlert("Нельзя вызругизть больше груза, чем есть");
            }
            else
            {
                weight -= load;
                
            }
                
        }


    }

}