using System;

namespace Pir1
{
    // Это код без комментариев. В архиве есть код с комментариями
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo keyInfo;
            do
            {
                int length;
                do
                {
                    Console.WriteLine("Введите длину загадываемого числа (от 1 до 10):");
                } while (!int.TryParse(Console.ReadLine(), out length) || length < 1 || length > 10);
                string sNumber = GenerateNumber(length);
                Console.WriteLine("Число загадано. Отгадайте!");
                // !Можете раскомментировать следующую строчку для проверки!
                // Console.WriteLine(sNumber);
                string guess;
                do
                {
                    int cows = 0;
                    int bulls = 0;
                    guess = GetUserNumber(length);
                    for (int i = 0; i < length; i++)
                    {
                        int positionInNumber = sNumber.IndexOf(guess[i]);
                        if (positionInNumber == i)
                        {
                            bulls++;
                        }
                        else if (positionInNumber != -1)
                        {
                            cows++;
                        }
                    }
                    Console.WriteLine($"Коров: {cows}, быков: {bulls}");
                } while (guess != sNumber);
                Console.WriteLine("Ура, вы угадали! Для выхода нажмите Esc, для продолжения - любую другую клавишу");
                keyInfo = Console.ReadKey(true);
            } while (keyInfo.Key != ConsoleKey.Escape);


        }

        public static string GenerateNumber(int length)
        {
            string digits = "1234567890";
            string result = "";
            var myGenerator = new Random();
            for (int i = 0; i < length; i++)
            {
                int digitPosition;
                if (i == 0)
                {
                    digitPosition = myGenerator.Next() % 9;
                }
                else
                {
                    digitPosition = myGenerator.Next() % digits.Length;
                }
                result += digits[digitPosition];
                digits = digits.Remove(digitPosition, 1);
            }
            return result;
        }

        public static bool AreDigitsUnique(string sNumber)
        {
            for (int i = 0; i < sNumber.Length; i++)
            {
                for (int j = i + 1; j < sNumber.Length; j++)
                {
                    if (sNumber[i] == sNumber[j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static string GetUserNumber(int length)
        {
            string guess = "";
            do
            {
                Console.WriteLine("Введите вашу догадку:");
                string input = Console.ReadLine();
                input = input.Trim().TrimStart('0');
                if (!uint.TryParse(input, out uint inputInt))
                {
                    Console.WriteLine("Это должно быть число, причем неотрицательное");
                }
                else if (input.Length != length)
                {
                    Console.WriteLine($"Это должно быть {length}-значное число");
                }
                else if (!AreDigitsUnique(input))
                {
                    Console.WriteLine("Цифры не должны повторяться");
                }
                else
                {
                    guess = input;
                }
            } while (guess.Length == 0);
            return guess;
        }


    }
}
