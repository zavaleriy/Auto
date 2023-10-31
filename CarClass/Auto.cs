namespace CarClass
{
    internal class Auto
    {
        protected readonly string? number; // Номер
        protected float fuel; // Количество бензина в баке
        protected readonly float flow; //  Номинальный расход топлива
        protected float factFlow; // Фактический расход топлива
        protected int mileage; // Пробег
        protected readonly float maxFuel; // Макс. бензина в баке
        protected bool broken; // Cломана 
        protected int speed; // Скорость
        protected readonly int maxSpeed; // Макс. скорость
        protected readonly float dist; // Дистанция
        protected float xCord; // Текущая позиция на плоскости/дороге
        protected readonly byte direction; // Направление машины
        protected bool reached; // Доехали ли машина
        protected double time = 0; // Время за которое была преодолена дистанция

        public Auto (string number, float fuel, float flow, int mileage, float maxFuel, float dist, int maxSpeed, byte direction)
        {
            this.number = number;
            this.fuel = fuel;
            this.flow = flow;
            this.mileage = mileage;
            this.maxFuel = maxFuel;
            this.dist = dist;
            this.maxSpeed = maxSpeed;
            this.direction = direction;
            xCord = direction == 1 ? 0 : dist;
            broken = false;
            speed = 0;
            factFlow = flow;
            reached = false;
        }

        /// Вывод информации
        public virtual void Out()
        {
            Console.WriteLine($"Номер: {number}\n" +
                $"Топливо: {fuel}/{maxFuel} л.\n" +
                $"Расход топлива на 100 км: {factFlow} л.\n" +
                $"Пробег: {mileage} км.\n" +
                $"Скорость: {speed}/{maxSpeed} км/ч\n" +
                $"X: {xCord}\n");

            if (broken) Console.WriteLine("Вы разбились\n");
            if (direction == 1)
            {
                if (xCord < dist && !broken) Console.WriteLine($"Требуется проехать: {dist-xCord}");
                if (xCord >= dist)
                {
                    Console.WriteLine("Вы доехали\n");
                    reached = true;
                }
                else if (Cost(dist - xCord) > fuel && !broken) Console.WriteLine($"Нужно заправиться на {Math.Round(Math.Ceiling(Cost(dist - xCord) + 0.5) - fuel, 2)} л. и более\n");
            }
            else if (direction == 0)
            {
                if (xCord > 0 && !broken) Console.WriteLine($"Требуется проехать: {xCord}");
                if (xCord <= 0)
                {
                    Console.WriteLine("Вы доехали\n");
                    reached = true;
                }
                else if (Cost(xCord) > fuel && !broken) Console.WriteLine($"Нужно заправиться на {Math.Round(Math.Ceiling(Cost(xCord) + 0.5) - fuel, 2)} л. и более\n");
            }

        }

        /// Запрвка бензином
        public void Refuel(float top)
        {
            if (maxFuel >= top + fuel && top >= 0)
                fuel += top;
            else if (maxFuel == fuel && top > 0)
                ErrorAlert("Бак уже полный");
            else if (top > maxFuel - fuel)
            {
                fuel += maxFuel - fuel;
                WarningAlert("Бак был заполнен до конца");
            }
            else if (top < 0)
                ErrorAlert("Нельзя забрать бензин из бака");

        }

        /// Расчет требуемого бензина для поездки
        private float Cost(float interval) => (float) Math.Round(factFlow * (interval / 100), 2);

        /// Продвижение машины
        public void Move(int km)
        {
            if (!broken && !IsReached() )
            {
                if (Cost(km) >= fuel) WarningAlert("Для преодоления этого расстояния требуется больше топлива");
                if (km > 0 && Cost(km) < fuel && speed > 0)
                {
                    fuel -= Cost(km);
                    fuel = (float)Math.Round(fuel, 2);

                    switch (direction)
                    {
                        case 0:
                            xCord -= km;
                            break;
                        case 1:
                            xCord += km;
                            break;
                    }

                    mileage += km;
                }

                else if (km < 0) ErrorAlert("Нельзя поехать обратно");
                else if (km == 0 || speed == 0) WarningAlert("Вы простояли на месте");
            }
            else if (broken)
                ErrorAlert("Машина сломана");
            else if (IsReached())
                WarningAlert("Вы проехали всю дистанцию");
        }

        /// Проверка столкновений машин
        public void CheckAccident(Auto auto)
        {
            if (number == auto.number)
                return;
            
            float distanceBetweenCars = auto.GetX()-xCord;
            if (distanceBetweenCars > 0) Console.WriteLine($"Расстояние между машинами: {distanceBetweenCars}\n");
            if (distanceBetweenCars <= 0 && auto.GetX() != dist 
                    && xCord != 0 
                    && !auto.IsReached() 
                    && !IsReached() )
            {
                auto.Accident();
                Accident();
            }

        }

        /// Установка скорости
        public void SetSpeed(int speedTo)
        {
            if (!broken)
            {
                if (speedTo >= 0 && speedTo <= maxSpeed)
                {
                    speed = speedTo;
                    FlowMultiplier();
                }
                else if (speedTo < 0) ErrorAlert("Скорость не может быть отрицательной");
                else if (speedTo > maxSpeed) ErrorAlert("Превышение лимита скорости");
            }
            else
                ErrorAlert("Машина сломана");
        }

        /// Множитель расхода
        private void FlowMultiplier()
        {
            if (speed < 50) factFlow = flow;
            else if (speed >= 50 && speed < 100) factFlow = flow * 1.1f;
            else if (speed >= 100 && speed < 150) factFlow = flow * 1.2f;
            else if (speed >= 150 && speed < 200) factFlow = flow * 1.3f;
            else if (speed >= 200 && speed <= maxSpeed) factFlow = flow * 1.4f;
            factFlow = (float) Math.Round(factFlow, 2);
        }

        /// Вывод предупреждений
        private void WarningAlert(string message)
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"Внимание: {message}");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.ReadKey();
            Console.CursorVisible = true;
        }

        /// Вывод ошибок
        private void ErrorAlert(string message)
        {
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Ошибка: {message}");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ReadKey();
            Console.CursorVisible = true;
        }

        // Побочные методы
        private float GetX() => xCord;

        private bool IsReached() => reached;

        public void Accident() => broken = true;

    }
}
