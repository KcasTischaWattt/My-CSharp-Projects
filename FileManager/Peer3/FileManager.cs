using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace Peer3
{
    class FileManager
    {
        // Текущая директория, в которой находится пользователь, и которую можно менять в ходе программы
        public static string CurrentDirectory;
        static void Main(string[] args)
        {
            Console.WriteLine("Это файловый менеджер. В начале вам будет предложен список команд,\n" +
                "вызвать снова его можно будет в любое время командой help.\n" +
                "После выполнения одной команды вы можете сразу же выбрать следующую. \n" +
                "Для выхода из программы введите exit.\n" +
                "!!!!Важно. Так как строка парсится по пробелам, имена файлов и директорий с пробелами должны указываться в кавычках(\"a.txt\")\n" +
                "Соответственно, они не должны содержать кавычек. То же самое относится к маскам.");
            WriteHelp();
            // Задаем текущую директорию перед началом программы
            CurrentDirectory = Directory.GetCurrentDirectory();
            //ChangeDirectory(SplitInput("cd D:\\f"));
            Console.Write(CurrentDirectory + ">");
            // Основная логика программы
            while (true)
            {
                try
                {
                    // Вызов команды, вызывающей другие команды на основе введенного пользователем 
                    FileOperation();
                    Console.Write(CurrentDirectory + ">");
                }
                // Если вдруг поймана неизвестная ошибка, не указанная в более мелких методах,
                // то программа не завершается аварийно, а просит повторить попытку
                catch (Exception)
                {
                    Console.WriteLine("Произошла неизвестная ошибка. Повторите попытку");
                }
            }


        }

        /// <summary>
        /// Выводит на эран список доступных команд
        /// </summary>
        public static void WriteHelp()
        {
            Console.WriteLine("Список команд (* - обязательные параметры):\n" +
                "drive - Просмотр списка дисков компьютера\n" +
                "drive [диск] - Выбор диска, например, drive C:\\\n" +
                "cd [директория*] - Выбор текущей директории (Сменить директорию)\n" +
                "dir - Просмотр списка файлов в текущей директории\n" +
                "out [файл*] [кодировка]- Вывод содержимого текстового файла в консоль в выбранной кодировке (по умолчанию в UTF-8)\n" +
                "copy [файл 1*] [файл 2 или директория*] - Копирование файла\n" +
                "move [файл 1*] [файл 2 или директория*] - Переименование файла 1 в файл 2 или перемещение файла 1 в указанную директорию\n" +
                "del [файл*] - Удаление файла\n" +
                "txt [файл*] [кодировка] - Создание текстового файла в выбранной кодировке (по умолчанию в UTF-8)\n" +
                "concat [файл 1*] [файл 2] ... [файл n] - Конкатенация нескольких текстовых файлов и вывод результата в консоль.\n" +
                "find [маска*] - поиск файлов в директории по маске\n" +
                "rfind [маска*] - поиск файлов в директории и всех ее поддиректориях по маске\n" +
                "Примеры регулярных выражений для масок: mp? выведет mp3 и mp4 файлы\n" +
                "Для выхода из программы - введите exit");
        }

        /// <summary>
        /// Просит пользователя ввести команду и выполняет ее, обращаясь к другим методам
        /// </summary>
        public static void FileOperation()
        {
            string s = Console.ReadLine();
            // Если пользователь нажал Enter - то ничего не делать
            if (s.Length == 0)
                return;
            // Превращение пользовательского ввода в команду с помощью деления по пробелам (см. метод SplitInput)
            string[] userInput = SplitInput(s);
            // Вызов операции, согласно введенной пользователем команде
            switch (userInput[0])
            {
                case "drive":
                    Drive(userInput);
                    break;
                case "cd":
                    ChangeDirectory(userInput);
                    break;
                case "dir":
                    ShowDirectory(userInput);
                    break;
                case "out":
                    OutputFile(userInput);
                    break;
                case "copy":
                    CopyFile(userInput);
                    break;
                case "move":
                    MoveFile(userInput);
                    break;
                case "del":
                    DeleteFile(userInput);
                    break;
                case "txt":
                    TxtFile(userInput);
                    break;
                case "concat":
                    ConcatFiles(userInput);
                    break;
                case "help":
                    WriteHelp();
                    break;
                case "find":
                    FindFile(userInput);
                    break;
                case "rfind":
                    RFindFile(userInput);
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Команда не распознана. Повторите попытку.");
                    break;
            }
        }

        /// <summary>
        /// Поиск файла в директории и всех ее поддиректориях по маске, задаваемой в виде регулярного выражения
        /// </summary>
        /// <param name="userInput"> Команда и аргументы, введенные пользователем</param>
        public static void RFindFile(string[] userInput)
        {
            try
            {
                // Смотрим на количество аргументов введенных пользователем. Их должно быть 2
                if (userInput.Length == 1)
                    Console.WriteLine("Маска не указана");
                else if (userInput.Length == 2)
                {
                    // Превращаем маску в регулярное выражение
                    var re = new Regex(userInput[1]);
                    RecursiveFindFile(CurrentDirectory, re);
                }
                else
                    Console.WriteLine("Слишком много параметров. Использование: rfind [маска*]");
            }
            // Ловим Exception'ы
            catch (ArgumentException)
            {
                Console.WriteLine("Маска задана не регулярным выражением");
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка чтения директории");
            }
        }

        /// <summary>
        /// Рекурсивный алгоритм для поиска файла во всех поддиректориях
        /// </summary>
        /// <param name="startDirectory"> Базовая директория,в поддиректориях которой и следует вести поиск </param>
        /// <param name="re">Маска</param>
        public static void RecursiveFindFile(string startDirectory, Regex re)
        {
            try
            {
                // Получаем массив всех файлов в директории и проходимся по нему
                string[] files = Directory.GetFiles(startDirectory);
                foreach (var f in files)
                {
                    // Если нашли хотя бы одно совпадение - напечатать имя файла
                    var matches = re.Matches(Path.GetFileName(f));
                    if (matches.Count > 0)
                        Console.WriteLine(f);
                }
                // Получаем массив всех поддиректорий в директории и проходимся по нему, в каждой из них запуская этот алгоритм
                string[] dirs = Directory.GetDirectories(startDirectory);
                foreach (var d in dirs)
                {
                    RecursiveFindFile(d, re);
                }
            }
            // Иногда ОС может запретить читать директории -
            // тогда просто выводится сообщение и алгоритм продолжает работу
            catch (Exception)
            {
                Console.WriteLine($"Ошибка чтения директории: {startDirectory}");
            }
        }

        /// <summary>
        /// Поиск файлов по маске только в одной директории, без учета поддиректорий
        /// </summary>
        /// <param name="userInput">Команда и аргументы, введенные пользователем</param>
        public static void FindFile(string[] userInput)
        {
            try
            {
                // Смотрим на количество аргументов введенных пользователем. Их должно быть 2
                if (userInput.Length == 1)
                    Console.WriteLine("Маска не указана");
                else if (userInput.Length == 2)
                {
                    // Получаем массив всех файлов в директории и проходимся по нему
                    string[] files = Directory.GetFiles(CurrentDirectory);
                    var re = new Regex(userInput[1]);
                    foreach (var f in files)
                    {
                        // Если нашли хотя бы одно совпадение - напечатать имя файла
                        var matches = re.Matches(Path.GetFileName(f));
                        if (matches.Count > 0)
                            Console.WriteLine(Path.GetFileName(f));
                    }
                }
                else
                    Console.WriteLine("Слишком много параметров. Использование: find [маска*]");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Маска задана не регулярным выражением");
            }
            catch(Exception)
            {
                Console.WriteLine("Ошибка чтения директории");
            }
        }
    
        /// <summary>
        /// "Парсер" строки, разделяющий ее по пробелам, но игнорирующий пробелы внутри кавычек
        /// </summary>
        /// <param name="s">Исходная строка</param>
        /// <returns></returns>
        public static string[] SplitInput(string s)
        {
            var result = new List<string>();
            // Задаем регулярку для правильного разделения строки
            var re = new Regex("[^\" \t]+|\"[^\"]*\"");
            var matches = re.Matches(s);
            // Идем по строке и применяем регулярку. Команда не может быть в кавычках - ее не трогаем
            // А вот со второго слова - убираем кавычки, оставляя пробелы внутри них
            for (var i = 0; i < matches.Count; i++)
            {
                string value = matches[i].Value;
                if (i != 0)
                {
                    value = matches[i].Value.Trim('"');
                }
                result.Add(value);
            }
            return result.ToArray();
        }

        /// <summary>
        /// Конкатенация нескольких файлов
        /// </summary>
        /// <param name="userInput">Команда и аргументы, введенные пользователем</param>
        public static void ConcatFiles(string[] userInput)
        {
            // Проверяем, указаны ли аргументы после команды
            if (userInput.Length > 1)
            {
                // Выводим каждый файл в консоль по очереди
                for (int i = 1; i < userInput.Length; i++)
                    try
                    {
                        PrintFile(userInput[i]);
                    }
                    catch (IOException)
                    {
                        Console.WriteLine("Ошибка ввода-вывода");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Указан несуществующий или недоступный файл: {userInput[i]}");
                    }
                Console.WriteLine();
            }
            else
                Console.WriteLine("Не указаны файлы для конкатенации");
        }

        /// <summary>
        /// Запись файла в выбранной кодировке(На самом деле вызов метода WriteInFile и отлавливание ошибок)
        /// </summary>
        /// <param name="userInput">Команда и аргументы, введенные пользователем</param>
        public static void TxtFile(string[] userInput)
        {
            try
            {
                WriteInFile(userInput);
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка ввода-вывода");
            }
            catch (Exception)
            {
                Console.WriteLine("Неверный путь к файлу");
            }
        }

        /// <summary>
        /// Запись файла в выбранной кодировке (уже по-настоящему)
        /// </summary>
        /// <param name="userInput">Команда и аргументы, введенные пользователем</param>
        public static void WriteInFile(string[] userInput)
        {
            // Проверка количества аргумсентов
            if (userInput.Length <= 3)
            {
                Console.Write("Введите текст, который вы хотите записать в файл");
                string input = Console.ReadLine();
                if (userInput.Length == 1)
                    Console.WriteLine("Не указан путь к файлу");
                // Если не указана кодировка - значит, записать все в файл в UTF-8
                if (userInput.Length == 2)
                    File.WriteAllText(userInput[1], input, Encoding.UTF8);
                // Если кодировка указана - то превратить введенный пользователем аргумет в кодировку - и записать в ней
                if (userInput.Length == 3)
                {
                    // Переменная, значение которой сменится на true, если кодировка существует
                    bool encodingExists = false;
                    // Проходимся по всем кодировкам в поисках нужной
                    foreach (EncodingInfo ei in Encoding.GetEncodings())
                    {
                        // Если нашли - замечательно, записываем в ней файл
                        if (userInput[2] == ei.Name)
                        {
                            File.WriteAllText(userInput[1], input, ei.GetEncoding());
                            encodingExists = true;
                            break;
                        }
                    }
                    // Чтобы помочь пользователю, который ввел неправильную кодировку, следует показать все доступные кодировки
                    if (!encodingExists)
                        Console.WriteLine($"Неизвестная кодировка: {userInput[2]}. Доступные кодировки:\n" + ShowEncodings());
                }
            }
            else
                Console.WriteLine("Слишком много аргументов");
        }

        /// <summary>
        /// Удаление файла
        /// </summary>
        /// <param name="userInput">Команда и аргументы, введенные пользователем</param>
        public static void DeleteFile(string[] userInput)
        {
            try
            {
                // Проверка количества аргументов
                if (userInput.Length == 1)
                    Console.WriteLine("Файл не указан");
                else if (userInput.Length == 2)
                    File.Delete(userInput[1]);
                else
                    Console.WriteLine("Слишком много параметров");
            }
            catch (IOException)
            {
                Console.WriteLine("Указанный файл используется. Закройте файл и повторите попытку");
            }
            catch (Exception)
            {
                Console.WriteLine("Файл не найден");
            }
        }

        /// <summary>
        /// Перемещение или перерименование файла, в зависимости от аргументов
        /// </summary>
        /// <param name="userInput">Команда и аргументы, введенные пользователем</param>
        public static void MoveFile(string[] userInput)
        {
            try
            {
                if (userInput.Length < 3)
                    Console.WriteLine("Файл не указан. Формат: move [файл 1*] [файл 2 или директория*]");
                else if (userInput.Length == 3)
                {
                    // Если существует директория с таким именем - переместить туда файл
                    if (Directory.Exists(userInput[2]))
                        FileSystem.MoveFile(userInput[1], Path.Combine(userInput[2], Path.GetFileName(userInput[1])));
                    else
                        // Если не существует - то переименовать файл
                        FileSystem.RenameFile(userInput[1], Path.GetFileName(userInput[2]));
                }
                else
                    Console.WriteLine("Слишком много параметров");
            }
            catch (IOException)
            {
                Console.WriteLine( "Файлуже существует, или произошла ошибка ввода - вывода");
            }
            catch (Exception)
            {
                Console.WriteLine("Файл или директория не найдены: " + userInput[2]);
            }
        }

        /// <summary>
        /// Копирование файла в директорию или в файл, в зависимости от параметров
        /// </summary>
        /// <param name="userInput">Команда и аргументы, введенные пользователем</param>
        public static void CopyFile(string[] userInput)
        {
            try
            {
                if (userInput.Length < 3)
                    Console.WriteLine("Файл не указан. Формат: copy [файл 1*] [файл 2 или директория*]");
                else if (userInput.Length == 3)
                {
                    // Если существует директория с таким именем - то скопировать в нее
                    if (Directory.Exists(userInput[2]))
                        CopyToDirectory(userInput[1], userInput[2]);
                    else 
                        // Если не существует - скопировать в файл
                        CopyToFile(userInput[1], userInput[2]);                   
                }
                else
                    Console.WriteLine("Слишком много параметров");
            }
            catch (IOException)
            {
                Console.WriteLine("Файл уже существует, или произошла ошибка ввода-вывода");
            }
            catch (Exception)
            {
                Console.WriteLine("Файл или директория не найдены: " + userInput[2]);
            }
        }

        /// <summary>
        /// Копирование из файла в файл
        /// </summary>
        /// <param name="path1">Исходный файл</param>
        /// <param name="path2">Файл, в который производится копирование</param>
        private static void CopyToFile(string path1, string path2)
        {
            try
            {
                // Для того, чтобы скопировать из файла в файл, второй файл удаляется и вместо него создается идентичный первому файл
                File.Delete(path2);
            }
            catch (IOException)
            {
                Console.WriteLine($"Файл {path2} в данный момент используется. Закройте файл и повторите попытку");
            }
            // Создаем идентичный первому файл
            string content = File.ReadAllText(path1);
            File.WriteAllText(path2, content);
        }

        /// <summary>
        /// Копирование файла из директории в директорию
        /// </summary>
        /// <param name="path1">Исходный файл</param>
        /// <param name="path2">Директория, куда файл копируется</param>
        private static void CopyToDirectory(string path1, string path2)
        {
            File.Copy(path1, Path.Combine(path2, Path.GetFileName(path1)));
        }

        /// <summary>
        /// Вывод файла в консоль(на самом деле отлов ошибок)
        /// </summary>
        /// <param name="userInput">Команда и аргументы, введенные пользователем</param>
        public static void OutputFile(string[] userInput)
        {
            try
            {
                ReadFile(userInput);
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка ввода-вывода");
            }
            catch (Exception)
            {
                Console.WriteLine("Файл не найден");
            }
        }

        /// <summary>
        /// Вывод файла в консоль
        /// </summary>
        /// <param name="userInput">Команда и аргументы, введенные пользователем</param>
        public static void ReadFile(string[] userInput)
        {
            if (userInput.Length <= 3)
            {
                if (userInput.Length == 1)
                    Console.WriteLine("Не указан путь к файлу");
                // Переменная, в которую будет записываться соджержимое файла
                string content = "";
                // Если кодировка не указана - записать в content содержимое в UTF-8
                if (userInput.Length == 2)
                    content = File.ReadAllText(userInput[1], Encoding.UTF8);
                if (userInput.Length == 3)
                {
                    // Переменная, значение которой сменится на true, если кодировка существует 
                    bool encodingExists = false;
                    // Проходимся по всем кодировкам в поисках нужной
                    foreach (EncodingInfo ei in Encoding.GetEncodings())
                    {
                        if (userInput[2] == ei.Name)
                        {
                            // Если нашли - замечательно, читаем в ней файл
                            content = File.ReadAllText(userInput[1], ei.GetEncoding());
                            encodingExists = true;
                            break;
                        }
                    }
                    // Чтобы помочь пользователю, который ввел неправильную кодировку, следует показать все доступные кодировки
                    if (!encodingExists)
                        Console.WriteLine($"Неизвестная кодировка: {userInput[2]}. Доступные кодировки:\n" + ShowEncodings());
                }
                Console.WriteLine(content);
            }
            else
                Console.WriteLine("Слишком много параметров");
        }

        /// <summary>
        /// Непосредственно вывод тестового файла в консоль (для метода ConcatFiles) 
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        public static void PrintFile(string path)
        {
            string content;
            content = File.ReadAllText(path, Encoding.UTF8);
            Console.Write(content);
        }

        /// <summary>
        /// Показать все доступные кодировки
        /// </summary>
        /// <returns></returns>
        public static string ShowEncodings()
        {
            string result = "";
            foreach (EncodingInfo ei in Encoding.GetEncodings())
                result += (ei.Name + " ");
            return result;
        }

        /// <summary>
        /// Показать все файлы и директории в текущей директории
        /// </summary>
        /// <param name="userInput">Команда и аргументы, введенные пользователем</param>
        public static void ShowDirectory(string[] userInput)
        {
            try
            {
                if (userInput.Length == 1)
                {
                    Console.WriteLine("Обозначения: d - директория, f - файл");
                    ShowFolders();
                    ShowFiles();
                }
                else
                    Console.WriteLine("Слишком много параметров. Использование: dir");
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка доступа");
            }
        }

        /// <summary>
        /// Метод, меняющий текущую директорию (можно использовать как относительный, так и абсолютный путь)
        /// </summary>
        /// <param name="userInput">Команда и аргументы, введенные пользователем</param>
        public static void ChangeDirectory(string[] userInput)
        {
            if (userInput.Length == 1)
                Console.WriteLine("Не указан каталог. Использование: cd [имя каталога]");
            else if (userInput.Length == 2)
            {
                try
                {
                    // Меняем текущую директорию на введенную пользователем
                    Directory.SetCurrentDirectory(userInput[1]);
                    CurrentDirectory = Directory.GetCurrentDirectory();
                }
                catch (SecurityException)
                {
                    Console.WriteLine("Отсутствуют права доступа");
                }
                catch (Exception)
                {
                    Console.WriteLine("Указан неверный путь");
                }
            }
            else
                Console.WriteLine("Слишком много параметров. Использование: cd [имя каталога]");
        }

        /// <summary>
        /// Метод, показывающий список дисков, или устанавлювающий конкретный диск в качестве текущей директории
        /// </summary>
        /// <param name="userInput"></param>
        public static void Drive(string[] userInput)
        {
            if (userInput.Length == 1)
            {
                // Если аргументы не указаны - просто вывести список дисков
                ListDrives();
            }
            else if (userInput.Length == 2)
            {
                // Если указан диск - установить его в качестве текущей директории
                ChangeDrive(userInput[1]);
            }
            else
            {
                Console.WriteLine("Слишком много параметров");
            }
        }

        /// <summary>
        /// Метод, показывающий список дисков
        /// </summary>
        public static void ListDrives()
        {
            try
            {
                // Получаем список дисков
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                Console.WriteLine("Доступные диски:");
                // Проходимся по списку и выводим диски
                foreach (var d in allDrives)
                    Console.WriteLine(d);
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка ввода-вывода");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Отсутствуют права доступа");
            }
        }

        /// <summary>
        /// Метод, устанавлювающий конкретный диск в качестве текущей директории
        /// </summary>
        /// <param name="drive">Диск, который необходимо установить в качестве текущей директории</param>
        public static void ChangeDrive(string drive)
        {
            try
            {
                // Получаем список дисков
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (var d in allDrives)
                {
                    // Если диск, введенный пользователем, существует - установить его в качестве текущей директории
                    if (d.Name == drive)
                    {
                        Directory.SetCurrentDirectory(drive);
                        CurrentDirectory = Directory.GetCurrentDirectory();
                        return;
                    }
                }
                Console.WriteLine("Неизвестный диск. Убедитесь, что диск существует и указан в формате X:\\");
            }
            catch (IOException)
            {
                Console.WriteLine("Ошибка ввода-вывода");
            }
            catch (Exception)
            {
                Console.WriteLine("Отсутствуют права доступа");
            }
        }

        /// <summary>
        /// Метод, показывающий список всех файлов в директории
        /// </summary>
        public static void ShowFiles()
        {
            // Получаем список файлов
            string[] files = Directory.GetFiles(CurrentDirectory);
            foreach (var f in files)
                // Выводим только их имена с указанием, что это файл
                Console.WriteLine("f-- " + Path.GetFileName(f));
        }

        /// <summary>
        /// Метод, показывающий список всех поддиректорий в директории
        /// </summary>
        public static void ShowFolders()
        {
            // Получаем список поддиректорий
            string[] folders = Directory.GetDirectories(CurrentDirectory);
            foreach (var d in folders)
                // Выводим только их имена с указанием, что это директория
                Console.WriteLine("d-- " + Path.GetFileName(d));
        }
    }
}
