namespace CarClass
{
    internal class Program
    {
        static void Main(string[] args)
        {
            float dist = new Random().Next(200, 1000);

            Auto[] Cars = new Auto[2];
            Cars[0] = new Auto("А123СК", 20, 12, 17, 100, dist, 220, 0, 1);
            Cars[1] = new Auto("Р613ЕВ", 27, 13, 438, 110, dist, 200, dist, 0);


            while (true)
            {
                Console.Clear();

                int idx;

                Cars[0].CheckAccident(Cars[1]);

                int temp_idx = 1;
                for (int i = 0; i < Cars.Length; i++)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"{temp_idx++}.");
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Cars[i].Out();
                    Console.BackgroundColor = ConsoleColor.Black;
                }


                Console.Write("Какой машиной управлять: ");
                idx = Convert.ToInt32(Console.ReadLine())-1;
                
                Console.Clear();

                Cars[idx].Out();
                
                Console.WriteLine($"1. Ехать\n" +
                    $"2. Ускориться/Замедлиться\n" +
                    $"3. Заправиться\n" +
                    $"4. Выбрать другую машину\n" +
                    $"5. Выход\n");

#if DEBUG
                Console.WriteLine($"!!! DEUBG !!!\n" +
                    $"6. Разбить эту машину\n");
#endif


                Console.Write("Выбор: ");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Сколько проехать км: ");
                        int km = Convert.ToInt32(Console.ReadLine());
                        Cars[idx].Move(km);
                        break;
                    case "2":
                        Console.Write("Изменить скорость до: ");
                        Cars[idx].SetSpeed( Convert.ToInt32(Console.ReadLine()) );
                        break;
                    case "3":
                        Console.Write("Введите количества бензина для заправки: ");
                        Cars[idx].Refuel( Convert.ToSingle(Console.ReadLine()) );
                        break;
                    case "4":
                        break;
                    case "5":
                        return;
#if DEBUG
                    case "6":
                        Cars[idx].Accident();
                        break;
#endif
                    default:
                        break;
                }
            }
        }
    }
}