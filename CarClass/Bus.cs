namespace CarClass
{
    internal class Bus : Auto
    {
        private int passengers; // Пассажиры
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

        public void AddPassengers(int people)
        {
            if (passengers == maxPassengers)
                WarningAlert("Автобус полон");
            else if (passengers + people > maxPassengers)
            {
                WarningAlert($"{maxPassengers - passengers} село в автобус");
                passengers += maxPassengers - passengers;
            }
            else
            {
                passengers += people;    
            }
            




        }

        public void RemovePassengers(int people)
        {
            if (passengers - people < 0)
            {
                ErrorAlert("Нельзя высадить больше пассажиров, чем есть");
            }
            else
                passengers -= people;
            
            
            
        }
        
    }
}