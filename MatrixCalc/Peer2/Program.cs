using System;

namespace Peer2
{
    class Program
    {
        // Большинство методов, использующихся в программе, написаны и описаны в классе Matrix. Там же перегружены операторы.
        static void Main(string[] args)
        {
            ConsoleKeyInfo keyInfo;
            do
            {
                // Интерфейс пользователя.
                Console.WriteLine("Это калькулятор матриц");
                Console.WriteLine("Список операций:\n" +
                    "1) Найти след матрицы\n" +
                    "2) Транспонировать матрицу\n" +
                    "3) Найти сумму двух матриц\n" +
                    "4) Найти разность матриц\n" +
                    "5) Найти произведение матриц\n" +
                    "6) Умножить матрицу на число\n" +
                    "7) Найти определитель\n" +
                    "8) Решить СЛАУ методом Гаусса");
                // Вызов метода для определения операции, выбранной пользователем, и вычисления этой опрерации.
                Matrix matrResult = MatrOperation();
                Console.WriteLine("Результат:");
                // Вывод получившейся матрицы на экран.
                Console.WriteLine(matrResult);
                Console.WriteLine("Для выхода нажмите Esc, для продолжения - любую другую клавишу");
                // Повтор программы.
                keyInfo = Console.ReadKey(true);
            } while (keyInfo.Key != ConsoleKey.Escape);

        }

        /// <summary>
        /// Метод, возвращающий число типа uint, введенное пользователем.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        static uint GetUIntFromConsole(string message)
        {
            uint value = 0;
            do
            {
                Console.WriteLine(message);
            } while (!uint.TryParse(Console.ReadLine(), out value));
            return value;
        }

        /// <summary>
        /// Метод, возвращающий число типа uint, введенное пользователем, причем это число ограничено снизу и сверху.
        /// Пока  пользователь не введет подходяще число - метод не отработает.
        /// </summary>
        /// <returns></returns>
        static uint GetUIntFromConsoleWihthBorders(string message, uint minVal, uint maxVal)
        {
            uint value = 0;
            do
            {
                Console.WriteLine(message);
            } while (!uint.TryParse(Console.ReadLine(), out value) || (value < minVal) || (value > maxVal));
            return value;
        }

        /// <summary>
        /// Метод, возвращающий число типа double, введенное пользователем
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        static double GetDoubleFromConsole(string message)
        {
            double value = 0;
            do
            {
                Console.WriteLine(message);
            } while (!double.TryParse(Console.ReadLine(), out value));
            return value;
        }

        /// <summary>
        /// Метод для выбора типа генерации матрицы. После выбора типа вызывается метод,
        /// генерирующий непосредственно матрицу одним из трех типов.
        /// </summary>
        /// <returns></returns>
        static Matrix GenerateMatrixByType()
        {
            uint typeOfGen;
            Matrix matr1 = new Matrix();
            do
            {
                typeOfGen = GetUIntFromConsole("Выберите тип генерации:\n " +
                  "1) Случайно\n" +
                  " 2) Вручную\n 3)Из файла\n" +
                  "Введенное число должно быть в диапазоне 1-3");
                // Генерация матрицы методом, выбранным пользователем.
                switch (typeOfGen)
                {
                    case 1:
                        // Генерация случайной матрицы.
                        matr1 = GenerateRandomMatr();
                        break;
                    case 2:
                        // Генерация матрицы с помощью пользовательского ввода.
                        matr1 = GenerateManuallyMatr();
                        break;
                    case 3:
                        // Чтение матрицы из файла.
                        matr1 = ReadMatrFromFile();
                        break;
                    default:
                        Console.WriteLine("Несуществующий вариант. Повторите попытку.");
                        continue;

                }
            } while (typeOfGen < 1 || typeOfGen > 3);
            return matr1;
        }

        /// <summary>
        /// Генерация матрицы случайным образом по параметрам, введенным пользователем.
        /// </summary>
        /// <returns></returns>
        static Matrix GenerateRandomMatr()
        {
            // Четыре параметра - размер, минимальное и максимальные значения.
            double MatrMax;
            double MatrMin;
            uint rows = GetUIntFromConsole("Введите количество строк(Это должно быть целое число, большее нуля):");
            uint columns = GetUIntFromConsole("Введите количество столбцов(Это должно быть целое число, большее нуля):");
            // Генерация матрицы заданного размера.
            Matrix matr1 = new Matrix(rows, columns);
            do
            {
                // Ввод минимального и максимального значений.
                MatrMin = GetDoubleFromConsole("Введите минимальное значение, которое может присутствовать в матрице" +
                    "(Это должно быть число, допускаются дробные):");
                MatrMax = GetDoubleFromConsole("Введите максимальное значение, которое может присутствовать в матрице" +
                    "(Это должно быть число, допускаются дробные):");
                if (MatrMax < MatrMin)
                    Console.WriteLine("Минимальное значение не должно быть больше максимального.");
            } while (MatrMax < MatrMin);
            var generator = new Random();
            // Заполнение матрицы.
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    matr1.Body[i, j] = MatrMin - (generator.NextDouble() * (MatrMax - MatrMin));
                }
            }
            return matr1;
        }


        /// <summary>
        /// Генерация матрицы с помощью пользовательского ввода
        /// </summary>
        /// <returns></returns>
        static Matrix GenerateManuallyMatr()
        {
            // Ввод размеров матрицы.
            uint rows = GetUIntFromConsole("Введите количество строк(Это должно быть целое число, большее нуля):");
            uint columns = GetUIntFromConsole("Введите количество столбцов(Это должно быть целое число, большее нуля):");
            // Генерация матрицы заданных размеров.
            Matrix matr1 = new Matrix(rows, columns);
            // Заполнение матрицы числами построчно.
            for (uint i = 0; i < rows; i++)
            {
                bool success = false;
                do
                {
                    Console.WriteLine($"Введите {i+1}-ую строку (Допустимые разделители: пробел, ;, |, табуляция):");
                    string stringOfMatr = Console.ReadLine();
                    // Разбивка строки на символы по указанным разделителям.
                    int numColumnsOfMatr = stringOfMatr.Split(' ', ';', '|', '\t').Length;
                    // Проверка строки на соответствие размеров.
                    if (numColumnsOfMatr != columns)
                        Console.WriteLine("Несовпадающее количество введенных столбцов. Пожалуйста, повторите попытку");
                    // Проверка на возможность инициализации.
                    if (!matr1.InitRow(stringOfMatr, i))
                    {
                        Console.WriteLine("Найден неизвестный символ. Повторите, пожалуста, попытку");
                    }
                    else
                    {
                        success = true;
                    }
                } while (success != true);

            }
            return matr1;
        }

        /// <summary>
        /// Чтение матрицы из файла.
        /// </summary>
        /// <returns></returns>
        static Matrix ReadMatrFromFile()
        {
            Matrix matr1 = new Matrix();
            Console.WriteLine("Укажите имя файла в формате .txt (Само расширение не указывать).\n" +
                " Файл должен находится в одной папке с программой, в папке peer2, на один уровень ниже sln файла.\n" +
                "Допустимые разделители: пробел, ;, |, табуляция.\n Разделитель целой и дробной части числа зависит от ваших локальных настроек:");
            bool success = false;
            do
            {
                try
                {
                    // Ввод имени файла и чтение его из указанной папки.
                    string fileName = Console.ReadLine();
                    matr1 = new Matrix("..\\..\\..\\" + fileName + ".txt");
                    success = true;
                }
                // Если поймана ошибка - повторить попытку.
                catch (System.IO.FileNotFoundException e)
                {
                    Console.WriteLine("Не удалось найти файл. Повторите попытку");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Не удалось прочитать матрицу из файла(Введенная последовательность не является числовой матрицей с соотвтветствующими разделителями). Повторите попытку");
                }
            } while (!success);
            return matr1;
        }

        /// <summary>
        /// Главный хаб всей програмы - метод, вызыващий методы операций га основе числа введенного пользователем.
        /// А уже методы операций, в свою очередь, делают запрос на генерацию матриц.
        /// </summary>
        /// <returns></returns>
        static Matrix MatrOperation()
        {
            uint typeOfOperation;
            Matrix matrResult = new Matrix();
            typeOfOperation = GetUIntFromConsoleWihthBorders("Введите операцию(Число должно быть от 1 до 8):", 1, 8);
            // Генерация резуртирующий матрицы на основе выбора пользователя.
            switch (typeOfOperation)
            {
                case 1:
                    // Поиск следа.
                    matrResult = MatrTrace();
                    break;
                case 2:
                    // Транспонирование.
                    matrResult = MatrTranspose();
                    break;
                case 3:
                    // Суммирование.
                    matrResult = MatrSum();
                    break;
                case 4:
                    // Вычитание.
                    matrResult = MatrSubstract();
                    break;
                case 5:
                    // Умножение на матрицу.
                    matrResult = MatrMultiplyMatr();
                    break;
                case 6:
                    // Умножение на число
                    matrResult = MatrMultiplyDouble();
                    break;
                case 7:
                    // Поиск определителя
                    matrResult = MatrDeterm();
                    break;
                case 8:
                    // Метод Гаусса
                    matrResult = GaussSolution();
                    break;

            }
            return matrResult;
        }

        /// <summary>
        /// Поиск следа матрицы
        /// </summary>
        /// <returns></returns>
        static Matrix MatrTrace()
        {
            Console.WriteLine("Введите данные матрицы:");
            Matrix matr1;
            do
            {
                Console.WriteLine("Данная операция доступна только для квадратной матрицы. Пожалуйста, убедитесь перед вводом, что ваша матрица является таковой.");
                // Генерация матрицы.
                matr1 = GenerateMatrixByType();
                // Пока матрица не квадратная.
            } while (!matr1.IsSquare());
            // Вычисление следа.
            return matr1.Trace();
        }

        /// <summary>
        /// Транспонирование матрицы.
        /// </summary>
        /// <returns></returns>
        static Matrix MatrTranspose()
        {
            Console.WriteLine("Введите данные матрицы:");
            // Генерация матрицы.
            Matrix matr1 = GenerateMatrixByType();
            // Непострадственно транспонирование.
            return matr1.Transpose();
        }

        /// <summary>
        /// Суммирование матриц.
        /// </summary>
        /// <returns></returns>
        static Matrix MatrSum()
        {
            Matrix matr1;
            Matrix matr2;
            do
            {
                Console.WriteLine("Данная операция доступна только для матриц одинакового размера. Пожалуйста, убедитесь, что ваши матрицы являются таковыми.");
                // Ввод первой матрицы.
                Console.WriteLine("ВВедите данные первой матрицы:");
                matr1 = GenerateMatrixByType();
                // Ввод второй матрицы.
                Console.WriteLine("ВВедите данные второй матрицы:");
                matr2 = GenerateMatrixByType();
                // Проверка на одинаковую размерность. В случае неудачи - повторить.
            } while ((matr1.N != matr2.N) && (matr1.M != matr2.M));
            // Непострественно сложение.
            return matr1 + matr2;
        }

        /// <summary>
        /// Вычитание матриц.
        /// </summary>
        /// <returns></returns>
        static Matrix MatrSubstract()
        {
            Matrix matr1;
            Matrix matr2;
            do
            {
                Console.WriteLine("Данная операция доступна только для матриц одинакового размера. Пожалуйста, убедитесь, что ваши матрицы являются таковыми.");
                // Ввод первой матрицы.
                Console.WriteLine("ВВедите данные первой матрицы:");
                // Ввод второй матрицы.
                matr1 = GenerateMatrixByType();
                Console.WriteLine("ВВедите данные второй матрицы:");
                matr2 = GenerateMatrixByType();
                // Проверка на одинаковую размерность. В случае неудачи - повторить.
            } while ((matr1.N != matr2.N) && (matr1.M != matr2.M));
            // Непострественно сложение.
            return matr1 - matr2;
        }

        /// <summary>
        /// Умножение мамтрицы на матрицу.
        /// </summary>
        /// <returns></returns>
        static Matrix MatrMultiplyMatr()
        {
            Matrix matr1;
            Matrix matr2;
            do
            {
                Console.WriteLine("Данная операция доступна только для матриц c одинаковым промежуточным размером. Пожалуйста, убедитесь, что ваши матрицы являются таковыми.");
                // Ввод первой мватрицы.
                Console.WriteLine("Введите данные первой матрицы:");
                matr1 = GenerateMatrixByType();
                // Ввод второй матрицы.
                Console.WriteLine("Введите данные второй матрицы:");
                matr2 = GenerateMatrixByType();
                // Проверка на равенство промежуточного элемента. Если не равен - повторить.
            } while (matr1.M != matr2.N);
            return matr1 * matr2;
        }

        /// <summary>
        /// Умножение матрицы на число.
        /// </summary>
        /// <returns></returns>
        static Matrix MatrMultiplyDouble()
        {
            // Ввод матрицы.
            Console.WriteLine("Введите данные матрицы:");
            Matrix matr1 = GenerateMatrixByType();
            // Ввод числа.
            double alpha = GetDoubleFromConsole("Введите число, на которое хотите умножит матрицу(число, а не символ):");
            // Непосредственно само умножение.
            return matr1 * alpha;
        }

        /// <summary>
        /// Поиск определителя.
        /// </summary>
        /// <returns></returns>
        static Matrix MatrDeterm()
        {
            Matrix matr1;
            do
            {
                // Ввод матрицы до тех пор, пока она не квадратная.
                Console.WriteLine("Введите данные матрицы:");
                matr1 = GenerateMatrixByType();
                if (!matr1.IsSquare())
                {
                    Console.WriteLine("Матрица должна быть квадратной. Пожалуйста, повторите попытку");
                }
            } while (!matr1.IsSquare());
            // Поиск определителя.
            return matr1.MatrDeterminant();
        }

        /// <summary>
        /// Метод Гаусса.
        /// </summary>
        /// <returns></returns>
        static Matrix GaussSolution()
        {
            // Ввод системы в виде одной матрицы.
            Console.WriteLine("Введите данные матрицы. Последний столбец матрицы должен соответствовать правой части СЛАУ:");
            Matrix matr1 = GenerateMatrixByType();
            Console.WriteLine("Матрица, получившаяся в результате приминения метода Гаусса:");
            // Решение методом Гаусса.
            return matr1.GaussianElimination();
        }
    }
}
