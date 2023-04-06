using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EKRLib
{
    /// <summary>
    /// Класс Car
    /// </summary>
    public sealed class Car : Transport
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="model">Модель</param>
        /// <param name="power">Мощность</param>
        public Car(string model, uint power) : base(model, power) { }

        public override string StartEngine()
        {
            return $"{Model}: Vroom";
        }

        /// <summary>
        /// Переопределенный метод для строкового представления объекта
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Car. " + base.ToString();
        }
    }
}
