namespace CarClass
{
    internal class Program
    {

        private static void WriteCars(Auto[] cars)
        {
            int tempIdx = 1;
            for (int i = 0; i < cars.Length; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"{tempIdx++}.");
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                cars[i].Out();
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
        
        static void Main(string[] args)
        {
            float dist = new Random().Next(200, 1000);

            Auto[] cars = new Auto[4];
            cars[0] = new Auto("А123СК", 20, 12, 17, 100, dist, 220, 1);
            cars[1] = new Auto("Р613ЕВ", 27, 13, 438, 110, dist, 200, 0);
            cars[2] = new Bus("О795НТ", 55, 17, 643, 170, dist, 130, 0, 11);
            cars[3] = new Truck("М486ХУ", 94, 20, 475, 200, dist, 170, 1, 4);


            while (true)
            {
                Console.Clear();

                int idx;
                WriteCars(cars);

                Console.Write("\nКакой машиной управлять: ");
                idx = Convert.ToInt32(Console.ReadLine())-1;
                
                Console.Clear();

                cars[idx].Out();
                
                Console.WriteLine($"1. Ехать\n" +
                    $"2. Ускориться/Замедлиться\n" +
                    $"3. Заправиться\n" +
                    $"4. Узнать расстояние между машиной" +
                    $"5. Выбрать другую машину\n" +
                    $"6. Выход\n");

#if DEBUG
                Console.WriteLine($"!!! DEUBG !!!\n" +
                    $"a. Разбить эту машину\n");
#endif


                Console.Write("Выбор: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Сколько проехать км: ");
                        int km = Convert.ToInt32(Console.ReadLine());
                        cars[idx].Move(km);
                        break;
                    case "2":
                        Console.Write("Изменить скорость до: ");
                        cars[idx].SetSpeed( Convert.ToInt32(Console.ReadLine()) );
                        break;
                    case "3":
                        Console.Write("Введите количества бензина для заправки: ");
                        cars[idx].Refuel( Convert.ToSingle(Console.ReadLine()) );
                        break;
                    case "4":
                        WriteCars(cars);
                        int checkIdx = Convert.ToInt32(Console.ReadLine())-1;
                        cars[idx].CheckAccident(cars[checkIdx]);
                        break;
                    case "5":
                        break;
                    case "6":
                        return;
#if DEBUG
                    case "a":
                        cars[idx].Accident();
                        break;
#endif
                    default:
                        break;
                }
            }
        }
    }
}