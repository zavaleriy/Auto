using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CarClass
{
    internal class Auto
    {
        private readonly string? number; // Номер
        private float fuel; // Количество бензина в баке
        private readonly float flow; //  Номинальный расход топлива
        private float fact_flow; // Фактический расход топлива
        private int mileage; // Пробег
        private readonly float max_fuel; // Макс. бензина в баке
        private bool broken; // Cломана 
        private int speed; // Скорость
        private readonly int max_speed; // Макс. скорость
        private readonly float dist; // Дистанция
        private float x_cord; // Текущая позиция на плоскости/дороге
        private readonly byte direction; // Направление машины
        private bool reached; // Доехали ли машина

        public Auto (string number, float fuel, float flow, int mileage, float max_fuel, float dist, int max_speed, float x_cord, byte direction)
        {
            this.number = number;
            this.fuel = fuel;
            this.flow = flow;
            this.mileage = mileage;
            this.max_fuel = max_fuel;
            this.dist = dist;
            this.max_speed = max_speed;
            this.x_cord = x_cord;
            this.direction = direction;
            broken = false;
            speed = 0;
            fact_flow = flow;
            reached = false;
        }

        // Вывод информации
        public void Out()
        {
            Console.WriteLine($"Номер: {number}\n" +
                $"Топливо: {fuel}\n" +
                $"Расход топлива на 100 км: {fact_flow}\n" +
                $"Пробег: {mileage}\n" +
                $"Скорость: {speed}/{max_speed}\n" +
                $"X: {x_cord}\n");

            if (broken) Console.WriteLine("Вы разбились\n");
            if (direction == 1)
            {
                if (x_cord < dist && !broken) Console.WriteLine($"Требуется проехать: {dist-x_cord}");
                if (x_cord >= dist)
                {
                    Console.WriteLine("Вы доехали\n");
                    CarIsReached();
                }
                else if (Cost(dist - x_cord) > fuel && !broken) Console.WriteLine($"Нужно заправиться на {Math.Round(Math.Ceiling(Cost(dist - x_cord) + 0.5) - fuel, 2)} л. и более\n");
            }
            else if (direction == 0)
            {
                if (x_cord > 0 && !broken) Console.WriteLine($"Требуется проехать: {x_cord}");
                if (x_cord <= 0)
                {
                    Console.WriteLine("Вы доехали\n");
                    CarIsReached();
                }
                else if (Cost(x_cord) > fuel && !broken) Console.WriteLine($"Нужно заправиться на {Math.Round(Math.Ceiling(Cost(x_cord) + 0.5) - fuel, 2)} л. и более\n");
            }

        }

        // Запрвка бензином
        public void Refuel(float top)
        {
            if (max_fuel >= top + fuel && top >= 0)
                fuel += top;
            else if (max_fuel == fuel && top > 0)
                ErrorAlert("Бак уже полный");
            else if (top > max_fuel - fuel)
            {
                fuel += max_fuel - fuel;
                WarningAlert("Бак был заполнен до конца");
            }
            else if (top < 0)
                ErrorAlert("Нельзя забрать бензин из бака");

        }

        // Расчет требуемого бензина для поездки
        public float Cost(float dist) => (float) Math.Round(fact_flow * (dist / 100), 2);

        // Продвижение машины
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
                            x_cord -= km;
                            break;
                        case 1:
                            x_cord += km;
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

        // Проверка столкновений машин
        public void CheckAccident(Auto auto)
        {
            float distanceBetweenCars = auto.GetX()-x_cord;
            if (distanceBetweenCars > 0) Console.WriteLine($"Расстояние между машинами: {distanceBetweenCars}\n");
            if (distanceBetweenCars <= 0 && auto.GetX() != dist 
                    && x_cord != 0 
                    && !auto.IsReached() 
                    && !IsReached() )
            {
                auto.Accident();
                Accident();
            }

        }

        // Установка скорости
        public void SetSpeed(int speed)
        {
            if (!broken)
            {
                if (speed >= 0 && speed <= max_speed)
                {
                    this.speed = speed;
                    FlowMultiplier();
                }
                else if (speed < 0) ErrorAlert("Скорость не может быть отрицательной");
                else if (speed > max_speed) ErrorAlert("Превышение лимита скорости");
            }
            else
                ErrorAlert("Машина сломана");
        }

        // Множитель расхода
        private void FlowMultiplier()
        {
            if (speed < 50) fact_flow = flow;
            else if (speed >= 50 && speed < 100) fact_flow = flow * 1.1f;
            else if (speed >= 100 && speed < 150) fact_flow = flow * 1.2f;
            else if (speed >= 150 && speed < 200) fact_flow = flow * 1.3f;
            else if (speed >= 200 && speed <= max_speed) fact_flow = flow * 1.4f;
            fact_flow = (float) Math.Round(fact_flow, 2);
        }

        // Вывод предупреждений
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

        // Вывод ошибок
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
        public float GetFuel() => fuel;

        public float GetX() => x_cord;

        public bool IsReached() => reached;

        public void Accident() => broken = true;
        private void CarIsReached() => reached = true;

    }
}
