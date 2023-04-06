using System;

namespace EKRLib
{
    public abstract class Transport
    {
        private string model { get; set; }
        /// <summary>
        /// Модель
        /// </summary>
        public string Model
        {
            get
            {
                return model;
            }
            set
            {
                if (value.Length != 5)
                    throw new TransportException($"Недопустимая модель {Model}");
                foreach (var letter in value)
                    if ((int)letter < 65 || letter > 90)
                        throw new TransportException($"Недопустимая модель {Model}");
                model = value;
            }
        }

        private uint power { get; set; }
        /// <summary>
        /// Мощность в лошадиных силах
        /// </summary>
        public uint Power
        {
            get
            {
                return power;
            }
            set
            {
                if (value < 20)
                    throw new TransportException("мощность не может быть меньше 20 л.с.");
                power = value;
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="power">Мощность</param>
        public Transport(string model, uint power)
        {
            Model = model;
            Power = power;
        }

        /// <summary>
        /// Переопределенный метод для строкового представления объекта
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Model: {Model}, Power: {Power}";
        }

        /// <summary>
        /// Абстрактный метод string StartEngine(), переопределяемый в производных
        /// классах для получения звука(в виде строки), издаваемого транспортным средством.
        /// </summary>
        /// <returns></returns>
        public abstract string StartEngine();
    }
}
