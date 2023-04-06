using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Peer4
{
    public class AppSettings
    {
        /// <summary>
        /// Список всех открытых файлов в момент закрытия приложения
        /// </summary>
        public IList<string> OpenFileNames { get; set; }
        /// <summary>
        /// Цветовая тема приложения в момент закрытия
        /// </summary>
        public string ColorScheme { get; set; }
        /// <summary>
        /// Частота автосохранения в момент закрытия
        /// </summary>
        public int AutosaveInterval { get; set; }
        /// <summary>
        /// Доступные интервалы автосохранения
        /// </summary>
        public static int[] AvailableAutosaveIntervals = { 1, 2, 5, 10 };
        /// <summary>
        /// Json-файл, куда будут записываться все настройки
        /// </summary>
        private static string jsonFileName = "settings.json";

        /// <summary>
        /// Конструктор, срабатывающий, если json-файл не найден, или произошла ошибка его чтения
        /// </summary>
        public AppSettings()
        {
            ColorScheme = "light";
            AutosaveInterval = 5;
            OpenFileNames = new List<string>();
        }

        /// <summary>
        /// Загрузка из json
        /// </summary>
        /// <returns>Список настроек и файлов</returns>
        public static AppSettings LoadFromJson()
        {
            try
            {
                // Десериализуем json
                return JsonSerializer.Deserialize<AppSettings>(File.ReadAllText(jsonFileName));
            }
            catch (Exception)
            {
                return new AppSettings();
            }
        }

        /// <summary>
        /// Запись и сериализация json файла
        /// </summary>
        public void SaveToJson()
        {
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText(jsonFileName, json);
            OpenFileNames.Clear();
        }
    }
}
