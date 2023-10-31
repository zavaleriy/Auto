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

            Auto[] cars = new Auto[2];
            cars[0] = new Bus("О795НТ", 55, 17, 643, 170, dist, 130, 1, 57);
            cars[1] = new Truck("М486ХУ", 94, 20, 475, 200, dist, 170, 0, 4);

            Console.Clear();
            
            WriteCars(cars);

            Console.Write("\nКакой машиной управлять: ");
            int idx = Convert.ToInt32(Console.ReadLine())-1;


            while (true)
            {
                Console.Clear();
                
                cars[0].CheckAccident(cars[1]);
                
                cars[idx].Out();
                
                Console.WriteLine($"1. Ехать\n" +
                    $"2. Ускориться/Замедлиться\n" +
                    $"3. Заправиться\n" +
                    $"4. Выбрать другую машину\n" +
                    $"5. Выход");
                
                if (cars[idx] is Bus) Console.WriteLine("n. Посадить пассажиров\n" +
                                                        "m. Высадить пассажиров\n");
                else if (cars[idx] is Truck) Console.WriteLine("m. Загрузить груз\n" +
                                                               "m. Выгрузить груз\n");

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
                        Console.Clear();
                        WriteCars(cars);
                        Console.Write("\nКакой машиной управлять: ");
                        idx = Convert.ToInt32(Console.ReadLine())-1;
                        break;
                    case "5":
                        return;
                    case "n":
                    case "m":
                        break;
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