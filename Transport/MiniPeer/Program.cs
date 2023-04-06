using System;
using System.IO;
using EKRLib;

namespace MiniPeer
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo keyInfo;
            do
            {
                Console.Clear();
                Random generator = new();
                var length = generator.Next(6, 10);
                Transport[] vehicles = new Transport[length];
                // Генерация объектов и заполнение массива
                for (int i = 0; i < length; i++)
                {
                    try
                    {
                        if (generator.Next(2) == 0)
                            vehicles[i] = new Car(GenerateModel(), (uint)generator.Next(10, 100));
                        else
                            vehicles[i] = new MotorBoat(GenerateModel(), (uint)generator.Next(10, 100));
                        Console.WriteLine(vehicles[i].StartEngine());
                    }
                    catch (TransportException ex)
                    {
                        Console.WriteLine(ex.Message);
                        i--;
                    }
                }
                WriteAllInFiles(vehicles);
                Console.WriteLine("Для выхода нажмите Esc, для продолжения - любую другую клавишу");
                // Повтор программы
                keyInfo = Console.ReadKey(true);
            } while (keyInfo.Key != ConsoleKey.Escape);
        }

        /// <summary>
        /// Запись из массива в файл
        /// </summary>
        /// <param name="vehicles">Массив, из которого осуществляется запись</param>
        public static void WriteAllInFiles(Transport[] vehicles)
        {
            string Cars = "";
            string Boats = "";
            foreach (var vehicle in vehicles)
            {
                if (vehicle.GetType() == typeof(Car))
                    Cars += vehicle + Environment.NewLine;
                else
                    Boats += vehicle + Environment.NewLine;
            }
            File.WriteAllText("..\\..\\..\\..\\Cars.txt", Cars, System.Text.Encoding.Unicode);
            File.WriteAllText("..\\..\\..\\..\\MotorBoats.txt", Boats, System.Text.Encoding.Unicode);
        }

        /// <summary>
        /// Генерация модели транспорта
        /// </summary>
        /// <returns></returns>
        public static string GenerateModel()
        {
            string str = "";
            Random generator = new();
            for (int i = 0; i < 5; i++)
            {
                str += (char)generator.Next(65, 91);
            }
            return str;
        }
    }
}
